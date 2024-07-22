using TMPro;
using UnityEngine;

namespace StatusBar
{
    public class CharacterName : MonoBehaviour
    {
        [SerializeField] TMP_Text characterName;

        void Start()
        {
            GetComponent<TMP_Text>().text = characterName.text;
            Debug.Log(GetComponent<TMP_Text>().textBounds);
        }
    }
}
