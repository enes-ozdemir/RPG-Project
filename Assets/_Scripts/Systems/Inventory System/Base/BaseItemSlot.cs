using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
        private Image _slotImage;
        public int cellSize { get; set; }
        [SerializeField] protected Image itemImage;
        [SerializeField] private string itemName;

        private bool _isPointerOver;
        private Item _item;
        public bool isOccupied;
        public Vector2Int position;

        [HideInInspector] public bool isEnabled;

        public Item Item

        {
            get => _item;
            set
            {
                _item = value;
                itemName = _item.itemName;
                if (_item == null)
                    DisableSlot();
                else
                    EnableSlot();
            }
        }

        public bool IsContain(Item item)
        {
            return this._item == item;
        }

        private void Update()
        {
            if (_isPointerOver)
            {
                //  Debug.Log("Mouse position: " + Input.mousePosition);
            }
        }

        private void EnableSlot()
        {
            if (_item == null || itemImage == null) return;
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = _item.itemIcon;
            // Adjust the size of the image to cover multiple slots
            // itemImage.rectTransform.sizeDelta =
            //     new Vector2(_item.width * cellSize * 0.75f, _item.height * cellSize * 0.75f);

            itemImage.color = normalColor;
            isEnabled = true;
            isOccupied = true;
        }

        public void DisableSlot()
        {
            isEnabled = false;
            isOccupied = false;
            itemImage.gameObject.SetActive(false);
            if (itemImage == null) return;
            itemImage.color = _disabledColor;
        }

        private void OnEnable()
        {
            _slotImage = GetComponent<Image>();
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
            _isPointerOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitEvent?.Invoke(this);
            _isPointerOver = false;
        }

        public void LightUpTheSlot(Color color) => _slotImage.color = color;

        public void ResetSlotColor()
        {
            if (_slotImage.color != normalColor) _slotImage.color = normalColor;
        }
    }
}