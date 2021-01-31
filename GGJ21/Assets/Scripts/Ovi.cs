using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovi : MonoBehaviour
{
    [SerializeField]
    GameObject[] toEnable;

    [SerializeField]
    Animator door1;
    [SerializeField]
    Animator door2;

    void Start()
    {
        door1.speed = 0;
        door2.speed = 0;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            for(int i = 0; i<toEnable.Length;i++)
            {
                toEnable[i].SetActive(true);
            }
            door1.speed = 1;
            door2.speed = 1;
        }
    }
}
