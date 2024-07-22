using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core;

namespace WarScene
{
    public class HeadHealthBar : MonoBehaviour
    {

        //---------------Nesneler---------------

        [SerializeField]
         Slider slider;
        [SerializeField]
         TMP_Text healthCounter;
        [NonSerialized]
         public float currentHealth, maxHealth;

        CharacterState characterState => GetComponentInParent<CharacterState>();


        //---------------Metotlar---------------

        void Start()
        {
            maxHealth = characterState.maxHealth;
            currentHealth = maxHealth;
        }

        void Update()
        {
            currentHealth = characterState.currentHealth;

            if (currentHealth < maxHealth)
                slider.gameObject.SetActive(true);
            else
                slider.gameObject.SetActive(false);

            float fillValue = currentHealth / maxHealth; // Slider için 0 ile 1 arasýnda deðer verir.
            slider.value = fillValue;

            healthCounter.text = (int)currentHealth + "/" + maxHealth;

            if (currentHealth == 0)
            {
                slider.gameObject.SetActive(false);
            }
        }
    }
}
