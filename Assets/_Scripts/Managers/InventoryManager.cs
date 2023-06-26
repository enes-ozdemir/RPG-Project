using System;
using _Scripts.Inventory_System;
using _Scripts.Inventory_System.Base;
using _Scripts.Inventory_System.Tooltip;
using Enca.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Logger = Enca.Debug.Logger;

namespace _Scripts.Managers
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private Inventory playerInventory;
        [SerializeField] private Image draggableItem;
        [SerializeField] private ItemTooltip itemTooltip;
        private BaseItemSlot _sourceSlot;
        private BaseItemSlot _targetSlot;
        private Logger _log;

        [Header("Colors")] public Color itemGhostColor = Color.gray.GetColorWithAlpha(0.8f);
        public Color itemBlockedColor = Color.red;

        public bool isTooltipActive;

        private void OnEnable() => SetInventoryEvents();

        private void SetInventoryEvents()
        {
            playerInventory.OnShiftRightClickEvent += DivideItems;
            playerInventory.OnPointerEnterEvent += ShowTooltip;
            playerInventory.OnPointerExitEvent += HideTooltip;
            playerInventory.OnBeginDragEvent += BeginDrag;
            playerInventory.OnEndDragEvent += EndDrag;
            playerInventory.OnDragEvent += Drag;
            playerInventory.OnDropEvent += Drop;
        }

        private void OnDisable()
        {
            playerInventory.OnShiftRightClickEvent -= DivideItems;
            playerInventory.OnPointerEnterEvent -= ShowTooltip;
            playerInventory.OnPointerExitEvent -= HideTooltip;
            playerInventory.OnBeginDragEvent -= BeginDrag;
            playerInventory.OnEndDragEvent -= EndDrag;
            playerInventory.OnDragEvent -= Drag;
            playerInventory.OnDropEvent -= Drop;
        }

        private void Start()
        {
            playerInventory.gameObject.SetActive(true);
            _log = new Logger("InventoryManager");
        }

        public void ToggleInventory() => playerInventory.gameObject.SetActive(!playerInventory.isActiveAndEnabled);

        public void CloseInventory() => playerInventory.gameObject.SetActive(false);

        private void ShowTooltip(BaseItemSlot itemSlot)
        {
            var item = itemSlot.Item;
            if (item != null)
            {
                _sourceSlot = itemSlot;
                isTooltipActive = true;
                _log.Info($"ShowTooltip Item name: {item.itemName}");
            }
        }

        private void Update()
        {
            if (isTooltipActive)
            {
                itemTooltip.ShowTooltip(_sourceSlot.Item);
            }
            else
            {
                itemTooltip.HideTooltip();
            }
        }

        private void HideTooltip(BaseItemSlot itemSlot)
        {
            isTooltipActive = false;
        }

        private void BeginDrag(BaseItemSlot itemSlot)
        {
            if (itemSlot.Item != null)
            {
                _sourceSlot = itemSlot;
                draggableItem.sprite = itemSlot.Item.itemIcon;
                draggableItem.transform.position = Input.mousePosition;
                draggableItem.enabled = true;
                // itemSlot.LightUpTheSlot(Color.green);
                _log.Info($"BeginDrag Item name: {itemSlot.Item.itemName}");
            }
            else
            {
                _log.Warning($"No Item on BeginDrag itemSlot: {itemSlot.name}");
            }
        }

        private void EndDrag(BaseItemSlot itemSlot)
        {
            _sourceSlot = null;
            draggableItem.enabled = false;
            //itemSlot.ResetSlotColor();

            _log.Info($"EndDrag Item slot: {itemSlot.name}");
        }

        private void Drag(BaseItemSlot itemSlot)
        {
            if (!draggableItem.enabled)
                return;

            draggableItem.transform.position = Input.mousePosition;

            if (itemSlot is PlayerItemSlot playerItemSlot)
            {
                var currentItem = playerItemSlot.Item;
                Color lightColor;
                if (playerItemSlot.CanReceiveItem(currentItem))
                    lightColor = itemGhostColor;
                else
                    lightColor = itemBlockedColor;
                itemSlot.LightUpTheSlot(lightColor);
            }

            _log.Info($"Drag Item name: {itemSlot.Item.itemName}");
        }

        private void Drop(BaseItemSlot dropItemSlot)
        {
            if (_sourceSlot == null)
            {
                _log.Warning($"Dropped into nothingness Item name: {dropItemSlot.Item.itemName}");

                return;
            }

            // var isDraggedBuyable = ((PlayerItemSlot) _draggedSlot).buyableSlot;
            // var isDroppedBuyable = ((PlayerItemSlot) dropItemSlot).buyableSlot;

            //    if (Input.GetKey(KeyCode.LeftShift) && !isDroppedBuyable && !isDraggedBuyable)
            if (Input.GetKey(KeyCode.LeftShift))
            {
                DivideItems(dropItemSlot);
            }
            else if (dropItemSlot.CanAddStack(_sourceSlot.Item))
            {
                AddStacks(dropItemSlot);
            }
            else if (dropItemSlot.CanReceiveItem(_sourceSlot.Item) && _sourceSlot.CanReceiveItem(dropItemSlot.Item))
            {
                PlaceItem(dropItemSlot);
            }

            _log.Info($"Drop Item name: {dropItemSlot.Item.itemName}");
        }

        private void PlaceItem(BaseItemSlot targetSlot)
        {
            var sourceItem = _sourceSlot.Item;
            int sourceItemQuantity = _sourceSlot.Amount;

            _sourceSlot.Item = targetSlot.Item;
            _sourceSlot.Amount = targetSlot.Amount;

            //   if (_sourceSlot.Item == null) _sourceSlot.DisableSlot();

            targetSlot.Item = sourceItem;
            targetSlot.Amount = sourceItemQuantity;

            _log.Info($"Slot Swapped: target: {targetSlot.name} source: {_sourceSlot.name}");
            if (targetSlot.Item != null && _sourceSlot.Item != null)
                _log.Info($"Item Swapped: target: {targetSlot.Item.name} source: {_sourceSlot.Item.name}");
        }

        // private void ShopBuy(BaseItemSlot dropItemSlot)
        // {
        //     if (draggedSlot == null) return;
        //
        //     Item draggedItem = draggedSlot.Item;
        //     int draggedItemAmount = draggedSlot.Amount;
        //
        //     for (int i = 0; i < draggedItemAmount; i++)
        //     {
        //         shopPanel.BuyItem(draggedItem);
        //     }
        // }
        //
        // private void ShopSell(BaseItemSlot dropItemSlot)
        // {
        //     if (draggedSlot == null) return;
        //
        //     Item draggedItem = draggedSlot.Item;
        //     int draggedItemAmount = draggedSlot.Amount;
        //
        //
        //     for (int i = 0; i < draggedItemAmount; i++)
        //     {
        //         shopPanel.SellItem(draggedItem);
        //     }
        // }

        private void SwapItems(BaseItemSlot targetSlot)
        {
            var draggedItem = _sourceSlot.Item;
            int draggedItemAmount = _sourceSlot.Amount;

            _sourceSlot.Item = targetSlot.Item;
            _sourceSlot.Amount = targetSlot.Amount;

            targetSlot.Item = draggedItem;
            targetSlot.Amount = draggedItemAmount;

            _log.Info($"SwapItems Item name: {targetSlot.Item.itemName}");
        }

        private void DivideItems(BaseItemSlot dropItemSlot)
        {
            //if (inventory.IsFull()) return;
            if (_sourceSlot.Amount == 1)
            {
                _log.Warning($"Amount : {_sourceSlot.Amount} cant be divided Item name: {dropItemSlot.Item.itemName}");
                return;
            }

            var draggedItem = _sourceSlot.Item;
            if (dropItemSlot.Item != null && _sourceSlot.Item != dropItemSlot.Item)
            {
                return;
            }

            int draggedItemAmount;


            //Handle if item can be directly divided to 2
            if (_sourceSlot.Amount % 2 == 0)
            {
                draggedItemAmount = _sourceSlot.Amount / 2;
                _sourceSlot.Amount = draggedItemAmount;
            }
            else
            {
                draggedItemAmount = (_sourceSlot.Amount - 1) / 2;
                _sourceSlot.Amount = draggedItemAmount;
                dropItemSlot.Amount++;
            }

            _sourceSlot.Item = draggedItem;
            dropItemSlot.Item = draggedItem;

            if (_sourceSlot.Item == dropItemSlot.Item)
            {
                draggedItemAmount += dropItemSlot.Amount;
                if (draggedItemAmount <= dropItemSlot.Item.maximumStacks)
                {
                    dropItemSlot.Amount = draggedItemAmount;
                }
                else
                {
                    int tempAmount = _sourceSlot.Amount;
                    _sourceSlot.Amount =
                        (_sourceSlot.Amount * 2 - (dropItemSlot.Item.maximumStacks - dropItemSlot.Amount));
                    dropItemSlot.Amount = dropItemSlot.Item.maximumStacks;
                }
            }
            else dropItemSlot.Amount = draggedItemAmount;

            _log.Info($"DivideItems Item name: {dropItemSlot.Item.itemName}");
        }

        private void AddStacks(BaseItemSlot dropItemSlot)
        {
            int numAddableStacks = dropItemSlot.Item.maximumStacks - dropItemSlot.Amount;
            int stacksToAdd = Mathf.Min(numAddableStacks, _sourceSlot.Amount);

            dropItemSlot.Amount += stacksToAdd;
            _sourceSlot.Amount -= stacksToAdd;

            _log.Info($"AddStacks Item name: {dropItemSlot.Item.itemName}");
        }

        public void RemoveStack(BaseItemSlot currentItemSlot)
        {
            if (currentItemSlot.Item != null)
            {
                currentItemSlot.Amount--;
                _log.Info($"RemoveStack Item name: {currentItemSlot.Item.itemName}");
            }
        }
    }
}