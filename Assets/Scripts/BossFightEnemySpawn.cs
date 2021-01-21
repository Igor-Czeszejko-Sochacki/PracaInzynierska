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
        waveCountdown -= Time.deltaTime;
        if (waveCountdown <= 0 && isBossDead == false)
        {
            waveCountdown = timeBetweenWaves;
            SpawnEnemies();
        }
    }

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
