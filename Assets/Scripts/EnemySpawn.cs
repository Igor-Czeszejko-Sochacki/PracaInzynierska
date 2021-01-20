using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private int waveNumber = 0;
    public float timeBetweenWaves = 60;
    public float waveCountdown = 60;
    //public Transform objectlocation;
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
        waveCountdown -= Time.deltaTime;
        if (waveCountdown <= 0 && waveNumber <= 4)
        {
            waveNumber++;
            waveCountdown = timeBetweenWaves;
            SpawnEnemies();
        }   
    }

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
