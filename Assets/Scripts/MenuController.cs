using System.Collections;
using System.Collections.Generic;
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
        Cursor.visible = false;
    }
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }
    
    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
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
        Cursor.visible = true;
    }
    public void RestartCurrentLevel()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
        Time.timeScale = 1f;
        Cursor.visible = false;
    } 
    public void VictoryScreen()
    {
        Cursor.lockState = CursorLockMode.Confined;
        victoryMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        Cursor.visible = true;
    }

    public void StartNextLevel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
}
