using UnityEditor;
using UnityEngine;

// namespace _Scripts.Inventory_System
// {
//     public class Item : ScriptableObject
//     {
//         public string itemName;
//         public int itemID;
//         public int minLevel;
//         public int itemLevel;
//         public Sprite itemIcon;
//         public int width;
//         public int height;
//         public int itemValue;
//       //  public AdditionalStats additionalStats;
//
//     }
//
//     // [Serializable]
//     // public class AdditionalStats
//     // {
//     //     private int health;
//     //     private int damage;
//     //     private int attackSpeed;
//     //     private int healthRegeneration;
//     //     private int armor;
//     //
//     // }
// }

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
        [SerializeField] string id;
        [Range(1,16)]
        public int maximumStacks = 1;
        public string ID
        {
            get { return id; }
        }
        public Sprite icon;
        public int value;

        private void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            id = AssetDatabase.AssetPathToGUID(path);
        }

        public virtual Item GetCopy()
        {
            return this;
        }  
        public virtual void Destroy()
        {
        
        }
    }
}
