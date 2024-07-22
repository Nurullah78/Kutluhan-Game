using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core;

namespace StatusBar
{
    public class ManaBar : MonoBehaviour
    {

//---------------Nesneler---------------

        public TMP_Text manaCounter;
        public CharacterState characterState;

        Slider slider;
        
        float currentMana, maxMana;
        

//---------------Metotlar---------------

        void Awake()
        {
            slider = GetComponent<Slider>();
        }

        void Update()
        {
            currentMana = characterState.GetComponent<CharacterState>().currentMana;
            maxMana = characterState.GetComponent<CharacterState>().maxMana;

            float fillValue = currentMana / maxMana; // Slider için 0 ile 1 arasında değer verir.
            slider.value = fillValue;

            manaCounter.text = (int)currentMana + "/" + maxMana;
        }
    }
}
