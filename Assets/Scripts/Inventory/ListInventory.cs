using System.Collections.Generic;
using UnityEngine;
using CreateMenu;

namespace Inventory
{
    public class ListInventory : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField] InventoryManager inManager;
        
        List<CollectItem> items = new List<CollectItem>();
        

//---------------Metotlar---------------

        void Start()
        {
            items = inManager.items;
        }

        void Update()
        {
            inManager.ListItems();
        }
    }
}
