using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Cat_AI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public Animator anim;

    public LayerMask whatIsGround, whatIsPlayer, Door;

    //Patrolling (walkPointRange = how_far_can_it_go)
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking 
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    bool alreadySit;

    //States
    public float sightRange, attackRange, DoorRange;
    public bool playerInSightRange, playerInAttackRange, DoorInAttackRange;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Trash").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        DoorInAttackRange = Physics.CheckSphere(transform.position, DoorRange, Door);

        if (!playerInSightRange && !playerInAttackRange && !DoorInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange && !DoorInAttackRange) Find_Trash();
        if (playerInSightRange && playerInAttackRange && !DoorInAttackRange) Founded();
        if (DoorInAttackRange) Waited();

    }
    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);
            anim.Play("walk");


        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached 
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate Random Point in Range
        float randomZ = Random.Range (-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3 (transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void Find_Trash()
    {
        agent.SetDestination(player.position);
        anim.Play("walk");
    }

    private void Founded()
    {
        agent.SetDestination (transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            anim.Play("sit");

            alreadyAttacked = true;
            //Invoke = CallOn
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Waited()
    {
        agent.SetDestination(transform.position);

        if (!alreadySit)
        {
            anim.Play("sit");

            alreadySit = true;
            //Invoke = CallOn
            Invoke(nameof(ResetSit), timeBetweenAttacks);
        }
    }

    private void ResetSit()
    {
        alreadySit = false;
    }
}
