using System;
using _Scripts.Inventory_System;
using _Scripts.Inventory_System.Base;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Managers
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private Inventory playerInventory;
        [SerializeField] private Image draggableItem;
        [SerializeField] ItemTooltip itemTooltip;
        private BaseItemSlot _draggedSlot;

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
        }

        public void ToggleInventory() => playerInventory.gameObject.SetActive(!playerInventory.isActiveAndEnabled);

        public void CloseInventory() => playerInventory.gameObject.SetActive(false);

        // public void DisplayShop()
        // {
        //     if (_isShopActive)
        //     {
        //         shopPanel.SetActive(false);
        //         _isShopActive = false;
        //         playerManager.isPlayerNotBlocked = true;
        //     }
        //     else
        //     {
        //         shopPanel.SetActive(true);
        //         _isShopActive = true;
        //         playerManager.isPlayerNotBlocked = false;
        //     }
        // }
        private void ShowTooltip(BaseItemSlot itemSlot)
        {
            var equippableItem = itemSlot.Item;
            if (equippableItem != null)
            {
                itemTooltip.ShowTooltip(equippableItem);
            }
        }

        private void HideTooltip(BaseItemSlot itemSlot) => itemTooltip.HideTooltip();

        private void BeginDrag(BaseItemSlot itemSlot)
        {
            if (itemSlot.Item != null)
            {
                _draggedSlot = itemSlot;
                draggableItem.sprite = itemSlot.Item.itemIcon;
                draggableItem.transform.position = Input.mousePosition;
                draggableItem.enabled = true;
                itemSlot.LightUpTheSlot(Color.green);
            }
        }

        private void EndDrag(BaseItemSlot itemSlot)
        {
            _draggedSlot = null;
            draggableItem.enabled = false;
            itemSlot.ResetSlotColor();
        }

        private void Drag(BaseItemSlot itemSlot)
        {
            if (!draggableItem.enabled)
                return;

            draggableItem.transform.position = Input.mousePosition;

            if (itemSlot is PlayerItemSlot playerItemSlot)
            {
                var currentItem = playerItemSlot.GetItemFromSlot();
                Color lightColor;
                if (playerItemSlot.CanReceiveItem(currentItem))
                    lightColor = Color.green;
                else
                    lightColor = Color.red;
                itemSlot.LightUpTheSlot(lightColor);
            }
        }

        private void Drop(BaseItemSlot dropItemSlot)
        {
            if (_draggedSlot == null) return;

            // var isDraggedBuyable = ((PlayerItemSlot) _draggedSlot).buyableSlot;
            // var isDroppedBuyable = ((PlayerItemSlot) dropItemSlot).buyableSlot;

        //    if (Input.GetKey(KeyCode.LeftShift) && !isDroppedBuyable && !isDraggedBuyable)
            if (Input.GetKey(KeyCode.LeftShift))
            {
                DivideItems(dropItemSlot);
            }
            // else if (dropItemSlot.CanAddStack(draggedSlot.Item))
            // {
            //     if (isDraggedBuyable && !isDroppedBuyable)
            //     {
            //         ShopBuy(draggedSlot);
            //     }
            //     else if (isDroppedBuyable && !isDraggedBuyable)
            //     {
            //         ShopSell(draggedSlot);
            //     }
            //
            //     AddStacks(dropItemSlot);
            // }
            // else if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
            // {
            //     if (isDraggedBuyable && !isDroppedBuyable && dropItemSlot.Item == null)
            //     {
            //         ShopBuy(dropItemSlot);
            //         SwapItems(dropItemSlot);
            //     }
            //     else if (!isDraggedBuyable && isDroppedBuyable && dropItemSlot.Item == null)
            //     {
            //         ShopSell(dropItemSlot);
            //         SwapItems(dropItemSlot);
            //     }
            //     else if (!isDraggedBuyable && !isDroppedBuyable)
            //     {
            //         SwapItems(dropItemSlot);
            //     }
            // }
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

        private void SwapItems(BaseItemSlot dropItemSlot)
        {
            var draggedItem = _draggedSlot.Item;
            int draggedItemAmount = _draggedSlot.Amount;

            _draggedSlot.Item = dropItemSlot.Item;
            _draggedSlot.Amount = dropItemSlot.Amount;

            dropItemSlot.Item = draggedItem;
            dropItemSlot.Amount = draggedItemAmount;
        }

        private void DivideItems(BaseItemSlot dropItemSlot)
        {
            //if (inventory.IsFull()) return;
            if (_draggedSlot.Amount == 1) return;
            Item draggedItem = _draggedSlot.Item;
            if (dropItemSlot.Item != null && _draggedSlot.Item != dropItemSlot.Item)
            {
                Debug.Log("Dividing Items");
                return;
            }

            int draggedItemAmount;


            //Handle if item can be directly divided to 2
            if (_draggedSlot.Amount % 2 == 0)
            {
                draggedItemAmount = _draggedSlot.Amount / 2;
                _draggedSlot.Amount = draggedItemAmount;
            }
            else
            {
                draggedItemAmount = (_draggedSlot.Amount - 1) / 2;
                _draggedSlot.Amount = draggedItemAmount;
                dropItemSlot.Amount++;
            }

            _draggedSlot.Item = draggedItem;
            dropItemSlot.Item = draggedItem;

            if (_draggedSlot.Item == dropItemSlot.Item)
            {
                draggedItemAmount += dropItemSlot.Amount;
                if (draggedItemAmount <= dropItemSlot.Item.maximumStacks)
                {
                    dropItemSlot.Amount = draggedItemAmount;
                }
                else
                {
                    int tempAmount = _draggedSlot.Amount;
                    _draggedSlot.Amount =
                        (_draggedSlot.Amount * 2 - (dropItemSlot.Item.maximumStacks - dropItemSlot.Amount));
                    dropItemSlot.Amount = dropItemSlot.Item.maximumStacks;
                }
            }
            else dropItemSlot.Amount = draggedItemAmount;
        }

        private void AddStacks(BaseItemSlot dropItemSlot)
        {
            int numAddableStacks = dropItemSlot.Item.maximumStacks - dropItemSlot.Amount;
            int stacksToAdd = Mathf.Min(numAddableStacks, _draggedSlot.Amount);

            dropItemSlot.Amount += stacksToAdd;
            _draggedSlot.Amount -= stacksToAdd;
        }

        public void RemoveStack(BaseItemSlot currentItemSlot)
        {
            if (currentItemSlot.Item != null) currentItemSlot.Amount--;
        }
    }
}