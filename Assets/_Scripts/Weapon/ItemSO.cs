using UnityEngine;

namespace _Scripts.Weapon
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
    public class ItemSO : ScriptableObject
    {
        public string itemName;
        public string minLevel;
        public string description;
    }
}