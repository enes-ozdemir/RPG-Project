using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.Inventory_System.Base
{
    public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Image image;
        [SerializeField] protected TextMeshProUGUI amountText;

        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnShiftRightClickEvent;

        protected Color normalColor = Color.white;
        private Color _disabledColor = new(1, 1, 1, 0);
        private Item _item;

        public Item Item
        {
            get => _item;
            set
            {
                _item = value;
                if (_item == null)
                {
                    image.color = _disabledColor;
                }
                else
                {
                    image.sprite = _item.itemIcon;
                    image.color = normalColor;
                }
            }
        }

        private Image _baseImage;

        private void OnEnable()
        {
            _baseImage = GetComponentInParent<Image>();
        }

        private int _amount;

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = Mathf.Max(0, value);
                if (_amount == 0) Item = null;

                if (amountText != null)
                {
                    amountText.enabled = _item != null && _amount > 1;
                    if (amountText.enabled)
                    {
                        amountText.text = _amount.ToString();
                    }
                }
            }
        }

        protected void OnValidate()
        {
            if (image == null)
                image = GetComponent<Image>();

            if (amountText == null)
                amountText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public virtual bool CanAddStack(Item item, int amount = 1)
        {
            if (Item == null) return false;
            return Item != null && Item.itemID == item.itemID;
        }

        public virtual bool CanReceiveItem(Item item) => false;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData != null && eventData.button == PointerEventData.InputButton.Right &&
                Input.GetKey(KeyCode.LeftShift))
            {
                OnShiftRightClickEvent?.Invoke(this);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterEvent?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitEvent?.Invoke(this);
        }

        public Item GetItemFromSlot() => Item;

        public void LightUpTheSlot(Color color) => _baseImage.color= color;

        public void ResetSlotColor()
        {
            if (_baseImage.color != normalColor) _baseImage.color = normalColor;
        }
    }
}