using _Scripts.Weapon;
using MalbersAnimations;
using MalbersAnimations.Controller;
using PowerGridInventory;
using UnityEngine;

public class TestController : MonoBehaviour
{
    private MAnimal _animalController;
    private MInventory _inv;
    [SerializeField] private WeaponSO _weapon;
    [SerializeField] private PGISlotItem slotItemPrefab;

    private void Awake()
    {
        _animalController = GetComponent<MAnimal>();
        _inv = GetComponent<MInventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            _inv.AddItem(_weapon.weaponPrefab.gameObject);
            _inv.EquipItem(_inv.Inventory.Count-1);
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            slotItemPrefab.InitializeSlotItem(_weapon);
            
        }
        
    }
}