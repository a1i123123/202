using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player; // Player nesnesini GameObject olarak tanýmladýk

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange = 10f; // Oyuncu görüþ menzili
    public float attackRange = 2f; // Oyuncu saldýrý menzili
    public bool playerInSightRange, playerInAttackRange;

    public int health = 100; // Saðlýk deðerixx

    private void Awake()
    {
        player = GameObject.Find("PlayerObj"); // Player nesnesini bulup atadýk

        if (player != null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        else
        {
            Debug.LogWarning("PlayerObj not found in the scene!");
        }


    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, LayerMask.GetMask("Player"));
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, LayerMask.GetMask("Player"));

        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        if (playerInAttackRange && playerInSightRange)
            AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        float distanceToWalkPoint = Vector3.Distance(transform.position, walkPoint);
        if (distanceToWalkPoint > 1f)
        {
            // bakcez 
            
        }
    }


    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, LayerMask.GetMask("Ground")))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    public int damage = 10; // zombi dmg 

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        if (player != null)
        {
            transform.LookAt(player.transform);

            if (!alreadyAttacked)
            {
                alreadyAttacked = true;

                
                Player playerScript = player.GetComponent<Player>();
                if (playerScript != null)
                {
                    playerScript.TakeDamage(damage);
                }
                else
                {
                    Debug.LogWarning("Player script not found on PlayerObj!");
                }

                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        else
        {
            Debug.LogWarning("Player reference is null!");
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Saðlýk deðerini düþürrr

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Morg
        
        Destroy(gameObject); // Zombiyi 3 saniye sonra yok et(cesedi icin yazdim umarim oledir)
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
