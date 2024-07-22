using System.Collections.Generic;
using CreateMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {

//---------------Nesneler---------------

        public static InventoryManager Instance;
        public List<CollectItem> items = new List<CollectItem>();

        public Transform itemContent;
        public GameObject inventoryItem;
        public Toggle enableRemove;

        public InventoryItemController[] inventoryItems;


//---------------Metotlar---------------

        void Awake()
        {
            Instance = this;
        }

        public void Add(CollectItem item)
        {
            items.Add(item);
        }

        public void Remove(CollectItem item)
        {
            items.Remove(item);
        }

        public void SetInventoryItems()
        {
            inventoryItems = itemContent.GetComponentsInChildren<InventoryItemController>();

            for (int i = 0; i < items.Count; i++)
            {
                inventoryItems[i].AddItem(items[i]);
            }
        }

        public void ListItems()
        {
            // Yerleşmeden önce siler.
            foreach (Transform item in itemContent)
            {
                Destroy(item.gameObject);
            }

            foreach (var item in items)
            {
                GameObject obj = Instantiate(inventoryItem, itemContent);
                var itemName = obj.transform.Find("ItemAdı").GetComponent<TMP_Text>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

                itemName.text = item.itemName;
                itemIcon.sprite = item.icon;

                if (enableRemove.isOn)
                    removeButton.gameObject.SetActive(true);
            }

            SetInventoryItems();
        }

        public void EnableItemsRemove()
        {
            if (enableRemove.isOn)
            {
                foreach (Transform item in itemContent)
                {
                    item.Find("RemoveButton").gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (Transform item in itemContent)
                {
                    item.Find("RemoveButton").gameObject.SetActive(false);
                }
            }
        }
    }
}
