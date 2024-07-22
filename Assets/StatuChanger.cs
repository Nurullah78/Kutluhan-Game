using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatuChanger : MonoBehaviour
{
    [SerializeField]
     CharacterState player;

    [NonSerialized]
    public int healthPotionCount = 0, manaPotionCount = 0;

    public void ChangeStatu()
    {
        if (gameObject.name == "HealthButton")
        {
            if (healthPotionCount > 0)
            {
                healthPotionCount--;
                GetComponentInChildren<TMP_Text>().text = healthPotionCount.ToString();
                player.currentHealth += 50;
            }
        }
        
        if (gameObject.name == "ManaButton")
        {
            if (manaPotionCount > 0)
            {
                manaPotionCount--;
                GetComponentInChildren<TMP_Text>().text = manaPotionCount.ToString();
                player.currentHealth += 35;
            }
        }
    }
}
