using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour {

    [SerializeField]
    Text timer;

    [SerializeField]
    GameObject Pause;

    [SerializeField]
    Text timeToStart;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text GOscoreText;
    [SerializeField]
    Text killsText;

    public static float startTime = 3;

    public static float gameTime = 180;

    public static int score;

    public static int kills;

    public static bool gameStarted;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeController();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Pause.SetActive(true);
        }

        scoreText.text = "Score: " + score.ToString();
        GOscoreText.text = "Score: " + score.ToString();
        killsText.text = "Kills: " + kills.ToString();

    }

    public void returnToGame()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }


    void timeController()
    {
        gameTime -= Time.deltaTime;
        startTime -= Time.deltaTime;

        int seconds = (int)(gameTime % 60);
        int minutes = (int)(gameTime / 60) % 60;

        string timerFormat = string.Format("{0:0}: {1:00}", minutes, seconds);

        

        if(startTime > 0)
        {
            timeToStart.text = Mathf.RoundToInt(startTime).ToString();
        }
        else
        {
            gameStarted = true;
            timer.text = timerFormat;
            timeToStart.enabled = false;
        }

        
        
    }
}
