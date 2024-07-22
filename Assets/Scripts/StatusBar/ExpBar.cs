using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core;

namespace StatusBar
{
    public class ExpBar : MonoBehaviour
    {

//---------------Nesneler---------------

        public TMP_Text expCounter;
        public CharacterState characterState;

        Slider slider;
        
        float currentExp, requiredExp;
        

//---------------Metotlar---------------

        void Awake()
        {
            slider = GetComponent<Slider>();
        }

        void Update()
        {
            currentExp = characterState.GetComponent<CharacterState>().currentExp;
            requiredExp = characterState.GetComponent<CharacterState>().requiredExp;

            float fillValue = currentExp / requiredExp; // Slider için 0 ile 1 arasında değer verir.
            slider.value = fillValue;

            expCounter.text = (int)currentExp + "/" + requiredExp;
        }
    }
}
