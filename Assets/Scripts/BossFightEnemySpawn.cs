using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightEnemySpawn : MonoBehaviour
{
    public float timeBetweenWaves = 90;
    public float waveCountdown = 90;
    public Transform[] shootingEnemyPoints;
    public Transform[] spawningEnemyPoints;
    public Transform[] bomberEnemyPoints;
    public GameObject shootingEnemy;
    public GameObject bomberEnemy;
    public GameObject spawningEnemy;
    public bool isBossDead = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        //Substracting wave timer
        waveCountdown -= Time.deltaTime;

        //Checking if wave timer = 0 and if the boss is dead
        if (waveCountdown <= 0 && isBossDead == false)
        {
            //Reseting wave timer and spawning enemies
            waveCountdown = timeBetweenWaves;
            SpawnEnemies();
        }
    }

    //Spawning enemies in their spawn points
    public void SpawnEnemies()
    {
        foreach (Transform point in shootingEnemyPoints)
        {
            GameObject spawnedEnemy = Instantiate(shootingEnemy);
            spawnedEnemy.transform.position = point.transform.position;
            spawnedEnemy.transform.rotation = point.transform.rotation;
        }

        foreach (Transform point in bomberEnemyPoints)
        {
            GameObject spawnedEnemy = Instantiate(bomberEnemy);
            spawnedEnemy.transform.position = point.transform.position;
            spawnedEnemy.transform.rotation = point.transform.rotation;
        }

        foreach (Transform point in spawningEnemyPoints)
        {
            GameObject spawnedEnemy = Instantiate(spawningEnemy);
            spawnedEnemy.transform.position = point.transform.position;
            spawnedEnemy.transform.rotation = point.transform.rotation;
        }
    }
}
