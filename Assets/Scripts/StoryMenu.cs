using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryMenu : MonoBehaviour
{
    public void StartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().buildIndex == 9)
            Cursor.visible = true;
        else
            Cursor.visible = false;
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        Cursor.visible = true;
    }
}

