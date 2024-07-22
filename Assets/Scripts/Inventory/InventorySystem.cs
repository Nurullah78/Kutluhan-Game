using System.Collections.Generic;
using UnityEngine;
using CreateMenu;

namespace Inventory
{
    public class InventorySystem : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField] InventoryManager inManager;
        
        List<CollectItem> items = new List<CollectItem>();
        
        static InventorySystem Instance { get; set; }
        
        public GameObject inventoryScreenUI;

        bool _isOpen; //Envanter objesinin SetActive özelliğini tutar.


//---------------Metotlar---------------

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        
        void Start()
        {
            items = inManager.items;
        }

        public void InventoryOpen()
        {
            _isOpen = true;
            inventoryScreenUI.SetActive(_isOpen);
            
            inManager.ListItems();
        }

        public void InventoryClose()
        {
            _isOpen = false;
            inventoryScreenUI.SetActive(_isOpen);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!_isOpen)
                {
                    InventoryOpen();
                }
                else
                {
                    InventoryClose();
                }
            }
        }
    }
}
