using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{

    public float bulletSpeed = 8f;
    public float bulletLifeTime = 20f;

    private float currentBulletLifeTimer;
    // Start is called before the first frame update
    void Start()
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
            Destroy(gameObject);
        }
    }
}
