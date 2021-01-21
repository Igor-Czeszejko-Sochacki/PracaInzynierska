using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public bool isInMenu = false;
    public bool isGamePaused = false;
    public GameObject pauseMenu;
    public GameObject controllsMenu;
    public GameObject deathMenu;
    public GameObject victoryMenu;
    private void Start()
    {
        Time.timeScale = 1f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused == true && isInMenu == false)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }
    
    public void returnToMenu()
    {
        EditorSceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowControlls()
    {
        pauseMenu.SetActive(false);
        controllsMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        isInMenu = true;
    }

    public void BackToPauseMenu()
    {
        pauseMenu.SetActive(true);
        controllsMenu.SetActive(false);
        Time.timeScale = 0f;
        isInMenu = false;
    }

    public void ShowDeathScreen()
    {
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenu.SetActive(false);
        controllsMenu.SetActive(false);
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }
    public void RestartCurrentLevel()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
        Time.timeScale = 1f;
    } 
    public void VictoryScreen()
    {
        Cursor.lockState = CursorLockMode.Confined;
        victoryMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void StartNextLevel()
    {
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
}
