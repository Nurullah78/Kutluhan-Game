using UnityEngine;
using TMPro;
using WarScene;
using Core;

namespace StatusBar
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        CharacterState playerState;
        [SerializeField]
        TMP_Text lvlTag;
        [SerializeField]
        HeadHealthBar healthBar;

        int currentLevel = 1;

        void Start()
        {
            GetComponent<TMP_Text>().text = currentLevel.ToString();
            lvlTag.text = currentLevel.ToString() + ".";
        }

        void Update()
        {
            if (playerState.isLeveledUp)
            {
                currentLevel++;
                GetComponent<TMP_Text>().text = currentLevel.ToString();
                lvlTag.text = currentLevel.ToString() + ".";
                playerState.requiredExp *= currentLevel / 1.25f;
                playerState.maxHealth += 15;
                playerState.maxMana += 5;
                healthBar.maxHealth = playerState.maxHealth;
                playerState.GetComponent<Fighter>().defaultWeapon.weaponDamage *= currentLevel / 1.25f;
                playerState.isLeveledUp = false;
            }
        }
    }
}
