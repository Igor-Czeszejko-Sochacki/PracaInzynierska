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
    public Player player;

    // Update is called once per frame
    void Update()
    {

        //Checking how many enemies were killed and spawning next wave if enough died
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
    }
}
