using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Testing_Multiple : MonoBehaviour
{
    public GameObject[] AllTrash;
    public Transform NearestOBJ;
    float Distance;
    float NearestDistance = 10000;

    public NavMeshAgent agent;

    public Animator anim;
    public AudioSource meowSFX;
    private bool meowPlay;

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
    float time;
    float timeDelay;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        time = 0f;
        timeDelay = 2.5f;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        DoorInAttackRange = Physics.CheckSphere(transform.position, DoorRange, Door);

        AllTrash = GameObject.FindGameObjectsWithTag("Trash");

        FindNear();

        time = time + 1f * Time.deltaTime;

        if (time >= timeDelay)
        {
            time = 0f;
            walkPointSet = false;
        }

        NearestDistance = 10000;

        if (!playerInSightRange && !playerInAttackRange && !DoorInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange && !DoorInAttackRange) Find_Trash();
        if (playerInSightRange && playerInAttackRange && !DoorInAttackRange) Founded();
        if (DoorInAttackRange) Waited();
    }

    private void FindNear()
    {
        for (int i = 0; i < AllTrash.Length; i++)
        {
            Distance = Vector3.Distance(this.transform.position, AllTrash[i].transform.position);

            if (Distance < NearestDistance)
            {
                NearestOBJ = AllTrash[i].transform;
                NearestDistance = Distance;
            }
        }
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
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void Find_Trash()
    {
        agent.SetDestination(NearestOBJ.position);
        anim.Play("walk");
        meowSFX.Pause();
        meowPlay = false;
    }

    private void Founded()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(NearestOBJ);

        if (!alreadyAttacked)
        {
            anim.Play("sit");
            anim.Play("sitting");
            if (meowPlay == false)
            {
                meowSFX.Play();
                meowPlay = true;
            }

            alreadyAttacked = true;
            //Invoke = CallOn
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
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
            anim.Play("sitting");
            if (meowPlay == false)
            {
                meowSFX.Play();
                meowPlay = true;
            }

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
