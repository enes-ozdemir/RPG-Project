using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Inventory_System
{
    public class ItemTooltip : MonoBehaviour
    {
        [SerializeField] private ItemTip _itemTip;
        private Item _item;

        private void GetItemInfo()
        {
            
        }

        private void OnEnable()
        {
            var nameText = _itemTip.itemValueText;
        }

        public void ShowTooltip(Item item)
        {
            gameObject.SetActive(true);
        }

        public void HideTooltip() => gameObject.SetActive(false);
    }

    public class ItemTip : MonoBehaviour
    {
        [SerializeField] public Text itemValueText;
        [SerializeField] public LayoutElement layoutElement;
    }
}