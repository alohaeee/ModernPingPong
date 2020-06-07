using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    private int m_score1;
    private int m_score2;
    private int m_scoreGoal=0;
    [SerializeField] private TextMeshProUGUI m_scoreLabel1;
    [SerializeField] private TextMeshProUGUI m_scoreLabel2;
    [SerializeField] private TextMeshProUGUI m_timer;
    [SerializeField] private Ball m_ball;

    [SerializeField] private TextMeshProUGUI m_name1;
    [SerializeField] private TextMeshProUGUI m_name2;
    [SerializeField] private TextMeshProUGUI m_goalScoreLabel;

    private bool m_paused = false;
    [SerializeField] private GameObject m_pausePanel;
    [SerializeField] private GameObject m_winPanel;
    [SerializeField] private TextMeshProUGUI m_winLabel;

    private void Start()
    {
        if (m_timer)
        {
            m_scoreGoal = PlayerPrefs.GetInt("goal");
            m_goalScoreLabel.text = m_scoreGoal.ToString();
            m_name1.text = PlayerPrefs.GetString("name1");
            m_name2.text = PlayerPrefs.GetString("name2");
            ResetGame();
        }
    }
    private void Update()
    {
        PauseMenu();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PauseMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(m_paused)
            {
                Time.timeScale = 0;
                m_pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                m_pausePanel.SetActive(false);
            }
            m_paused = !m_paused;
        }
    }
    public void EndGame(string winnerName)
    {
        m_winLabel.text = winnerName;
        m_winPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        m_pausePanel.SetActive(false);
        m_paused = false;

    }
    public void UpScoreFirst()
    {
        m_score1++;
        m_scoreLabel1.text = m_score1.ToString();
        if (m_score1 >= m_scoreGoal)
        {
            EndGame(m_name1.text);
        }
    }
    public void UpScoreSecond()
    {
        m_score2++;
        m_scoreLabel2.text = m_score2.ToString();
        if(m_score2 >= m_scoreGoal)
        {
            EndGame(m_name2.text);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);

        if (m_timer.text == "0")
        {
            m_ball.gameObject.SetActive(true);
            m_timer.text = "3";
            m_timer.gameObject.SetActive(false);
            m_ball.RandMoveBall();
            yield return null;
        }

        else
        {
            var time = System.Int32.Parse(m_timer.text);
            time--;
            m_timer.text = time.ToString();
            yield return Timer();
        }

    }

    public void ResetGame()
    {
        var objs = FindObjectsOfType<Movement>();

        var transform1 = objs[0].transform;
        transform1.localScale = new Vector3(0.05f, 0.7f,1f);
        objs[0].ResetSpeed();

        var transform2 = objs[1].transform;
        transform2.localScale = new Vector3(0.05f, 0.7f, 1f);
        objs[1].ResetSpeed();

        m_timer.gameObject.SetActive(true);
        m_ball.ResetBall();
        m_ball.gameObject.SetActive(false);
        StartCoroutine(Timer());
    }
}
