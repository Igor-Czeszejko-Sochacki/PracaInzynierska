using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;
    public GameObject ammoPack;

    //Enemy stats
    public int health = 100;
    public int damage = 5;
    public float shootingDistance = 40f;
    private bool isDead = false;
    //Timer to wait for another shot
    public float shootingInterval = 4f;
    private float shootingTimer;
    
    Vector3 newVector = new Vector3(0, (1/2), 0);
    Vector3 newVector2 = new Vector3(0.5f,-1.85f, 0);


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        shootingTimer = Random.Range(0, shootingInterval);
    }

    void Update()
    {
        //Shooting mechanic
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && Vector3.Distance(transform.position,player.transform.position)<= shootingDistance)
        {
            shootingTimer = shootingInterval;
            GameObject bullet = ObjectPoolingManger.Instance.SpawnBullet(false,damage);
            bullet.transform.position = transform.position;
            bullet.transform.forward = ((player.cylinder.transform.position - newVector) - transform.position).normalized;
            bullet.transform.Rotate(Vector3.right * 90);
        }
    }

    //Getting hit
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
            if (health <= 0 && isDead == false)
            {
                bullet.gameObject.SetActive(false);
                isDead = true;
                Destroy(gameObject);
                GameObject ammoPrefab = Instantiate(ammoPack);
                ammoPrefab.transform.position = transform.position + newVector2;
                
            }
        }
    }
}
