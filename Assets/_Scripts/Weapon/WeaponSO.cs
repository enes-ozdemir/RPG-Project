using UnityEngine;

namespace _Scripts.Weapon
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
    public class WeaponSO : ItemSO
    {
        public int upgradeLevel = 0;

        [Header("Weapon properties")]
        public int minBaseAttackDamage;
        public int maxBaseAttackDamage;
        public int minBaseMagicalDamage;
        public int maxBaseMagicalDamage;
        public float attackSpeed;
        
        public GameObject weaponPrefab;
        
        public int GetMinAttackDamage() => minBaseAttackDamage + minBaseAttackDamage * upgradeLevel /10;
        public int GetMaxAttackDamage() => maxBaseAttackDamage + maxBaseAttackDamage * upgradeLevel /10;
        public int GetMinMagicalDamage() => minBaseMagicalDamage + minBaseMagicalDamage * upgradeLevel /10;
        public int GetMaxMagicalDamage() => maxBaseMagicalDamage + maxBaseMagicalDamage * upgradeLevel /10;
        public void UpgradeItem() => upgradeLevel++;

    }
}