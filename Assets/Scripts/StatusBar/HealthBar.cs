using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core;

namespace StatusBar
{
    public class HealthBar : MonoBehaviour
    {

//---------------Nesneler---------------

        public TMP_Text healthCounter;
        public CharacterState characterState;
        [NonSerialized] public float currentHealth;

        Slider slider;
        
        float maxHealth;
        

//---------------Metotlar---------------

        void Awake()
        {
            slider = GetComponent<Slider>();
        }

        void Update()
        {
            currentHealth = characterState.GetComponent<CharacterState>().currentHealth;
            maxHealth = characterState.GetComponent<CharacterState>().maxHealth;

            float fillValue = currentHealth / maxHealth; // Slider için 0 ile 1 arasında değer verir.
            slider.value = fillValue;

            healthCounter.text = (int)currentHealth + "/" + maxHealth;
        }
    }
}
