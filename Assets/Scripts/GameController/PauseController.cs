using SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameController
{
    public class PauseController : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField] UnityEvent GamePaused, GameResumed;
        [SerializeField] GameObject gamePausedScreen, pausedUI;
        [SerializeField] Image pauseButton;

        GameObject portalController => GameObject.FindGameObjectWithTag("PortalController");

        bool _isPaused; //Oyunun durup durmadığını tutar.


//---------------Metotlar---------------

        public void GamePause()
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                Time.timeScale = 0; //Zamanı durdurarak oyunun donmasını sağlar.
                GamePaused.Invoke();
                gamePausedScreen.gameObject.SetActive(true);
                pausedUI.gameObject.SetActive(false);
                pauseButton.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 1; //Zamanı tekrar başlatarak oyunun devam etmesini sağlar sağlar.
                GameResumed.Invoke();
                gamePausedScreen.gameObject.SetActive(false);
                pausedUI.gameObject.SetActive(true);
                pauseButton.gameObject.SetActive(true);
            }
        }

        public void LoadMainMenu()
        {
            foreach (var core in portalController.GetComponent<PortalController>()._core)
            {

                Destroy(core);
            }
            foreach (var player in portalController.GetComponent<PortalController>()._player)
            {
                Destroy(player);
            }
            SceneManager.LoadScene(0);
            foreach (var controllers in portalController.GetComponent<PortalController>()._controllers)
            {
                Destroy(controllers);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GamePause();
            }
        }
    }
}
