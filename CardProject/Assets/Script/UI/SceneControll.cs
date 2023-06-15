using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControll : MonoBehaviour
{
    public void Begin()
    {
     Time.timeScale = 1;
     SceneManager.LoadScene(1);
    }
    public void Quit()
    {
     Application.Quit();
    }
    public void Home()
    {
     Time.timeScale = 1;
     SceneManager.LoadScene(0);
    }
    public void StopGame()
    {
    Debug.Log("stopgame");
     Time.timeScale = 0;
    }
    public void ContinGame()
    {
     Time.timeScale = 1;
    }
    public void Level2()
    {
     Time.timeScale = 1;
     SceneManager.LoadScene(0);
    }
}
