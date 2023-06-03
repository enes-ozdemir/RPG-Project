using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Inventory_System.Base
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        [Header("Main Info")]
        public string itemName;
        public int minLevel;
        public int itemLevel;
        public int itemValue;
        public Sprite itemIcon;
        public List<BonusStat> bonusStats;
        public ItemType itemType;
        [Range(1, 16)] public int maximumStacks = 1;
        [Header("Additional Info")]
        public int itemID;
        public int width;
        public int height;
        [SerializeField] string id;
        
        public List<string> GetItemInfoAsList()
        {
            var itemInfoList = new List<string>();

            itemInfoList.Add(itemName + " " + itemLevel);
            itemInfoList.Add("Level: " + minLevel);
            foreach (var stat in bonusStats)
            {
                itemInfoList.Add(stat.additionalStats + ": " + stat.value);
            }

            itemInfoList.Add("Item Value: " + itemValue);

            return itemInfoList;
        }
        
        public string ID => id;

        private void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            id = AssetDatabase.AssetPathToGUID(path);
        }

        public Item GetCopy() => this;
    }

    public enum AdditionalStats
    {
        MovementSpeed,
        AttackSpeed,
        AttackDamage,
        AbilityPower,
        Armor,
        MagicResist,
        Health,
        Mana,
        HealthRegen,
        ManaRegen,
        CooldownReduction,
        LifeSteal,
        ArmorPenetration,
    }

    [Serializable]
    public class BonusStat
    {
        public AdditionalStats additionalStats;
        public int value;
        public int combatPoint;
    }
}