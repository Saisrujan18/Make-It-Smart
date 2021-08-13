using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void PlayGame(){

        SceneManager.LoadScene("Level");
    }

    public void QuitGame(){

        Debug.Log("QUIT!!");
        Application.Quit();
    }

    public void PlayAgain(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
