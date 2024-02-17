// using System;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEditor.UIElements;
// using UnityEngine;
// using UnityEngine.UIElements;
//
// namespace Editor
// {
//     public class ErrorArea : VisualElement
//     {
//         private EditorErrors _editorErrors;
//         private string _errorText;
//         private Sprite _errorImage;
//         
//         public ErrorArea()
//         {
//             var visualTreeAsset = Resources.Load<VisualTreeAsset>("Assets/Editor/ErrorArea.uxml");
//             visualTreeAsset.CloneTree(this);
//
//             var errorImage = this.Q<VisualElement>("ErrorImage");
//             var errorText = this.Q<Label>("ErrorText");
//             
//             errorImage.style.backgroundImage = _errorImage.texture;
//             errorText.text = _errorText;
//         }
//     }
//     
//     
//     public class ItemCreator : EditorWindow
//     {
//         [SerializeField] private VisualTreeAsset m_UXMLTree;
//         [SerializeField] private VisualTreeAsset additionalStatElement;
//         private Foldout _foldoutContentArea;
//         private string _infoText;
//         private Label _infoLabel;
//         private Item _item;
//
//         private ItemTypeBonusStats _bonusStats;
//         private ItemType _currentItemType;
//
//         private const string ItemTypeBonusStatsPath = "Assets/SO/ItemSystem/ItemTypeBonusStats.asset";
//
//         private List<string> _additionalStatsList = new List<string>();
//
//         private List<string> _additionalStatTypes = new List<string>();
//         //   private List<TemplateContainer> additionalStatsElements = new();
//
//         private ErrorArea _errorArea = new ErrorArea();
//         
//         [MenuItem("RPG/ItemCreator")]
//         public static void ShowMyEditor()
//         {
//             var window = GetWindow<ItemCreator>();
//             window.titleContent = new GUIContent("ItemCreator");
//             window.maxSize = new Vector2(320, 500);
//             window.minSize = window.maxSize;
//         }
//
//         public void OnEnable()
//         {
//             InitializeUI();
//             SetupEventHandlers();
//             LoadItemBonusStats();
//         }
//
//         private void InitializeUI()
//         {
//             var root = rootVisualElement;
//             m_UXMLTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/ItemCreator.uxml");
//             m_UXMLTree.CloneTree();
//             var ui = m_UXMLTree.CloneTree();
//             root.Add(ui);
//         }
//
//         #region SetupRegion
//
//         private void SetupEventHandlers()
//         {
//             SetupItemIcon();
//             SetupClearButton();
//             SetupCreateButton();
//             SetupEnumField();
//             SetupAddAdditionalStatArea();
//         }
//
//         private void SetupItemIcon()
//         {
//             var itemIcon = rootVisualElement.Q<VisualElement>("ItemImage");
//             SelectItemImage(itemIcon);
//         }
//
//         private void SetupClearButton()
//         {
//             var clearButton = rootVisualElement.Q<Button>("ClearButton");
//             var itemIcon = rootVisualElement.Q<VisualElement>("ItemImage");
//             _infoLabel = rootVisualElement.Q<Label>("ImageInfoLabel");
//             _infoText = _infoLabel.text;
//
//             clearButton.clicked += () =>
//             {
//                 itemIcon.style.backgroundImage = null;
//                 _infoLabel.text = _infoText;
//             };
//         }
//
//         private void SetupCreateButton()
//         {
//             var createButton = rootVisualElement.Q<Button>("CreateButton");
//             var itemLevelField = rootVisualElement.Q<SliderInt>("ItemLevel");
//             createButton.clicked += () => CreateNewItem(_item);
//         }
//
//         private void SetupEnumField()
//         {
//             var itemTypeField = rootVisualElement.Q<EnumField>("ItemType");
//             itemTypeField.Init(ItemType.Weapon);
//             itemTypeField.value = ItemType.Weapon;
//             itemTypeField.RegisterValueChangedCallback(OnItemTypeChanged);
//         }
//
//         private void SetupAddAdditionalStatArea()
//         {
//             _foldoutContentArea = rootVisualElement.Q<Foldout>();
//             var addStatButton = rootVisualElement.Q<Button>("AddStatButton");
//             addStatButton.clicked += AddNewAdditionalStatElement;
//         }
//
//         #endregion
//
//         #region AdditionalStatRegioon
//
//         private void LoadItemBonusStats()
//         {
//             _additionalStatsList.Clear();
//             var itemTypeBonusStat = AssetDatabase.LoadAssetAtPath<ItemTypeBonusStats>(ItemTypeBonusStatsPath);
//             if (itemTypeBonusStat == null)
//             {
//                 Debug.LogError("ItemTypeBonusStats not found");
//                 return;
//             }
//
//             var statsList = itemTypeBonusStat.GetAdditionalStatsFromItemType(_currentItemType);
//
//             foreach (var stat in statsList)
//             {
//                 _additionalStatsList.Add(stat.ToString());
//             }
//         }
//
//         private void ResetAdditionalStatsForItemType()
//         {
//             LoadItemBonusStats();
//
//             _foldoutContentArea.Clear();
//         }
//
//         private void AddNewAdditionalStatElement()
//         {
//             var statUI = additionalStatElement.CloneTree();
//             statUI.Q<DropdownField>("Stat").choices = _additionalStatsList;
//             var removeStatButton = statUI.Q<Button>("RemoveStatButton");
//
//             var value = statUI.Q<DropdownField>("Stat").value;
//             if (_additionalStatTypes.Contains(value))
//             {
//                 Debug.LogError("Stat already added");
//                 return;
//             }
//
//             _additionalStatTypes.Add(value);
//             _foldoutContentArea.Add(statUI);
//
//             removeStatButton.clicked += () => RemoveStatElement(statUI);
//         }
//
//         private void RemoveStatElement(VisualElement element) => _foldoutContentArea.Remove(element);
//
//         #endregion
//
//         #region Callback Region
//
//         private void OnItemTypeChanged(ChangeEvent<Enum> evt)
//         {
//             _currentItemType = (ItemType)evt.newValue;
//             ResetAdditionalStatsForItemType();
//         }
//
//         #endregion
//
//         #region ImageRegion
//
//         private void SelectItemImage(VisualElement itemIcon)
//         {
//             itemIcon.RegisterCallback<ClickEvent>(evt =>
//             {
//                 string path = EditorUtility.OpenFilePanel("Select Item Icon", "Assets/Art/ItemImages", "png,jpg,jpeg");
//
//                 if (!string.IsNullOrEmpty(path))
//                 {
//                     var texture = new Texture2D(2, 2);
//                     texture.LoadImage(System.IO.File.ReadAllBytes(path));
//                     itemIcon.style.backgroundImage = texture;
//                     _infoLabel.text = "";
//                 }
//             });
//         }
//
//         #endregion
//
//         #region CreateRegion
//
//         private void CreateNewItem(Item item)
//         {
//             throw new NotImplementedException();
//         }
//
//         #endregion
//     }
// }