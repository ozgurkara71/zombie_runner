using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform target;
    // The variable that will lead the enemy to the player
    NavMeshAgent navMeshAgent;

    // Setting the chasing range
    [SerializeField] float chaseRange = 10f;
    float distanceToTarget = Mathf.Infinity;

    // If player shooted to zombie, zombie should chase
    bool isProvoked = false;

    [SerializeField] float turnSpeed = 5f;

    EnemyHealth health;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        health = GetComponent<EnemyHealth>();

        target = FindObjectOfType<PlayerHealth>().transform;
    }

    void Update()
    {
        if(health.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
            return;
        }

        distanceToTarget = Vector3.Distance(target.position, gameObject.transform.position);

        if(isProvoked)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    public void OnDamageTaken()
    {
        isProvoked= true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    void ChaseTarget()
    {
        // If player get away from the zombie, stop attack animation
        GetComponent<Animator>().SetBool("Attack", false);

        // By using GetComponent<Animator>().SetBool("ozgur", true);,
        // we can make it transition to the animation that satisfies the condition
        // (assuming we've set up transitions in the animator)
        GetComponent<Animator>().SetBool("move", true);

        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        GetComponent<Animator>().SetBool("move", false);
        GetComponent<Animator>().SetBool("Attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - gameObject.transform.position).normalized;
        // y ye 0 verdik cunku y de bir animasyon ayarlamasi vs yapmadik
        // set y to 0 because there isn't any animation for y
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // transform.rotation = where the target is, we need to rotate at a certain speed
        // Slerp() provides a more stable transition between two vectors.
        // For example, if there's a need to rotate in both x and z axes simultaneously,
        // it ensures a more stable rotation.
        // Quaternion.Slerp(current_pos, turning_pos, BuildCompression time_to_turn);
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, lookRotation, 
            Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
