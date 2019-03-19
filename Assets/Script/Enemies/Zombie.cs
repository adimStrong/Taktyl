using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Zombie : MonoBehaviour, IEnemy

{
    private NavMeshAgent navAgent;
    private float currentHealth, power, defense;

    public LayerMask aggroLayerMask;

    private Player player;

    public float maxHealth;

    private Collider[] aggroColliders;

    private void Start()
    {

        // set up navmesh and health
        navAgent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
    }


    private void FixedUpdate()
    {
        // set up agro colliderrs
       aggroColliders =  Physics.OverlapSphere(transform.position,10, aggroLayerMask);
        if(aggroColliders.Length > 0)
        {
        //    Debug.Log("Found playa");
            ChasePlayer(aggroColliders[0].GetComponent<Player>());
        }
    }

    public void PerformAttack()
    {
        player.TakeDamage(1);
        Debug.Log("Found playa");
        // play attack animation
    }

    public void TakeDamage(int amount)
    {

        // if damage has done
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void ChasePlayer(Player player)
    {
        navAgent.SetDestination(player.transform.position);
        this.player = player;
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (!IsInvoking("PerformAttack"))
            {
                InvokeRepeating("PerformAttack", 0.5f, 2f);

            }
            else
            {

                CancelInvoke();
            }
        }
        // play walking animation

    }

    void Die()
    {
        // destroy this
        Destroy(this.gameObject);
    }
}
