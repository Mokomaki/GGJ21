using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int health = 15;

    bool die = false;

    [SerializeField]
    GameObject deathParticles;
    [SerializeField]
    GameObject hitParticles;

    private void Update()
    {
        if(die)
        {
            transform.position = transform.position + new Vector3(0, 0.003f, 0.005f);
            transform.Rotate(5, 0, 0);
        }
    }

    public void TakeDamage(int dmg)
    {
        Destroy(Instantiate(deathParticles, transform.position, transform.rotation), 1.5f);
        health -= dmg;
        if(health<=0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(Instantiate(deathParticles, transform.position, transform.rotation), 1.5f);
        Destroy(gameObject);
    }
}