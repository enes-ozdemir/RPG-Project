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
        public BaseItemSlot[,] itemSlots;

        [SerializeField] protected GameObject cellSlot;

        // protected List<BaseItemSlot> itemSlots = new();
        [SerializeField] protected int width;
        [SerializeField] protected int height;
        protected int slotCount;

        private void Awake()
        {
            Debug.Log("Initializing Inventory with width: " + width + " and height: " + height);
            this.width = width;
            this.height = height;
            itemSlots = new BaseItemSlot[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    itemSlots[i, j] = new BaseItemSlot
                    {
                        position = new Vector2Int(i, j)
                    };
                }
            }

            Debug.Log("Inventory initialized.");
        }

        public ItemContainer()
        {
            // Debug.Log("Initializing Inventory with width: " + width + " and height: " + height);
            itemSlots = new BaseItemSlot[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    itemSlots[i, j] = new BaseItemSlot
                    {
                        position = new Vector2Int(i, j)
                    };
                }
            }

            // Debug.Log("Inventory initialized.");
        }

        public bool AddItem(Item item)
        {
            Debug.Log("Adding item: " + item.name + " to inventory.");

            for (int x = 0; x <= width - item.width; x++)
            {
                for (int y = 0; y <= height - item.height; y++)
                {
                    if (CanFit(item, new Vector2Int(x, y)))
                    {
                        PlaceItem(item, new Vector2Int(x, y));
                        Debug.Log("Item added successfully.");
                        return true;
                    }
                }
            }

            Debug.Log("Failed to add item. No suitable place found.");
            return false;
        }

        private bool CanFit(Item item, Vector2Int position)
        {
            Debug.Log("Checking if item: " + item.name + " can fit at position: " + position);

            int x = position.x;
            int y = position.y;

            for (int i = x; i < x + item.width; i++)
            {
                for (int j = y; j < y + item.height; j++)
                {
                    if (itemSlots[i, j].isOccupied)
                    {
                        Debug.Log("Item can't fit. Required slot is not free.");
                        return false;
                    }
                }
            }

            Debug.Log("Item can fit at given position.");
            return true;
        }

        private void PlaceItem(Item item, Vector2Int position)
        {
            Debug.Log("Placing item: " + item.name + " at position: " + position);

            int x = position.x;
            int y = position.y;

            for (int i = x; i < x + item.width; i++)
            {
                for (int j = y; j < y + item.height; j++)
                {
                    itemSlots[i, j].Item = item;
                    itemSlots[i, j].isOccupied = true;

                    if (i == x && j == y)
                    {
                        //slots[i, j].SetSprite(item.sprite);
                    }
                }
            }

            Debug.Log("Item placed successfully.");
        }

        public bool RemoveItem(Item item)
        {
            Debug.Log("Removing item: " + item.name + " from inventory.");
            bool itemFound = false;

            // Iterate over all slots in the inventory
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // Check if the current slot contains the item
                    if (itemSlots[i, j].isOccupied && itemSlots[i, j].Item == item)
                    {
                        // If the item is found, remove it
                        itemSlots[i, j].Item = null;
                        itemSlots[i, j].isOccupied = false;
                        itemFound = true;
                        Debug.Log("Item found and removed at position: (" + i + "," + j + ")");
                    }
                }
            }

            if (!itemFound)
            {
                Debug.Log("Item not found in inventory. Removal failed.");
                return false;
            }

            Debug.Log("Item removed successfully.");
            return true;
        }

        public int ItemCount(string itemID)
        {
            return 12;
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

        public bool IsEmpty()
        {
            Debug.Log("Checking if inventory is empty");
            return itemSlots.Cast<BaseItemSlot>().All(slot => !slot.isOccupied);
        }

        public bool IsFull()
        {
            Debug.Log("Checking if inventory is full");
            return itemSlots.Cast<BaseItemSlot>().All(slot => slot.isOccupied);
        }

        public int GetItemCount(Item item)
        {
            Debug.Log("Getting count of item: " + item.name);
            return itemSlots.Cast<BaseItemSlot>().Count(slot => slot.isOccupied && slot.Item == item);
        }
    }
}