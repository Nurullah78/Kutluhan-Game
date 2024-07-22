using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace MainMenu
{
    public class MenuControls : MonoBehaviour
    {
        [SerializeField]
         GameObject menuSlider, anywhereButton, anywhereText, loadingGame, nameMenu;
        [SerializeField]
         AudioSource menuMusic;

        public string characterName;


        void Start()
        {
            DontDestroyOnLoad(gameObject);

            nameMenu.GetComponentInChildren<TMP_InputField>().onEndEdit.AddListener(GetName);
        }

        public void SlideMenu()
        {
            menuSlider.GetComponent<Animation>().Play("MenuSlide");
            anywhereButton.SetActive(false);
            anywhereText.SetActive(false);
        }

        public void NameMenu()
        {
            nameMenu.SetActive(true);
        }

        void GetName(string input)
        {
            characterName = input;
        }

        public void NewGame()
        {
            menuMusic.Stop();
            loadingGame.SetActive(true);
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
