using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningEnemy : MonoBehaviour
{
    public Transform player;
    public Player playerModel;
    public GameObject ammoPack;
    public GameObject whiteEnemy;
    
    //Enemy stats
    public int health = 500;
    public int damage = 5; 
    public float sightRange;
    public float attackRange = 3;
    public float attackInterval = 0.5f;
    public float spawnNewEnemyTimer = 5f;

    //States
    public bool playerIsInRange = false;
    public bool playerInAttackRange = false;
    public bool attacked = false;
    private bool isDead = false;
    public bool spawnedNewEnemy = false;

    //Vector for ammunition pickup
    Vector3 ammoPackVector = new Vector3(0.5f, -2.5f, 0);

    // Start is called before the first frame update
    void Start()
    {
        playerModel = GameObject.Find("Player").GetComponent<Player>();
        player = GameObject.Find("Player").transform;
        sightRange = Random.Range(30, 50);
    }

    // Update is called once per frame
    void Update()
    {
        attackInterval -= Time.unscaledDeltaTime;
        if (attackInterval <= 0 && attacked == true)
            attacked = false;
        if (spawnedNewEnemy == true)
            spawnNewEnemyTimer -= Time.deltaTime;
        if (spawnNewEnemyTimer <= 0)
        {
            spawnedNewEnemy = false;
            spawnNewEnemyTimer = 5f;
        }
        //Checking player position and doing action according to the distance
        if (Vector3.Distance(player.position, transform.position) <= sightRange)
            playerIsInRange = true;
        else
            playerIsInRange = false;

        if (Vector3.Distance(player.position, transform.position) <= attackRange)
            playerInAttackRange = true;
        else
            playerInAttackRange = false;


        if (playerIsInRange == true && playerInAttackRange == false)
        {
            SpawnEnemy();
        }
        if (playerIsInRange == true && playerInAttackRange == true)
        {
            Attack();
        }
    }

    private void SpawnEnemy()
    {
        if (spawnedNewEnemy == false)
        {
            GameObject whiteEnemyMinion = Instantiate(whiteEnemy);
            whiteEnemy.transform.position = transform.position + transform.forward * 2;
            spawnedNewEnemy = true;
        }
    }

    private void Attack()
    {       
        //Attacking the player
        if (attacked == false)
        {
            playerModel.Health -= damage;
            playerModel.timeFromAttack = 0;
            attacked = true;
            attackInterval = 0.5f;
        }
    }

    //Gizmo to better show enemy range
    private void OnDrawGizmosSelected()
    {
        //Attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        //Sight range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
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

            //Checking if health if below 0 - if yes then die and respawn ammoPack
            if (health <= 0 && isDead == false)
            {
                bullet.gameObject.SetActive(false);
                isDead = true;
                Vector3 position = transform.position;
                Destroy(gameObject);
                GameObject ammoPrefab = Instantiate(ammoPack);
                ammoPrefab.transform.position = transform.position + ammoPackVector;
                playerModel.enemiesKilled += 1;
            }
        }
    }
}
