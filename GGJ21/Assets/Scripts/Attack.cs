using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    Player p;

    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnDestroy()
    {
        if(!hit)
        {
            p.AttackMissed();
        }
    }

    [SerializeField]
    int damage = 5;

    bool hit = false;

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyhealth = other.GetComponent<EnemyHealth>();
        if(enemyhealth)
        {
            enemyhealth.TakeDamage((int)(damage*Player.damageMultiplier));
        }
        p.AttackHit();
        hit = true;
    }
}
