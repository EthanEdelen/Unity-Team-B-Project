using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// This script manages UI, Game statistics like master volume and score, and Allows gameobjects to find eachother easily.
/// </summary>
public class GM_Script : MonoBehaviour
{
    public static GM_Script GM;  //This means there is only one GM at any given point
    private int score;  //Access score through set and get.
    public TextMeshProUGUI scoreText;
    public GameObject playerObject;
    public bool gamePaused;
    public GameObject pausePanel;


    // Start is called before the first frame update
    public void Start()
    {
        GM = this;  //This means when an object with GM_Script is put into a scene, it becomes the GM!
    }

    // Update is called once per frame
    public void Update()
    {
        UpdateUI();
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            gamePaused = !gamePaused;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    void UpdateUI()
    {
        scoreText.text = "SCORE:  " + score;
    }
    public void AddScore(int i)
    {
        score += i;
        UpdateUI();
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }


}
