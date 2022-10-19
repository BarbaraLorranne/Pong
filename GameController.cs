using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    //Singleton
    private static GameController s_Instance = null;
    public static GameController Instance {
        get { return s_Instance; }
    }
    void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            Destroy(gameObject);
    }

    public enum PlayerType
    {
        P1 = 0,
        P2
    }

    [SerializeField]
    private int m_EndGameScore;

    [Header("Game")]
    [SerializeField]
    private GameObject m_GameGroup;

    [Header("Score")]
    [SerializeField]
    private UnityEngine.UI.Text[] m_UITextScore = new UnityEngine.UI.Text[2];
    private int[] m_Score;

    [Header("Game Over")]
    [SerializeField]
    private GameObject m_GameOverGroup;
    [SerializeField]
    private UnityEngine.UI.Text m_UITexrWinner;

    private bool m_IsGameOver;

    void Start()
    {
        Time.timeScale = 1;
        m_Score = new int[2];
        ResetScore();
    }
    public void ResetScore()
    {
        m_Score[0] = 0;
        m_Score[1] = 0;
        m_IsGameOver = false;

        SetGameOver(false);
        m_UITextScore[0].text = m_Score[0].ToString();
        m_UITextScore[1].text = m_Score[1].ToString();
    }
    public void IncScore(PlayerType player)
    {
        int index = (int)player;
        ++m_Score[(int)player];
        m_UITextScore[index].text = m_Score[index].ToString();

        if (m_Score[index] == m_EndGameScore)
        {
            GameOver();
        }
    }
    public int GetScore(PlayerType player)
    {
        return m_Score[(int)player];
    }
    public PlayerType GetWinner()
    {
        return m_Score[0] > m_Score[1] ? PlayerType.P1 : PlayerType.P2;
    }
    private void GameOver1()
    {
        Debug.Log("GAME OVER! WINNER: " + GetWinner());
        m_IsGameOver = true;
       
    }
    private void GameOver()
    {
        m_UITexrWinner.text = "Jogador" + ((int)GetWinner() + 1) + "Ganhou!";
        SetGameOver(true);

    }
    private void SetGameOver(bool isGameOver)
    {
        m_IsGameOver = isGameOver;
        m_GameGroup.SetActive(!m_IsGameOver);
        m_GameOverGroup.SetActive(m_IsGameOver);
     
    }
   
   
   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || m_IsGameOver && Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
