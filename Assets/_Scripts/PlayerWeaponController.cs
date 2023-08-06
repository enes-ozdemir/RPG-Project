using UnityEngine;

namespace _Scripts
{
    public class PlayerWeaponController : MonoBehaviour
    {
    
        [SerializeField] private Transform backWeaponTransform;
        [SerializeField] private Transform backShieldTransform;
    
        [SerializeField] private Transform oneHandTransform;

        [SerializeField] private GameObject tempWeapon;
    

        private void Start()
        {
            Equip(tempWeapon, WeaponType.OneHanded);
        }

        private void Equip(GameObject weapon, WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.OneHanded:
                    Instantiate(weapon, oneHandTransform);
                    break;
            
            }
        }
    }

    public enum WeaponType
    {
        OneHanded,
        Shield,
        Bow,
    }
}