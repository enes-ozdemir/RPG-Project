using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Inventory_System.Base
{
    public abstract class ItemContainer : MonoBehaviour, IItemContainer
    {
        //  [SerializeField] protected PlayerItemSlot[] itemSlots;
        [SerializeField] protected PlayerItemSlot playerItemSlot;
        [SerializeField] protected GameObject cellSlot;
        protected List<BaseItemSlot> itemSlots = new();

        [SerializeField] protected Vector2Int containerSize;
        protected int slotCount;
        public int cellSize;


        private void Awake()
        {
            slotCount = containerSize.x * containerSize.y;
        }

        private bool IsSlotAvailable(int index)
        {
            if (index < 0 || index >= itemSlots.Count) return false;
            if (itemSlots[index].isOccupied) return false;
            return itemSlots[index].Item == null;
        }

   public bool AddItem(Item item)
{
    for (int itemIndex = 0; itemIndex < itemSlots.Count; itemIndex++)
    {
        if (!IsSlotAvailable(itemIndex))
        {
            continue;
        }

        int itemWIndex = item.width;
        int itemH = item.height;

        if (itemWIndex <= 0 || itemH <= 0)
        {
            Debug.LogError("Item width or height is less than 0");
            return false;
        }

        var widthOccupiedIndices = new List<int>();
        var heightOccupiedIndices = new List<int>();
        if (!IsWidthAvailable(itemWIndex, itemIndex, out widthOccupiedIndices) || !IsHeightAvailable(itemH, itemIndex, out heightOccupiedIndices))
        {
            continue;
        }

        try
        {
            itemSlots[itemIndex].Item = item;
            itemSlots[itemIndex].isOccupied = true;
            
            foreach (var index in widthOccupiedIndices)
            {
                itemSlots[index].isOccupied = true;
            }
            foreach (var index in heightOccupiedIndices)
            {
                itemSlots[index].isOccupied = true;
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + " " + e.ToString());
        }
    }

    return false;
}

private bool IsHeightAvailable(int itemHeight, int itemIndex, out List<int> availableIndex)
{
    availableIndex = new List<int>();
    int currentRowIndex = itemIndex / containerSize.x;
    for (int i = 1; i < itemHeight; i++)
    {
        var newRow = currentRowIndex + i;
        if (newRow >= containerSize.y) return false;

        var newIndex = newRow * containerSize.x + (itemIndex % containerSize.x);
        if (!IsSlotAvailable(newIndex))
        {
            return false;
        }
        availableIndex.Add(newIndex);
    }
    return true;
}

private bool IsWidthAvailable(int itemWidth, int itemIndex, out List<int> availableIndex)
{
    availableIndex = new List<int>();
    int currentColumnIndex = itemIndex % containerSize.x;
    for (int i = 1; i < itemWidth; i++)
    {
        var newColumn = currentColumnIndex + i;
        if (newColumn >= containerSize.x) return false;

        var newIndex = itemIndex + i;
        if (!IsSlotAvailable(newIndex))
        {
            return false;
        }
        availableIndex.Add(newIndex);
    }
    return true;
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