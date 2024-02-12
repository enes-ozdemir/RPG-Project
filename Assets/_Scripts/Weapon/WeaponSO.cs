using System;
using MalbersAnimations.Weapons;
using UnityEngine;

namespace _Scripts.Weapon
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
    public class WeaponSO : ItemSO
    {
        [SerializeReference] public MWeapon MWeapon;
        public void SetPrefab(GameObject prefab) => MWeapon.Owner = prefab;
    }
}