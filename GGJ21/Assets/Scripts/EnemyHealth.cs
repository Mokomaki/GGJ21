using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int health = 15;

    bool die = false;

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
        health -= dmg;
        if(health<=0)
        {
            Die();
        }
        Debug.Log("Hei pojat, tyt�t, napanallet ja vesikarhut! Otin juuri " + dmg + " damagea.");
    }

    void Die()
    {
        Debug.Log("Voi ei! Kuolin! T�M�N VUOKSI MINUA KUTSUTAAN LENT�V�KSI SIRKKELIKSI... HOPEANUOLIIII!!!!!!");
        StartCoroutine(diuee());

    }

    IEnumerator diuee ()
    {
        yield return new WaitForSeconds(2);
        die = true;
    }
}
