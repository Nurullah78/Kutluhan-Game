using UnityEngine;
using UnityEngine.UI;

namespace StatusBar
{
    public class EnergyBar : MonoBehaviour
    {

//---------------Nesneler---------------

        //public TMP_Text energyCounter;
        //public CharacterState characterState;
        
        Slider slider;

        //float currentEnergy, maxEnergy;
        

//---------------Metotlar---------------

        void Awake()
        {
            slider = GetComponent<Slider>();
        }

        //void Update()
        //{
        //    currentEnergy = characterState.GetComponent<CharacterState>().currentEnergy;
        //    maxEnergy = characterState.GetComponent<CharacterState>().maxEnergy;

        //    float fillValue = currentEnergy / maxEnergy; // Slider için 0 ile 1 arasında değer verir.
        //    slider.value = fillValue;

        //    energyCounter.text = (int)currentEnergy + "/" + maxEnergy;
        //}
    }
}
