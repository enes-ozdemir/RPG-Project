using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Inventory_System.Base
{
    public class PlayerItemSlot : BaseItemSlot, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
    {
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;
        public event Action<BaseItemSlot> OnBuyEvent;

        private Color _dragColor = new(1, 1, 1, 0.5f);
        private Vector2 _originalPosition;

        public override bool CanAddStack(Item item, int amount = 1) => base.CanAddStack(item, amount) && Amount + amount <= item.maximumStacks;

        public override bool CanReceiveItem(Item item) => true;

        public void OnDrag(PointerEventData eventData) => OnDragEvent?.Invoke(this);

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Item != null) itemImage.color = _dragColor;
            
            OnBeginDragEvent?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (Item != null) itemImage.color = normalColor;
            
            OnEndDragEvent?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData) => OnDropEvent?.Invoke(this);

        public void OnBuy(PointerEventData eventData) => OnBuyEvent?.Invoke(this);
        
        
    }
}