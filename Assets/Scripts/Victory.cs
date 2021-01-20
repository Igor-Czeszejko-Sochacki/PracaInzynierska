using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Victory : MonoBehaviour
{

    public Player player;
    public int level1KilledEnemies = 126;
    public int level2KilledEnemies = 65;
    public MenuController menu;

    // Update is called once per frame
    void Update()
    {
        if (player.enemiesKilled == level2KilledEnemies && EditorSceneManager.GetActiveScene().buildIndex == 1)
            menu.VictoryScreen();
        if (player.enemiesKilled == level2KilledEnemies && EditorSceneManager.GetActiveScene().buildIndex == 2)
            menu.VictoryScreen();
    }
}
