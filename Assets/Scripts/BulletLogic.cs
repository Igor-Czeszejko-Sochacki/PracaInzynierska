using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{

    public float bulletSpeed = 50f;
    public float bulletLifeTime = 20f;
    public LayerMask objectMask;

    public int enemyBulletDamage;
    public int playerBulletDamage;

    private bool shotByPlayer;
    public bool ShotByPlayer { get { return shotByPlayer; } set { shotByPlayer = value; } }

    private float currentBulletLifeTimer;

    //When enabling the bullet we set its timer to bulletLifeTime
    void OnEnable()
    {
        currentBulletLifeTimer = bulletLifeTime;
    }

   
    void Update()
    {
        //Moving the bullet
        transform.position += transform.up * bulletSpeed * Time.deltaTime;

        //Counting the bullet life timer
        currentBulletLifeTimer -= Time.deltaTime;

        //If enough time passed - disable bullet
        if (currentBulletLifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    //If bullet hits a collider - disable it
    void OnTriggerEnter(Collider characterCollider)
    {
        if (characterCollider.gameObject.layer == objectMask)
        {
            gameObject.SetActive(false);
        }
    }
}
