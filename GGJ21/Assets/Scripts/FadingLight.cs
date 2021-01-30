using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingLight : MonoBehaviour
{
    [SerializeField]
    float Speed = 5f;

    Light m_light;

    void Start()
    {
        m_light = GetComponent<Light>();
    }

    void Update()
    {
        if(m_light.intensity>0)
        {
            m_light.intensity -= Speed * Time.deltaTime;
        }
    }
}
