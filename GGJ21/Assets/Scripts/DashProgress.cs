using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashProgress : MonoBehaviour
{
    static RectTransform dashRect;

    private void Start()
    {
        dashRect = GetComponent<RectTransform>();
    }



    public static void ReduceDash()
    {
        float newWidth =  Player.dash/ 100;
        dashRect.localScale = new Vector3(newWidth, 1, 1);
    }
}
