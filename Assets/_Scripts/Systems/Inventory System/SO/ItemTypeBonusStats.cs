using System;
using System.Collections.Generic;
using _Scripts.Inventory_System.Base;
using UnityEngine;

namespace _Scripts.Inventory_System.SO
{
    [CreateAssetMenu(menuName = "ItemSystem/ItemTypeBonusStats")]
    public class ItemTypeBonusStats : ScriptableObject
    {
        public List<ItemBonusStats> bonusStats;

        public List<AdditionalStats> GetAdditionalStatsFromItemType(ItemType itemType)
        {
            foreach (var bonusStat in bonusStats)
            {
                if (bonusStat.itemType == itemType)
                {
                    return bonusStat.additionalStats;
                }
            }
            return new List<AdditionalStats>();
        }
    }

    [Serializable]
    public class ItemBonusStats
    {
        public ItemType itemType;
        public List<AdditionalStats> additionalStats;
    }
}