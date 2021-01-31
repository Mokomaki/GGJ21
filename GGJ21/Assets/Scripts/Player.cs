using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    enum BuffType
    {
        HP,
        DMG,
        SPEED
    }

    public static int Health = 100;
    public static float dash = 3;
    public static float combat = 0.5f;

    public static float damageMultiplier = 1.0f;
    public static float healthMultiplier = 1.0f;
    public static float speedMultiplier = 1.0f;

    Animator anim;
    

    [SerializeField]
    float speed = 4.5f;

    [SerializeField]
    GameObject attackObject;

    [SerializeField]
    float attackDuration = 0.4f;

    [SerializeField]
    GameObject blockObject;

    [SerializeField]
    float blockDuration = 0.2f;

    [SerializeField]
    float combatCooldownTime = 0.2f;

    [SerializeField]
    float dashCooldownTime = 3.0f;

    [SerializeField]
    float dashForce = 8.0f;

    [SerializeField]
    GameObject dashParticles;

    [SerializeField]
    GameObject hitParticles;

    static GameObject HitParticles;
    static Transform myTrans;

    float dashCooldown = 0;

    short ComboCount = 0;

    float comboCounterTime = 0;
    float comboCounterCooldown = 2;

    Camera mainCam;
    bool canMove = true;
    Rigidbody rb;
    float combatCooldown = 0;
    static bool isBlocking = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        HitParticles = hitParticles;
        myTrans = transform;
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;   
    }

    void Update()
    {

        MovePlayer();
        RotatePlayer();
        DisableCombo();
        if(CombatCooldown())
        {
            CombatAction();
        }
        Emote();
    }

    private void FixedUpdate()
    {
        CalculateDash();
        DashProgress.ReduceDash();
        CalculateCombat();
        CombatProgress.ReduceCombat();
    }

    void Emote()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("Emote", true);
            StartCoroutine(setAnimBoolOff("Emote", 0.2f));
        }
    }

    void CalculateDash()
    {
        dash = (dashCooldownTime - dashCooldown) * 33.33f;
    }
    void CalculateCombat()
    {
        combat = (combatCooldownTime - combatCooldown) * 190;
    }

    void DisableCombo()
    {
        if(ComboCount>2)
        {
            ComboCount = 0;
        }
        if(comboCounterTime>0)
        {
            comboCounterTime -= Time.deltaTime;
        }
        else
        {
            ComboCount = 0;
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

    public static void TakeDamage(int DMG)
    {
        if(!isBlocking)
        {
            Health -= DMG;
            HealthProgress.ReduceHealth();
            Destroy(Instantiate(HitParticles, myTrans.position, myTrans.rotation), 2);
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
        switch(ComboCount)
        {
            case 0:
                anim.SetBool("Hit1", true);
                StartCoroutine(setAnimBoolOff("Hit1", attackDuration));
                break;

            case 1:
                anim.SetBool("Hit2", true);
                StartCoroutine(setAnimBoolOff("Hit2", attackDuration));
                break;

            case 2:
                anim.SetBool("Hit3", true);
                StartCoroutine(setAnimBoolOff("Hit3", attackDuration));
                break;
        }
        combatCooldown = combatCooldownTime;
        Destroy(Instantiate(attackObject, transform.position, transform.rotation), attackDuration);

    }
    void Block()
    {
        anim.SetBool("Block", true);
        StartCoroutine(setAnimBoolOff("Block", blockDuration));
        isBlocking = true;
        StartCoroutine(StopBlock(blockDuration));
        combatCooldown = combatCooldownTime;
        Destroy(Instantiate(blockObject, transform), blockDuration);
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
                anim.SetBool("Dash", true);
                StartCoroutine(setAnimBoolOff("Dash", 0.2f));
                Dash();
                return;
            }
            else
            {
                rb.velocity = dir*speed*speedMultiplier;
            }
            if(dir.magnitude>0)
            {
                anim.SetBool("Walking", true);
            }else
            {
                anim.SetBool("Walking", false);
            }
        }
    }
        
    void Dash()
    {
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
        dashCooldown = dashCooldownTime;
        Destroy(Instantiate(dashParticles, transform.position, transform.rotation), 1f);
        Buff(0.5f, BuffType.DMG, 1);
        Buff(0.5f, BuffType.SPEED, 0.5f);
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

    public void AttackHit()
    {
        if(ComboCount<3)
        {
            ComboCount++;
            comboCounterTime = comboCounterCooldown;
        }
        Buff(0.5f, BuffType.DMG, 1);
    }

    public void AttackMissed()
    {
        ComboCount = 0;
        comboCounterTime = 0;
    }


     IEnumerator setAnimBoolOff(string boolName, float t)
    {
        yield return new WaitForSeconds(t);
        anim.SetBool(boolName, false);
    }

    IEnumerator StopBlock(float t)
    {
        yield return new WaitForSeconds(t);
        isBlocking = false;
    }

    IEnumerator Buff(float duration, BuffType bType, float Amount)
    {
        switch(bType)
        {
            case BuffType.DMG:
                damageMultiplier += Amount;
                yield return new WaitForSeconds(duration);
                damageMultiplier -= Amount;
                break;
            case BuffType.HP:
                healthMultiplier += Amount;
                yield return new WaitForSeconds(duration);
                healthMultiplier -= Amount;
                break;
            case BuffType.SPEED:
                speedMultiplier += Amount;
                yield return new WaitForSeconds(duration);
                speedMultiplier -= Amount;
                break;
        }
    }


}
