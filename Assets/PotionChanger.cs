using TMPro;
using UnityEngine;

public class PotionChanger : MonoBehaviour
{
    [SerializeField]
     GameObject healthButton, manaButton;

    public void PotionChange()
    {
        if (gameObject.name == "HealthPotion")
        {
            healthButton.GetComponent<StatuChanger>().healthPotionCount++;
            healthButton.GetComponentInChildren<TMP_Text>().text = healthButton.GetComponent<StatuChanger>().healthPotionCount.ToString();
        }

        if (gameObject.name == "ManaPotion")
        {
            manaButton.GetComponent<StatuChanger>().manaPotionCount++;
            manaButton.GetComponentInChildren<TMP_Text>().text = manaButton.GetComponent<StatuChanger>().manaPotionCount.ToString();
        }
    }
}
