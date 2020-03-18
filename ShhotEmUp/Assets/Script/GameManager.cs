using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager :MonoBehaviour
{
    float score;
     GameObject Score;
     GameObject pause;
    // Start is called before the first frame update
    void Start()
    {
        Score = GameObject.FindGameObjectWithTag("Score");
        pause = GameObject.FindGameObjectWithTag("Pause");
        Score.GetComponent<Text>().text = score.ToString();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!pause.transform.GetChild(0).gameObject.activeSelf)
            {
                
                pause.transform.GetChild(0).gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
                Unpause();
        }
        
    }
    /// <summary>
    /// add or substract value to the current score
    /// </summary>
    /// <param name="value"></param>
    public void ScoreChange(float value)
    {
        score += value;
        Score.GetComponent<Text>().text = score.ToString();
    }

    public void Unpause()
    {
        pause.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    public void Play()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1;
    }
}
