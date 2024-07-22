using System.Collections;
using TMPro;
using UnityEngine;

namespace SceneManagement
{
    public class Fader : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField]
         TMP_Text loadingText;

        CanvasGroup _canvasGroup => GetComponent<CanvasGroup>();


//---------------Metotlar---------------

        public IEnumerator FadeOut(float time)
        {
            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }

            loadingText.gameObject.SetActive(true);
        }
        
        public IEnumerator FadeIn(float time)
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }

            loadingText.gameObject.SetActive(false);
        }
    }
}
