using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardFSM : MonoBehaviour
{
    [Header("Guard Ditance Check")]
    public float attackDistance;
    //public float closeDistance;
    public float chaseDistance;
    public float distanceToPlayer;
    //public float attackDelay;
    [Header("Enemy FoV")]
    public float foV;
    public float veticalFov;
    [Header("Guard Speed")]
    public float guardWalking;
    public float guardRunning;

    [Header("Wander Variables")]
    public float wanderRadius;
    public float maxWanderTimer;
    public GameObject player;
    //private bool isAttacking;
    private int currCondition;
    private int wanderCondition = 1;
    private int chaseCondition = 2;
    private int attackCondition = 3;
    private NavMeshAgent guardAgent;
    private Transform target;
    private float wanderTimer;
    //private float attackTime;
    private Vector3 tarDir;
    private Animator anim;
    [SerializeField]
    private float angle;
    [SerializeField]
    private float verticalAngle;
    void Start()
    {
        //isAttacking = false;
        guardAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //attackTime = attackDelay;
        wanderTimer = maxWanderTimer;
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

        if (distanceToPlayer > chaseDistance && currCondition != wanderCondition)
        {
            //Wander
            currCondition = 1;
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);
        }

        if (((angle < foV && distanceToPlayer < chaseDistance && verticalAngle < veticalFov) /*|| distanceToPlayer < closeDistance*/) && currCondition != chaseCondition)
        // if (angle < foV && distanceToPlayer < chaseDistance)
        {
            //Chase Player
            currCondition = 2;
            anim.SetBool("isRunning", true);
            anim.SetBool("isAttacking", false);
        }

        if (distanceToPlayer < attackDistance && currCondition != attackCondition)
        {
            //Attack Player
            currCondition = 3;
            anim.SetBool("isAttacking", true);
        }

        if (!player.activeInHierarchy)
        {
            currCondition = 1;
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);
        }

        if (guardAgent.acceleration <= 0)
            currCondition = 4;
        // else
        //     anim.SetBool("isAttacking", false);

        // if (isAttacking)
        // {
        //     attackTime += Time.deltaTime;

        //     if (attackTime >= attackDelay)
        //     {
        //         //Attack
        //         attackTime = 0;
        //         Debug.LogWarning("Attacking Player");
        //     }

        // }
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
                // isAttacking = false;
                break;

            case 3: //Attack Condition
                // isAttacking = true;
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
}
