using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatProgress : MonoBehaviour
{
    static RectTransform combatRect;

    private void Start()
    {
        combatRect = GetComponent<RectTransform>();
    }



    public static void ReduceCombat()
    {
        float newWidth = Player.combat / 100;
        combatRect.localScale = new Vector3(newWidth, 1, 1);
    }
}
