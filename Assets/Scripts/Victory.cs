using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{

    public Player player;
    public int level1KilledEnemies = 84;
    public int level2KilledEnemies = 64;
    public bool isBoosKilled = false;
    public MenuController menu;

    // Update is called once per frame
    void Update()
    {
        if (player.enemiesKilled == level1KilledEnemies && SceneManager.GetActiveScene().buildIndex == 2)
            menu.VictoryScreen();
        if (player.enemiesKilled == level2KilledEnemies && SceneManager.GetActiveScene().buildIndex == 4)
            menu.VictoryScreen();
        if (player.enemiesKilled == level2KilledEnemies && SceneManager.GetActiveScene().buildIndex == 6)
            menu.VictoryScreen();
        if (player.isBossKilled == true && SceneManager.GetActiveScene().buildIndex == 8)
            menu.VictoryScreen();
    }
}
