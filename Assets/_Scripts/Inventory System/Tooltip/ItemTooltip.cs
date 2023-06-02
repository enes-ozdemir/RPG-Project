using System.Collections.Generic;
using _Scripts.Inventory_System.Base;
using UnityEngine;

namespace _Scripts.Inventory_System
{
    public class ItemTooltip : MonoBehaviour
    {
        [SerializeField] private List<ItemTip> itemTipList;
        private Item _item;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            itemTipList.Clear();
            var childItems = GetComponentsInChildren<ItemTip>(true);
            itemTipList.AddRange(childItems);
        }
#endif

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
    }
}