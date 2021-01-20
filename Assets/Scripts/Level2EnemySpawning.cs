using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2EnemySpawning : MonoBehaviour
{
    public GameObject firstWall;
    public GameObject secondWall;
    public GameObject thirdWall;

    public GameObject firstEnemies;
    public GameObject secondEnemies;
    public GameObject thirdEnemies;
    public GameObject forthEnemies;
    public MenuController menu;
    public Player player;

    // Update is called once per frame
    void Update()
    {
        if (player.enemiesKilled == 13)
        {
            firstWall.SetActive(false);
            secondEnemies.SetActive(true);
        }
        else if (player.enemiesKilled == 29)
        {
            secondWall.SetActive(false);
            thirdEnemies.SetActive(true);
        }
        else if (player.enemiesKilled == 49)
        {
            thirdWall.SetActive(false);
            forthEnemies.SetActive(true);
        }
        else if (player.enemiesKilled == 64)
        {
            menu.VictoryScreen();
        }
            
    }
}
