using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterEnemy : MonoBehaviour
{
    public Transform player;
    public Player playerModel;
    public GameObject ammoPack;
    private NavMeshAgent agent;
    public LayerMask mask;

    //Enemy stats
    public int health = 100;
    public int damage = 5;
    private bool isDead = false;
    public float sightRange;
    public float attackRange;

    //Timer to wait for another shot
    public float shootingInterval = 2f;
    private float shootingTimer;

    //Vectors for bullet transformation
    Vector3 newVector = new Vector3(0, (1 / 2), 0);
    Vector3 newVector2 = new Vector3(0.5f, -1.85f, 0);

    //States
    public bool playerIsInRange = false;
    public bool playerInAttackRange = false;

    //Pathfinding to random point
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    void Awake()
    {
        playerModel = GameObject.Find("Player").GetComponent<Player>();
        player = GameObject.Find("Player").transform;
        shootingTimer = 0.5f;
        attackRange = Random.Range(5, 20);
        sightRange = Random.Range(attackRange,40);
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Checking player position and doing action according to the distance
        if (Vector3.Distance(player.position, transform.position) <= sightRange)
            playerIsInRange = true;
        else
            playerIsInRange = false;

        if (Vector3.Distance(player.position, transform.position) <= attackRange)
            playerInAttackRange = true;
        else
            playerInAttackRange = false;

        if (playerIsInRange == false && playerInAttackRange == false)
        {
            Patroling();
        }
        if (playerIsInRange == true && playerInAttackRange == false)
        {
            Chasing();
        }
        if (playerIsInRange == true && playerInAttackRange == true)
        {
            Attacking();
        }
    }

    private void Patroling()
    {
        //Checking if enemy has a walk point
        if (walkPointSet == false)
        {
            SearchWalkPoint();
        }
        if (walkPointSet == true)
        {
            agent.SetDestination(walkPoint);
        }

        //Checking if enenmy is close to the walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Generating random coordinates for enemy to patrol and creating Vector with them
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        //Checking if the walk point is withing the map
        if (Physics.Raycast(walkPoint, -transform.up, 2f, mask))
            walkPointSet = true;
    }

    private void Chasing()
    {
        //Enemy is chasing the player
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }

    private void Attacking()
    {
        //Stoping the enemy and rotating in the direction of the player
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        //Shooting
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            shootingTimer = shootingInterval;
            GameObject bullet = ObjectPoolingManger.Instance.SpawnBullet(false, damage);
            bullet.transform.position = transform.position + transform.forward*2;
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
                Destroy(gameObject);
                GameObject ammoPrefab = Instantiate(ammoPack);
                ammoPrefab.transform.position = transform.position + newVector2;
            }
        }
    }
}
