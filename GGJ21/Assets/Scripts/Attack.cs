using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    int damage = 5;

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyhealth = other.GetComponent<EnemyHealth>();
        if(enemyhealth)
        {
            enemyhealth.TakeDamage(damage);
        }
    }
}
