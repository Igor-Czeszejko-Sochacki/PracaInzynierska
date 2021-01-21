using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

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
        if (player.enemiesKilled == level1KilledEnemies && EditorSceneManager.GetActiveScene().buildIndex == 2)
            menu.VictoryScreen();
        if (player.enemiesKilled == level2KilledEnemies && EditorSceneManager.GetActiveScene().buildIndex == 4)
            menu.VictoryScreen();
        if (player.enemiesKilled == level2KilledEnemies && EditorSceneManager.GetActiveScene().buildIndex == 6)
            menu.VictoryScreen();
        if (player.isBossKilled == true && EditorSceneManager.GetActiveScene().buildIndex == 8)
            menu.VictoryScreen();
    }
}
