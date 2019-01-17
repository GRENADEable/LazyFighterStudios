using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWander : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent guardAgent;
    private Animator guardAnim;
    private float timer;

    void OnEnable()
    {
        guardAgent = GetComponent<NavMeshAgent>();
        guardAnim = GetComponent<Animator>();
        timer = wanderTimer;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            guardAgent.SetDestination(newPos);
            timer = 0;
        }

        if (guardAgent.velocity.magnitude < 0.1f)
        {
            guardAnim.SetBool("isIdle", true);
            // Debug.LogWarning("Idle");
        }
        if (guardAgent.velocity.magnitude > 0.2f)
        {
            guardAnim.SetBool("isIdle", false);
            // Debug.LogWarning("Not Idle");
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}
