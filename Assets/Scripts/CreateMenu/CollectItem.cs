using UnityEngine;

namespace CreateMenu
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]

    public class CollectItem : ScriptableObject
    {
        
//---------------Nesneler---------------

        public enum ItemType
        {
            Bracer,
            Shield,
            Sword
        }
        
        public int id;
        public string itemName;
        public int value;
        public Sprite icon;
        public ItemType itemType;
    }
}
