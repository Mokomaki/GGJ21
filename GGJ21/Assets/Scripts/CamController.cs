using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField]
    Vector3 offset;

    [SerializeField]
    Transform follow;

    [SerializeField]
    float smoothing = 0.124789f;
    [SerializeField]
    float trigger = 1;

    Vector3 target;
    Vector3 lastPos;
    Vector3 velocity;
    void Start()
    {
        lastPos = transform.position;
        velocity = Vector3.zero;
    }

    void Update()
    {
        
        target = follow.position + offset;
        
        if(Vector3.Distance((transform.position-offset),follow.position)>trigger)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothing);
        }

        velocity = (transform.position - lastPos).normalized;
        lastPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer ms = other.GetComponent<MeshRenderer>();
        if(ms)
        {
            ms.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        MeshRenderer ms = other.GetComponent<MeshRenderer>();
        if (ms)
        {
            ms.enabled = true;
        }
    }
}