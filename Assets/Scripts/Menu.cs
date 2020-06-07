using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_name1;
    [SerializeField] private TextMeshProUGUI m_name2;
    [SerializeField] private TextMeshProUGUI m_goalScoreLabel;
    public void StartGame()
    {
        if (m_name1.text.Length != 0 && m_name2.text.Length != 0 && m_goalScoreLabel.text.Length != 0)
        {


            int score = 0;
            if (!int.TryParse(m_goalScoreLabel.text.Substring(0, m_goalScoreLabel.text.Length - 1), out score))
            {
                return;
            }
            if (score <= 0)
            {
                return;
            }
            PlayerPrefs.SetString("name1", m_name1.text);
            PlayerPrefs.SetString("name2", m_name2.text);
            PlayerPrefs.SetInt("goal", score);
            SceneManager.LoadScene(1);

        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
