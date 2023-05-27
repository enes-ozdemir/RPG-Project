using _Scripts.Inventory_System;
using UnityEngine;

namespace _Scripts
{
    [CreateAssetMenu]
    public class Weapon : Item
    {
        public int damage;
        public int attackSpeed;
    }

    [CreateAssetMenu]
    public class Armor : Item
    {
        public int armor;
        public int health;
        public int healthRegeneration;
    }

    [CreateAssetMenu]
    public class Boot : Item
    {
        public int movementSpeed;
        public int armor;
    }

    [CreateAssetMenu]
    public class Shield : Item
    {
        public int armor;
    }
}