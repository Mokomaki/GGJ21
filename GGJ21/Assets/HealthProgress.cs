using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthProgress : MonoBehaviour
{
    static RectTransform hpRect;
    
    private void Start()
    {
        hpRect = GetComponent<RectTransform>();
    }



    public static void ReduceHealth()
    {
        float fHealth = Player.Health;
        float newWidth = fHealth/100;
        hpRect.localScale = new Vector3(newWidth, 1, 1);
    }
}
