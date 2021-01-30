using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;

    public static float damageMultiplier = 1.0f;

    [SerializeField]
    float speed = 4.5f;

    [SerializeField]
    GameObject attackObject;

    [SerializeField]
    float attackDuration = 0.2f;

    [SerializeField]
    float combatCooldownTime = 0.2f;

    [SerializeField]
    float dashCooldownTime = 3.0f;

    [SerializeField]
    float dashForce = 8.0f;

    [SerializeField]
    GameObject dashParticles;

    float dashCooldown = 0;    

    Camera mainCam;
    bool canMove = true;
    Rigidbody rb;
    float combatCooldown = 0;


    void Start()
    {
        player = this;
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;   
    }

    void Update()
    {

        MovePlayer();
        RotatePlayer();
        
        if(CombatCooldown())
        {
            CombatAction();
        }
    }

    bool DashCooldown()
    {
        if (dashCooldown <= 0)
        {
            return true;
        }
        else
        {
            dashCooldown -= Time.deltaTime;
            return false;
        }
    }

    bool CombatCooldown()
    {
        if(combatCooldown<=0)
        {
            return true;
        }
        else
        {
            combatCooldown -= Time.deltaTime;
            return false;
        }
    }

    void CombatAction()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Strike();
        }else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Block();
        }
    }

    void Strike()
    {
        combatCooldown = combatCooldownTime;
        Destroy(Instantiate(attackObject, transform.position, transform.rotation), attackDuration);
    }
    void Block()
    {
        combatCooldown = combatCooldownTime;
    }

    void MovePlayer()
    {
        if(canMove)
        {
            Vector3 dir;
            dir.x = Input.GetAxis("Horizontal");
            dir.z = Input.GetAxis("Vertical");
            dir.y = 0;

            if(DashCooldown()&&Input.GetKeyDown(KeyCode.LeftShift))
            {
                rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
                dashCooldown = dashCooldownTime;
                Destroy(Instantiate(dashParticles,transform.position,transform.rotation), 1f);
            }
            else
            {
                rb.velocity = dir*speed;
            }

        }
    }

    void RotatePlayer()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 target = hit.point;
            target.y = transform.position.y;

            if (Vector3.Distance(target, transform.position) > 0.5f)
            {
                transform.LookAt(target);
            }
        }
    }
}
