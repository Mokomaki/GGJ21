using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeMolli : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    [SerializeField]
    float backingSpeed = 1.2f;

    [SerializeField]
    float attackCooldownTime = 1.5f;

    [SerializeField]
    GameObject attackObject;

    [SerializeField]
    float attackDuration = 0.1f;

    float attackCooldown = 0;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target.position) > 2.5f)
        {
            if(attackCooldown <= 0)
            {
                agent.SetDestination(target.position);
            }
        }else
        {
            agent.ResetPath();
        }

        if(attackCooldown<=0)
        {
            rb.velocity = Vector3.zero;
        }
    }

    void Update()
    {
        if(Vector3.Distance(transform.position,target.position)<2.5f)
        {
            transform.LookAt(target);
            AttackIfPossible();
        }

        if(attackCooldown>0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    void AttackIfPossible()
    {
        if(attackCooldown <= 0)
        {
            Debug.Log("LÖIN SUA LOL");
            //Destroy(Instantiate(attackObject, transform.position, transform.rotation), attackDuration);
            attackCooldown = attackCooldownTime;
        }
        else
        {
            rb.velocity = -transform.forward * backingSpeed;
        }
    }
}
