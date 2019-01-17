using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardFSM : MonoBehaviour
{
    [Header("Guard Ditance Check")]
    public float attackDistance;
    public float closeDistance;
    public float chaseDistance;
    public float distanceToPlayer;
    [Header("Enemy FoV")]
    public float foV;
    public float verticalFov;
    [Header("Guard Speed")]
    public float guardWalking;
    public float guardRunning;

    [Header("Wander Variables")]
    public float wanderRadius;
    public float maxWanderTimer;
    [Header("Other")]
    public GameObject player;
    public Light flashlight;
    private int currCondition;
    private int wanderCondition = 1;
    private int chaseCondition = 2;
    private int attackCondition = 3;
    private NavMeshAgent guardAgent;
    private Transform target;
    private float wanderTimer;
    private Vector3 tarDir;
    private Animator guardAnim;
    [SerializeField]
    private float angle;
    [SerializeField]
    private float verticalAngle;
    void Start()
    {
        guardAgent = GetComponent<NavMeshAgent>();
        guardAnim = GetComponent<Animator>();
        wanderTimer = maxWanderTimer;
        flashlight.GetComponent<Light>();
    }

    void Update()
    {
        //Distance check to Player
        Vector3 chaseLength = this.transform.TransformDirection(Vector3.forward) * chaseDistance;
        Debug.DrawRay(this.transform.position, chaseLength, Color.green);

        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        tarDir = player.transform.position - this.transform.position;
        angle = Vector3.Angle(this.tarDir, this.transform.forward);
        verticalAngle = Vector3.Angle(this.tarDir, -this.transform.up);

        if ((distanceToPlayer > chaseDistance || angle > foV) && currCondition != wanderCondition)
        {
            //Wander
            currCondition = 1;
            flashlight.color = Color.white;
            guardAnim.SetBool("isRunning", false);
            guardAnim.SetBool("isAttacking", false);
        }

        if (angle < foV && distanceToPlayer < attackDistance && verticalAngle < verticalFov && currCondition != attackCondition)
        {
            //Attack Player
            currCondition = 3;
            guardAnim.SetBool("isAttacking", true);
        }
        //else if (angle < foV && distanceToPlayer < chaseDistance && verticalAngle < verticalFov && currCondition != chaseCondition)
        if ((distanceToPlayer < chaseDistance && angle < foV || distanceToPlayer < closeDistance) && currCondition != chaseCondition)
        {
            //Chase Player
            currCondition = 2;
            flashlight.color = Color.red;
            guardAnim.SetBool("isRunning", true);
            guardAnim.SetBool("isAttacking", false);

        }

        if (!player.activeInHierarchy)
        {
            currCondition = 1;
            guardAnim.SetBool("isRunning", false);
            guardAnim.SetBool("isAttacking", false);
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

    void FixedUpdate()
    {
        switch (currCondition)
        {
            case 1: //Wander Condition
                wanderTimer += Time.deltaTime;

                if (wanderTimer >= maxWanderTimer)
                {
                    //isAttacking = false;
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    guardAgent.SetDestination(newPos);
                    guardAgent.speed = guardWalking;
                    wanderTimer = 0;
                }
                // Debug.LogWarning("Wandering");
                break;

            case 2: //Chase Condition
                guardAgent.SetDestination(player.transform.position);
                guardAgent.speed = guardRunning;
                Debug.LogWarning("Chasing Player");
                break;

            case 3: //Attack Condition
                // Debug.LogWarning("Attacking Player");
                break;

            case 4: //Idle Condition
                Debug.LogWarning("Idle");
                break;
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
