using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.Inventory_System.Base
{
    public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected TextMeshProUGUI amountText;

        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnShiftRightClickEvent;

        protected Color normalColor = Color.white;
        private Color _disabledColor = new(1, 1, 1, 0);
        private Item _item;
        private Image _slotImage;
        protected Image itemImage;

        [HideInInspector] public bool isEnabled;

        public Item Item

        {
            get => _item;
            set
            {
                _item = value;
                if (_item == null)
                    DisableSlot();
                else
                    EnableSlot();
            }
        }

        private void EnableSlot()
        {
            if (_item == null || itemImage==null) return;

            itemImage.sprite = _item.itemIcon;
            itemImage.color = normalColor;
            isEnabled = true;
        }

        public void DisableSlot()
        {
            itemImage.color = _disabledColor;
            isEnabled = false;
        }

        private void OnEnable()
        {
            _slotImage = GetComponentInParent<Image>();
            itemImage = GetComponent<Image>();
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
            if (itemImage == null)
                itemImage = GetComponent<Image>();

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

        public void LightUpTheSlot(Color color) => _slotImage.color = color;

        public void ResetSlotColor()
        {
            if (_slotImage.color != normalColor) _slotImage.color = normalColor;
        }
    }
}