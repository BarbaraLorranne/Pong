using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_MaxSpeed;
    [SerializeField]
    private float m_SpeedIncBy;

    private Rigidbody2D m_Rigidbody2D;
    private Vector2 m_Velocity;

    private float m_InitialSpeed;
    private Vector3 m_InitialPosition;

    private AudioSource m_AudioSource;

    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks); 

        m_Velocity = m_Speed * (Random.Range(0, 100) < 50 ? Vector2.left : Vector2.right); 

        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.velocity = m_Velocity;

        m_InitialSpeed = m_Speed;
        m_InitialPosition = transform.position;

        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        m_AudioSource.Play();

        switch (collision.gameObject.name) 
        {
            case "player1":
                IncSpeed();
                UpdateVelocity(1.0f, GetHitYAxis(collision));
                break;

            case "player2":
                IncSpeed();
                UpdateVelocity(-1.0f, GetHitYAxis(collision));
                break;

            case "paredeCima":
            case "paredeBaixo":
                UpdateVelocity(m_Velocity.x, -m_Velocity.y);
                break;

            case "paredeEsquerda":
                ResetSpeed();
                UpdateVelocity(1.0f, 0.0f);
                transform.position = m_InitialPosition;
                GameController.Instance.IncScore(GameController.PlayerType.P1);
                break;

            case "paredeDireita":
                ResetSpeed();
                UpdateVelocity(-1.0f, 0.0f);
                transform.position = m_InitialPosition;
                GameController.Instance.IncScore(GameController.PlayerType.P2);
                break;

        }
    }
    private float GetHitYAxis(Collision2D player) 
    {
        return (transform.position.y - player.transform.position.y) / player.collider.bounds.size.y;
    }
    private void ResetSpeed() 
    {
        m_Speed = m_InitialSpeed;
    }
    private void IncSpeed()
    {
        m_Speed += m_SpeedIncBy;
        if(m_Speed > m_MaxSpeed)
        {
            m_Speed = m_MaxSpeed;
        }
    }
    private void UpdateVelocity(float x, float y) 
    { 
        m_Velocity.x = x;
        m_Velocity.y = y;
        m_Velocity = m_Velocity.normalized * m_Speed;

        m_Rigidbody2D.velocity = m_Velocity;
    }

}
