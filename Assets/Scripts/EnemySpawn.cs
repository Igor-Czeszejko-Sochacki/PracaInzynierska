using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private int waveNumber = 0;
    public float timeBetweenWaves = 90;
    public float waveCountdown = 90;

    public Transform[] spawnPoints;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        //Counting down the timer to spawn a new wave
        waveCountdown -= Time.deltaTime;

        //If enough time passed - respawn new wave
        if (waveCountdown <= 0 && waveNumber <= 2)
        {
            waveNumber++;
            waveCountdown = timeBetweenWaves;
            SpawnEnemies();
        }   
    }

    //Spawning enemies in their spawn points
    public void SpawnEnemies()
    {
        foreach (Transform point in spawnPoints)
        {
            GameObject spawnedEnemy = Instantiate(enemy);
            spawnedEnemy.transform.position = point.transform.position;
            spawnedEnemy.transform.rotation = point.transform.rotation;
        }
    }
}
