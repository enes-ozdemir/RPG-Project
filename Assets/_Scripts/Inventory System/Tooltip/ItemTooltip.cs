using System.Collections.Generic;
using _Scripts.Inventory_System.Base;
using UnityEngine;

namespace _Scripts.Inventory_System.Tooltip
{
    public class ItemTooltip : MonoBehaviour
    {
        [SerializeField] private float yOffset = -2;
        [SerializeField] private float xOffset = -2;
        [SerializeField] private List<ItemTip> itemTipList;
        private Item _item;
        private Vector2 _rectSize;
        private RectTransform _rectTransform;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            itemTipList.Clear();
            var childItems = GetComponentsInChildren<ItemTip>(true);
            itemTipList.AddRange(childItems);
        }
#endif
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            var rect = _rectTransform.rect;
            _rectSize = new Vector2(rect.width, rect.height);
        }

        private void SetItemInfo(List<string> itemInfo)
        {
            for (int i = 0; i < itemInfo.Count; i++)
            {
                if (i < itemTipList.Count)
                {
                    itemTipList[i].gameObject.SetActive(true);
                    itemTipList[i].itemValueText.text = itemInfo[i];
                }
                else
                {
                    Debug.LogError("Not enough itemTipList elements for all itemInfo values.");
                    break;
                }
            }
        }

        public void ShowTooltip(Item item)
        {
            gameObject.SetActive(true);
            var itemInfoAsList = item.GetItemInfoAsList();
            SetItemInfo(itemInfoAsList);
            UpdateTooltipPosition();
        }

        public void HideTooltip()
        {
            HideAllInfo();
            gameObject.SetActive(false);
        }

        private void HideAllInfo()
        {
            foreach (var itemTip in itemTipList)
            {
                itemTip.gameObject.SetActive(false);
            }
        }

        private void UpdateTooltipPosition()
        {
            Vector2 mousePos = Input.mousePosition;

            mousePos.x = Mathf.Clamp(mousePos.x, _rectSize.x / 2f, Screen.width - _rectSize.x / 2f);
            mousePos.y = Mathf.Clamp(mousePos.y, _rectSize.y / 2f, Screen.height - _rectSize.y / 2f);

            _rectTransform.position =
                new Vector3(mousePos.x + xOffset, mousePos.y + yOffset, _rectTransform.position.z);
        }
    }
}