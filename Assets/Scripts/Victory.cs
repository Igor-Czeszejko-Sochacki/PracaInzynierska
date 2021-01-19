using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{

    public Player player;
    public int level1KilledEnemies = 126;
    public PauseMenu menu;

    // Update is called once per frame
    void Update()
    {
        if (player.enemiesKilled == level1KilledEnemies)
            menu.VictoryScreen();
    }
}
