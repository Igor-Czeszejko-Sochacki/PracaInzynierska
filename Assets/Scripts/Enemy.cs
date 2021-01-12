using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damage = 5;

    public float shootingInterval = 4f;
    private float shootingTimer;

    public float shootingDistance = 40f;
    private ShootingGun player;
    Vector3 proba = new Vector3(0, (1/2), 0);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<ShootingGun>();
        shootingTimer = Random.Range(0, shootingInterval);
        
    }

    // Update is called once per frame
    void Update()
    {
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && Vector3.Distance(transform.position,player.transform.position)<= shootingDistance)
        {
            shootingTimer = shootingInterval;
            GameObject bullet = ObjectPoolingManger.Instance.SpawnBullet(false);
            bullet.transform.position = transform.position;
            bullet.transform.forward = ((player.cylinder.transform.position - proba) - transform.position).normalized;
            bullet.transform.Rotate(Vector3.right * 90);
        }
    }
    void OnTriggerEnter(Collider characterCollider)
    {
        if (characterCollider.GetComponent<BulletLogic>() != null)
        {
            BulletLogic bullet = characterCollider.GetComponent<BulletLogic>();
            if (bullet.ShotByPlayer == true)
            {
                bullet.gameObject.SetActive(false);
                health -= bullet.playerBulletDamage;
            }
            if (health <= 0)
            {
                bullet.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
