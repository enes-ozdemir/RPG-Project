using UnityEngine;

namespace _Scripts.Weapon
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
    public class ItemSO : ScriptableObject
    {
        public string weaponName;
        public string minLevel;
        public string description;
    }
}