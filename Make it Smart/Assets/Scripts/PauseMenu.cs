using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static Boolean gameIsPaused;
    private static Boolean isGameOver;
    [SerializeField] GameObject[] panels;//when game is paused
    //0th Index-Upgrade Description Button
    //1st Index-Pause Button
    //2nd Index-Money
    //Last Index-PauseMenuUI

    private void Start()
    {
        gameIsPaused = false;
        isGameOver = false;
        panels[panels.Length - 1].SetActive(gameIsPaused);
    }
    void Update()
    {
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused)
                    Resume();
                else if (!gameIsPaused) 
                    Pause();
            }
        }
    }

    public void Resume(){

        gameIsPaused = false;
        panels[panels.Length - 1].SetActive(false);
        for (int i = 0; i < 3; i++)
            panels[i].SetActive(true);
        Time.timeScale = 1f;
        StartCoroutine(FindObjectOfType<MoneyScript>().refresh());
    }

    public void Pause(){
        gameIsPaused = true;
        Time.timeScale = 0f;
        panels[panels.Length - 1].SetActive(true);
        for (int i = 0; i < panels.Length - 1; i++) 
            panels[i].SetActive(false);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public static Boolean isGamePaused()
    {
        return gameIsPaused;
    }
    public void GameOver(bool flag)
    {
        isGameOver = flag;
    }
}
