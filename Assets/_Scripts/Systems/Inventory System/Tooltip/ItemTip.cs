using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Inventory_System.Tooltip
{
    public class ItemTip : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI itemValueText;
        [SerializeField] public LayoutElement layoutElement;

        // private void OnEnable()
        // {
        //     itemValueText = GetComponent<TextMeshProUGUI>();
        //     layoutElement = GetComponent<LayoutElement>();
        // }
    }
}