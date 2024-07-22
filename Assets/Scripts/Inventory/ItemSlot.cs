using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        
//---------------Nesneler---------------

        public GameObject Item
        {
            get
            {
                if (transform.childCount > 0)
                {
                    return transform.GetChild(0).gameObject;
                }

                return null;
            }
        }


//---------------Metotlar---------------

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop");

            //if there is not item already then set our item.
            if (!Item)
            {
                DragDrop.ItemBeingDragged.transform.SetParent(transform);
                DragDrop.ItemBeingDragged.transform.localPosition = new Vector2(0, 0);
            }
        }
    }
}
