using UnityEngine;
using UnityEngine.UI;

namespace Shopping
{
    public class PotionShop : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField] Image potionShop, potionShopButton, envanter;
        
        bool _isActive;
        

//---------------Metotlar---------------

        public void OpenShop()
        {
            _isActive = !_isActive;
            potionShop.gameObject.SetActive(_isActive);
            potionShopButton.gameObject.SetActive(false);
            //envanter.gameObject.SetActive(_isActive);
        }

        public void CloseShop()
        {
            _isActive = !_isActive;
            potionShop.gameObject.SetActive(_isActive);
        }

        void OnTriggerStay(Collider other)
        {
            potionShopButton.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenShop();
            }
        }

        void OnTriggerExit(Collider other)
        {
            potionShopButton.gameObject.SetActive(false);
        }
    }
}
