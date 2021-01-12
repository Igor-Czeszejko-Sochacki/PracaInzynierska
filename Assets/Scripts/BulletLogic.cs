using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{

    public float bulletSpeed = 2f;
    public float bulletLifeTime = 20f;
    public int enemyBulletDamage = 5;
    public int playerBulletDamage = 20;
    private bool shotByPlayer;
    public bool ShotByPlayer { get { return shotByPlayer; } set { shotByPlayer = value; } }

    private float currentBulletLifeTimer;
    // Start is called before the first frame update
    void OnEnable()
    {
        currentBulletLifeTimer = bulletLifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
        currentBulletLifeTimer -= Time.deltaTime;
        if (currentBulletLifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
