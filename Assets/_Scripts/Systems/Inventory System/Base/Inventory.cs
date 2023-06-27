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
            for (int i = 0; i < containerSize.x; i++)
            {
                for (int j = 0; j < containerSize.y; j++)
                {
                    var slot = Instantiate(cellSlot, cellParent);
                }
            }
        }

        private void CreateInventorySlots()
        {
            for (int i = 0; i < containerSize.x; i++)
            {
                for (int j = 0; j < containerSize.y; j++)
                {
                    var slot = Instantiate(playerItemSlot, itemParent);
                    itemSlots.Add(slot);
                    slot.cellSize = cellSize;
                    AddEventsForInventorySlots(slot);
                }
            }
        }

        private void SetCellSize()
        {
            gridLayout.cellSize = new Vector2(cellSize, cellSize);
            cellLayout.cellSize = new Vector2(cellSize, cellSize);
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
            for (; i < startingItems.Count && i < itemSlots.Count; i++)
            {
                AddItem(startingItems[i].GetCopy());
            }

            for (; i < itemSlots.Count; i++)
            {
                itemSlots[i].Item = null;
                itemSlots[i].Amount = 0;
            }
        }

        public bool ContainsItem(Item item)
        {
            return itemSlots.Any(slot => slot.Item == item);
        }
    }
}