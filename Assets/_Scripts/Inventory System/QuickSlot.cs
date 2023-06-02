using UnityEngine;

namespace _Scripts.Inventory_System
{
    public class QuickSlot : ItemSlot
    {
        [SerializeField] private GameObject selection;
        private bool _isSelected;

        private void Start()
        {
            DeSelectSlot();
        }

        public void SelectSlot()
        {
            selection.SetActive(true);
        }

        public void DeSelectSlot()
        {
            selection.SetActive(false);
        }
    }
}