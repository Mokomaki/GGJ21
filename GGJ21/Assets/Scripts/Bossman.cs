using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bossman : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    [SerializeField]
    float backingSpeed = 3f;

    [SerializeField]
    float attackCooldownTime = 1f;

    [SerializeField]
    GameObject attackObject;

    [SerializeField]
    float attackDuration = 0.1f;

    float attackCooldown = 0;

    Rigidbody rb;

    Animator anim;

    float shootCooldownTime = 6;

    float shootCooldown;

    [SerializeField]
    GameObject ammo;

    [SerializeField]
    Transform shootPos;

    void Start()
    {
        shootCooldown = shootCooldownTime;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Shoot()
    {
        if(shootCooldown>0)
        {
            shootCooldown -= Time.deltaTime;
        }
        if(Vector3.Distance(transform.position,target.position)>10)
        {
            if(shootCooldown<=0)
            {
                anim.SetBool("Shoot", true);
                StartCoroutine(animBoolOff("Shoot", attackDuration));
                transform.LookAt(target);
                Destroy(Instantiate(ammo, shootPos.position, transform.rotation), 5);
                shootCooldown = shootCooldownTime;
            }
        }
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target.position) > 2.5f)
        {
            if (attackCooldown <= 0)
            {
                agent.SetDestination(target.position);
            }
        }
        else
        {
            agent.ResetPath();
        }

        if (attackCooldown <= 0)
        {
            rb.velocity = Vector3.zero;
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < 2.5f)
        {
            transform.LookAt(target);
            AttackIfPossible();
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        //Shoot();
    }

    void AttackIfPossible()
    {
        if (attackCooldown <= 0)
        {
            anim.SetBool("Attack", true);
            StartCoroutine(animBoolOff("Attack",attackDuration));
            Destroy(Instantiate(attackObject, transform.position, transform.rotation), attackDuration);
            attackCooldown = attackCooldownTime;
        }
        else
        {
            rb.velocity = -transform.forward * backingSpeed;
        }
    }

    IEnumerator animBoolOff(string name, float t)
    {
        yield return new WaitForSeconds(t);
        anim.SetBool(name, false);
    }

}
