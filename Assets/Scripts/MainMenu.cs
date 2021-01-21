using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   //Starting new game
   public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
        Cursor.visible = true;
    }

    //Quiting the game
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void Level1()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    public void Level2()
    {
        SceneManager.LoadScene(4);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    public void Level3()
    {
        SceneManager.LoadScene(6);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    public void Level4()
    {
        SceneManager.LoadScene(8);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
}
