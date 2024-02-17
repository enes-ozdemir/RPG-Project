using UnityEngine;

namespace _Scripts.Weapon
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
    public class ItemSO : ScriptableObject
    {
        [Header("Inventory properties")]
        public string itemName;
        public string minLevel;
        public string description;
        [Min(1)] public int width=1;
        [Min(1)] public int height=1;
        public Sprite icon;
        public int id;
        [Min(1)] public int maxStackSize=1;
    }
}