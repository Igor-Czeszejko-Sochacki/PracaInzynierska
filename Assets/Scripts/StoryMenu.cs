using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StoryMenu : MonoBehaviour
{
    public void StartLevel()
    {
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
    public void BackToMainMenu()
    {
        EditorSceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}

