using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using _Scripts.Inventory_System.Base;
using _Scripts.Inventory_System.SO;


namespace Editor
{
    public class ItemCreator : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_UXMLTree;
        [SerializeField] private VisualTreeAsset additionalStatElement;
        private string _infoText;
        private Label _infoLabel;
        private Item _item;

        private ItemTypeBonusStats _bonusStats;
        private ItemType currentItemType;

        private const string ItemTypeBonusStatsPath = "Assets/SO/ItemSystem/ItemTypeBonusStats.asset";

        private List<string> _additionalStatsList;
        private List<TemplateContainer> additionalStatsElements = new();


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
            _additionalStatsList = LoadItemBonusStats();
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
            SetupAddAdditionalStatButton();
        }

        #region AdditionalStatRegioon
        
        private List<string> LoadItemBonusStats()
        {
            _additionalStatsList.Clear();
            var itemTypeBonusStat = AssetDatabase.LoadAssetAtPath<ItemTypeBonusStats>(ItemTypeBonusStatsPath);
            var statsList = itemTypeBonusStat.GetAdditionalStatsFromItemType(currentItemType);

            foreach (var stat in statsList)
            {
                _additionalStatsList.Add(stat.ToString());
            }

            return _additionalStatsList;
        }

        private void ResetAdditionalStatsForItemType()
        {
            _additionalStatsList = LoadItemBonusStats();

            foreach (var element in additionalStatsElements)
            {
                rootVisualElement.Remove(element);
                additionalStatsElements.Remove(element);
            }
        }

        private void OnPlusButtonClicked() => AddNewAdditionalStatElement();

        private void AddNewAdditionalStatElement()
        {
            var statUI = additionalStatElement.CloneTree();
            statUI.Q<DropdownField>("DropdownField").choices = _additionalStatsList;
            rootVisualElement.Add(statUI);
            additionalStatsElements.Add(statUI);
        }
        
        private void SetupAddAdditionalStatButton()
        {
            var addStatButton = rootVisualElement.Q<Button>("AddStatButton");
            addStatButton.clicked += OnPlusButtonClicked;
        }
        
        #endregion

        private void SetupEnumField()
        {
            var itemTypeField = rootVisualElement.Q<EnumField>("ItemType");
            itemTypeField.Init(ItemType.Weapon);
            itemTypeField.value = ItemType.Weapon;
            itemTypeField.RegisterValueChangedCallback(OnItemTypeChanged);
        }

        private void OnItemTypeChanged(ChangeEvent<Enum> evt)
        {
            currentItemType = (ItemType) evt.newValue;
            ResetAdditionalStatsForItemType();
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
        
        private void CreateNewItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}