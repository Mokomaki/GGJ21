using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    int health = 15;

    [SerializeField]
    GameObject deathParticles;
    [SerializeField]
    GameObject hitParticles;

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
        if(transform.CompareTag("Boss"))
        {
            SceneManager.LoadScene(1);
        }
        Destroy(gameObject);
    }
}