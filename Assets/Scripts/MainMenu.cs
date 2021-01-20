using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class MainMenu : MonoBehaviour
{
   //Starting new game
   public void StartGame()
    {
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    //Quiting the game
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void Level1()
    {
        EditorSceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }
    public void Level2()
    {
        EditorSceneManager.LoadScene(4);
        Time.timeScale = 1f;
    }
    public void Level3()
    {
        EditorSceneManager.LoadScene(6);
        Time.timeScale = 1f;
    }
    public void Level4()
    {
        EditorSceneManager.LoadScene(8);
        Time.timeScale = 1f;
    }
}
