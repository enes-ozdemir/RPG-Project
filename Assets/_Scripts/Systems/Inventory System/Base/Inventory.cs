using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Inventory_System.Base
{
    public class Inventory : ItemContainer
    {
        [SerializeField] private List<Item> startingItems;
        [SerializeField] private Transform itemParent;
        [SerializeField] private Transform cellParent;
        [SerializeField] private GridLayoutGroup gridLayout;
        [SerializeField] private GridLayoutGroup cellLayout;

        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;
        public event Action<BaseItemSlot> OnShiftRightClickEvent;
        public event Action<BaseItemSlot> onBuyEvent;

        private void InitializeInventory()
        {
            SetCellSize();
            
            CreateInventorySlots();
            CreateCells();

            SetStartingItems();
        }

        private void CreateCells()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var slot = Instantiate(cellSlot, cellParent);
                }
            }
        }

        private void CreateInventorySlots()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var slot = Instantiate(playerItemSlot, itemParent);
                    itemSlots[i, j] = slot;
                    AddEventsForInventorySlots(slot);
                }
            }
        }

        private void SetCellSize()
        {
            gridLayout.cellSize = new Vector2(50, 50);
            cellLayout.cellSize = new Vector2(50, 50);
        }

        private void AddEventsForInventorySlots(PlayerItemSlot slot)
        {
            slot.OnPointerEnterEvent += OnPointerEnterEvent;
            slot.OnPointerExitEvent += OnPointerExitEvent;
            slot.OnBeginDragEvent += OnBeginDragEvent;
            slot.OnEndDragEvent += OnEndDragEvent;
            slot.OnDropEvent += OnDropEvent;
            slot.OnDragEvent += OnDragEvent;
            slot.OnShiftRightClickEvent += OnShiftRightClickEvent;
        }

        private void Start()
        {
            InitializeInventory();
        }

        private void SetStartingItems()
        {
            int i = 0;
            for (; i < startingItems.Count && i < itemSlots.Length; i++)
            {
                AddItem(startingItems[i].GetCopy());
            }

        }

        public bool ContainsItem(Item item)
        {
            return itemSlots.Cast<PlayerItemSlot>().Any(t => t.Item == item);
        }

    }
}