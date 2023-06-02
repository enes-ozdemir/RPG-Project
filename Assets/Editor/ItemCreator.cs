using System;
using _Scripts.Inventory_System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using _Scripts.Inventory_System;
using _Scripts.Inventory_System.Base;


namespace Editor
{
    public class ItemCreator : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_UXMLTree;
        private string _infoText;
        private Label _infoLabel;
        private Item _item;

        [MenuItem("RPG/ItemCreator")]
        public static void ShowMyEditor()
        {
            var window = GetWindow<ItemCreator>();
            window.titleContent = new GUIContent("ItemCreator");
        }

        public void OnEnable()
        {
            InitializeUI();
            SetupEventHandlers();
        }

        private void InitializeUI()
        {
            var root = rootVisualElement;
            m_UXMLTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/ItemCreator.uxml");
            m_UXMLTree.CloneTree();
            var ui = m_UXMLTree.CloneTree();
            root.Add(ui);
        }

        private void SetupEventHandlers()
        {
            SetupItemIcon();
            SetupClearButton();
            SetupCreateButton();
            SetupEnumField();
        }

        private void SetupEnumField()
        {
            var itemTypeField = rootVisualElement.Q<EnumField>("ItemType");
            itemTypeField.Init(ItemType.Weapon);
            itemTypeField.value = ItemType.Weapon;
            itemTypeField.RegisterValueChangedCallback(OnItemTypeChanged);
        }
        
        private void OnItemTypeChanged(ChangeEvent<Enum> evt)
        {
            var selectedItemType = (ItemType)evt.newValue;
            SetAdditionalStatSection(selectedItemType);
        }

        private void SetAdditionalStatSection(ItemType selectedItemType)
        {
            throw new NotImplementedException();
        }

        private void SetupItemIcon()
        {
            var itemIcon = rootVisualElement.Q<VisualElement>("ItemImage");
            SelectItemImage(itemIcon);
        }

        private void SetupClearButton()
        {
            var clearButton = rootVisualElement.Q<Button>("ClearButton");
            var itemIcon = rootVisualElement.Q<VisualElement>("ItemImage");
            _infoLabel = rootVisualElement.Q<Label>("ImageInfoLabel");
            _infoText = _infoLabel.text;

            clearButton.clicked += () =>
            {
                itemIcon.style.backgroundImage = null;
                _infoLabel.text = _infoText;
            };
        }

        private void SetupCreateButton()
        {
            var createButton = rootVisualElement.Q<Button>("CreateButton");
            var itemLevelField = rootVisualElement.Q<SliderInt>("ItemLevel");
            createButton.clicked += () => CreateNewItem(_item);
        }

        private void CreateNewItem(Item item)
        {
            
            throw new NotImplementedException();
        }

        private void SelectItemImage(VisualElement itemIcon)
        {
            itemIcon.RegisterCallback<ClickEvent>(evt =>
            {
                string path = EditorUtility.OpenFilePanel("Select Item Icon", "Assets/Art/ItemImages", "png,jpg,jpeg");

                if (!string.IsNullOrEmpty(path))
                {
                    var texture = new Texture2D(2, 2);
                    texture.LoadImage(System.IO.File.ReadAllBytes(path));
                    itemIcon.style.backgroundImage = texture;
                    _infoLabel.text = "";
                }
            });
        }
    }
}