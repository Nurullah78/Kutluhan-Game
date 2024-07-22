using UnityEngine;
using TMPro;
using MainMenu;

public class GetCharacterName : MonoBehaviour
{
    MenuControls menuController => FindObjectOfType<MenuControls>();

    TMP_Text nameText => GetComponent<TMP_Text>();

    void Awake()
    {
        nameText.text = menuController.characterName;

        Destroy(menuController.gameObject);
    }
}
