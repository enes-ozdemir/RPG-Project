using System.Linq;
using UnityEngine;

namespace _Scripts.Inventory_System.Base
{
    public abstract class ItemContainer : MonoBehaviour, IItemContainer
    {
        [SerializeField] protected PlayerItemSlot[] itemSlots;

        public bool AddItem(Item item)
        {
            Debug.Log(item + " Added to the inventory");
            foreach (var slot in itemSlots)
            {
                if (slot.Item == null || slot.CanAddStack(item))
                {
                    slot.Item = item;
                    slot.Amount++;
                    return true;
                }
            }

            return false;
        }

        public bool RemoveItem(Item item)
        {
            foreach (var slot in itemSlots)
            {
                if (slot.Item == item)
                {
                    slot.Amount--;
                    return true;
                }
            }

            return false;
        }

        public Item RemoveItem(string itemID)
        {
            foreach (var slot in itemSlots)
            {
                var item = slot.Item;
                if (item != null && item.ID == itemID)
                {
                    slot.Amount--;
                    return item;
                }
            }

            return null;
        }

        public bool IsFull() => itemSlots.All(slot => slot.Item != null);

        public int ItemCount(string itemID) => itemSlots.Count(slot => slot.Item.ID == itemID);
    }
}