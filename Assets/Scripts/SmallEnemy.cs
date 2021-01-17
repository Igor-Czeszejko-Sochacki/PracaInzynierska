using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemy : MonoBehaviour
{
    public Transform player;
    public Player playerModel;
    private NavMeshAgent agent;

    //Enemy stats
    public int health = 100;
    public int damage = 5;
    public float sightRange;
    public float attackRange = 3;
    public float attackInterval = 0.5f;

    //States
    public bool playerIsInRange = false;
    public bool playerInAttackRange = false;
    public bool attacked = false;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        playerModel = GameObject.Find("Player").GetComponent<Player>();
        player = GameObject.Find("Player").transform;
        sightRange = Random.Range(90, 100);
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Reseting melee cooldown
        attackInterval -= Time.unscaledDeltaTime;
        if (attackInterval <= 0 && attacked == true)
            attacked = false;

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
            Chasing();
        }
        if (playerIsInRange == true && playerInAttackRange == true)
        {
            Attack();
        }
    }

    //Chasing the player
    private void Chasing()
    {
        //Enemy is chasing the player
        agent.SetDestination(player.position);
    }

    //Attacking the player
    private void Attack()
    {
        //Stopping the enemy
        agent.SetDestination(transform.position);

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
