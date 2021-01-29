using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 3;

    [SerializeField]
    GameObject attackObject;

    [SerializeField]
    float attackDuration = 0.2f;

    [SerializeField]
    float combatCooldownTime = 0.2f;
    
    Camera mainCam;
    bool canMove = true;
    Rigidbody rb;
    float combatCooldown = 0;
    

    void Start()
    {
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

            rb.velocity = dir*speed;
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
