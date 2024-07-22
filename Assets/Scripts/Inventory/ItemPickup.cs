using UnityEngine;
using CreateMenu;

namespace Inventory
{
    public class ItemPickup : MonoBehaviour
    {

//---------------Nesneler---------------

        public CollectItem Item;


//---------------Metotlar---------------

        void Pickup()
        {
            InventoryManager.Instance.Add(Item);
            Destroy(gameObject);
        }

        void OnMouseDown()
        {
            Pickup();
        }
    }
}
