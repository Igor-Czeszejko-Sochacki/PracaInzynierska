using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour
{
    public Transform player;
    public Player playerModel;
    private NavMeshAgent agent;
    public LayerMask mask;

    //Enemy stats
    public int health = 2000;
    public int damage = 5;
    private bool isDead = false;
    public float sightRange = 200;
    public float attackRange = 5;

    //Timer to wait for another shot
    public float shootingInterval = 0.5f;
    private float shootingTimer = 0.5f;
    public float attackDuration = 5;
    public float attackCooldown = 5;
    public float attackInterval = 0.5f;

    //Vectors for bullet transformation
    Vector3 newVector = new Vector3(0, (1 / 2), 0);

    //States
    public bool atacking = false;
    public bool playerIsInRange = false;
    public bool playerInAttackRange = false;
    public bool attackedMelee = false;


    void Start()
    {
        playerModel = GameObject.Find("Player").GetComponent<Player>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Reseting melee cooldown
        attackInterval -= Time.unscaledDeltaTime;
        if (attackInterval <= 0 && attackedMelee == true)
            attackedMelee = false;

        //Checking player position and doing action according to the distance
        if (Vector3.Distance(player.position, transform.position) <= sightRange)
            playerIsInRange = true;
        else
            playerIsInRange = false;

        if (Vector3.Distance(player.position, transform.position) <= attackRange)
            playerInAttackRange = true;
        else
            playerInAttackRange = false;

        //Reseting shooting attack cooldown
        if (attackDuration < 0)
        {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0)
            {
                attackDuration = 5;
                attackCooldown = 5;
            }
        }

        //Doing actions accrding to the distance from the player
        if (playerIsInRange == true && playerInAttackRange == false)
        {  
            Chasing();
        }

        if (playerIsInRange == true && playerInAttackRange == true)
        {  
            Attacking();
        }
    }

    private void Chasing()
    {
        //Enemy is chasing the player
        agent.SetDestination(player.position);
        
        //Attacking if the duration if still positive number
        if (attackDuration >= 0)
        {
            //Shooting
            shootingTimer -= Time.deltaTime;
            if (shootingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= sightRange)
            {
                shootingTimer = shootingInterval;
                GameObject bullet = ObjectPoolingManger.Instance.SpawnBullet(false, damage);
                bullet.transform.position = transform.position + transform.forward * 2;
                bullet.transform.forward = ((playerModel.cylinder.transform.position - newVector) - transform.position).normalized;
                bullet.transform.Rotate(Vector3.right * 90);
            }

            //Substracting attack duration
            attackDuration -= Time.deltaTime;
        }
        
    }

    //Melee attacking
    private void Attacking()
    {
        //Stoping the enemy
        agent.SetDestination(transform.position);

        //Attacking the player
        if (attackedMelee == false)
        {
            playerModel.Health -= damage;
            playerModel.timeFromAttack = 0;
            attackedMelee = true;
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
                Destroy(gameObject);
                playerModel.isBossKilled = true;
            }
        }
    }
}
