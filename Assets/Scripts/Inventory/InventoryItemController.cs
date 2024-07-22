using UnityEngine;
using UnityEngine.UI;
using CreateMenu;

namespace Inventory
{
    public class InventoryItemController : MonoBehaviour
    {

//---------------Nesneler---------------

        CollectItem _item;

        public Button RemoveButton;


//---------------Metotlar---------------

        //Envanterde bulunan itemin kaldırılmasını sağlar. Item objesine bağlıdır.
        public void RemoveItem()
        {
            InventoryManager.Instance.Remove(_item);

            Destroy(gameObject);
        }

        public void AddItem(CollectItem newItem)
        {
            _item = newItem;
        }
    }
}
