using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Inventory_System
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public string itemName;
        public int itemID;
        public int minLevel;
        public int itemLevel;
        public Sprite itemIcon;
        public int width;
        public int height;
        public int itemValue;
        public AdditionalStats additionalStats;
        public ItemType itemType;
        [SerializeField] string id;
        [Range(1, 16)] public int maximumStacks = 1;

        public List<string> GetItemInfoAsList()
        {
            List<string> itemInfoList = new List<string>();

            itemInfoList.Add(itemName + " " + itemLevel.ToString());
            itemInfoList.Add("Level: " + minLevel.ToString());
            itemInfoList.Add("Item Value: " + itemValue.ToString());

            return itemInfoList;
        }

        public string ID
        {
            get { return id; }
        }

        private void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            id = AssetDatabase.AssetPathToGUID(path);
        }

        public Item GetCopy()
        {
            return this;
        }

        public void Destroy()
        {
        }
    }

    [Serializable]
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
        GoldPer10,
        ExperiencePer10
    }
}