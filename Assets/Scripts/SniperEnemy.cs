using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : MonoBehaviour
{
    public Transform player;
    public Player playerModel;

    //Enemy stats
    public int health = 300;
    public int damage = 30;
    private bool isDead = false;
    public float attackRange;

    //Timer to wait for another shot
    public float shootingInterval = 5f;
    private float shootingTimer;

    //Vectors for bullet transformation
    Vector3 newVector = new Vector3(0, (1 / 2), 0);

    //States
    public bool playerInAttackRange = false;

    void Awake()
    {
        playerModel = GameObject.Find("Player").GetComponent<Player>();
        player = GameObject.Find("Player").transform;
        shootingTimer = shootingInterval;
        attackRange = Random.Range(20, 40);
    }

    void Update()
    {
        //Checking player position and doing action according to the distance
        if (Vector3.Distance(player.position, transform.position) <= attackRange)
            playerInAttackRange = true;
        else
            playerInAttackRange = false;

        if (playerInAttackRange == true)
        {
            Attacking();
        }
    }


    private void Attacking()
    {
        //Rotating the enemy in the direction of the player
        transform.LookAt(player);

        //Shooting
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            shootingTimer = shootingInterval;
            GameObject bullet = ObjectPoolingManger.Instance.SpawnBullet(false, damage);
            bullet.transform.position = transform.position + transform.forward * 2;
            bullet.transform.forward = ((playerModel.cylinder.transform.position - newVector) - transform.position).normalized;
            bullet.transform.Rotate(Vector3.right * 90);
        }
    }


    //Gizmo to better show enemy range
    private void OnDrawGizmosSelected()
    {
        //Attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    //Getting hit
    void OnTriggerEnter(Collider characterCollider)
    {
        //Checking collision
        if (characterCollider.GetComponent<BulletLogic>() != null)
        {

            BulletLogic bullet = characterCollider.GetComponent<BulletLogic>();

            //Checking if bullet was shot by player
            if (bullet.ShotByPlayer == true)
            {
                bullet.gameObject.SetActive(false);
                health -= bullet.playerBulletDamage;
            }

            //Checking if health if below 0 - if yes then die
            if (health <= 0 && isDead == false)
            {
                bullet.gameObject.SetActive(false);
                isDead = true;
                Destroy(gameObject);
            }
        }
    }
}
