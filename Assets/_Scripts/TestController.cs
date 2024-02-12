using MalbersAnimations;
using MalbersAnimations.Controller;
using UnityEngine;

public class TestController : MonoBehaviour
{
    private MAnimal _animalController;
    private MInventory _inv;
    [SerializeField] private GameObject _weapon;

    private void Awake()
    {
        _animalController = GetComponent<MAnimal>();
        _inv = GetComponent<MInventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            _inv.AddItem(_weapon.gameObject);
            _inv.EquipItem(0);
        }
    }
}