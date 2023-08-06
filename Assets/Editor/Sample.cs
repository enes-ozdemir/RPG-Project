using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class Sample : EditorWindow
    {
        
        private VisualElement m_RightPane;
        
        [MenuItem("asd/My Custom Editor")]
        public static void ShowMyEditor()
        {
            // This method is called when the user selects the menu item in the Editor
            EditorWindow wnd = GetWindow<Sample>();
            wnd.titleContent = new GUIContent("ItemCreator");
            
            // Limit size of the window
            wnd.minSize = new Vector2(450, 200);
            wnd.maxSize = new Vector2(1920, 720);
        }
        
        public void CreateGUI()
        {
            rootVisualElement.Add(new Label("Hello"));
            
            // Get a list of all sprites in the project
            var allObjectGuids = AssetDatabase.FindAssets("t:Sprite");
            var allObjects = new List<Sprite>();
            foreach (var guid in allObjectGuids)
            {
                allObjects.Add(AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(guid)));
            }
            
            var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

// Add the view to the visual tree by adding it as a child to the root element
            rootVisualElement.Add(splitView);

// A TwoPaneSplitView always needs exactly two child elements
            var leftPane = new ListView();
            splitView.Add(leftPane);
            m_RightPane = new VisualElement();
            splitView.Add(m_RightPane);
            
            leftPane.makeItem = () => new Label();
            leftPane.bindItem = (item, index) => { (item as Label).text = allObjects[index].name; };
            leftPane.itemsSource = allObjects;
            
            leftPane.onSelectionChange += OnSpriteSelectionChange;
            
            m_RightPane = new ScrollView(ScrollViewMode.VerticalAndHorizontal);
            splitView.Add(m_RightPane);
        }
        
        private void OnSpriteSelectionChange(IEnumerable<object> selectedItems)
        {
            // Clear all previous content from the pane
            m_RightPane.Clear();

            // Get the selected sprite
            var selectedSprite = selectedItems.First() as Sprite;
            if (selectedSprite == null)
                return;

            // Add a new Image control and display the sprite
            var spriteImage = new Image();
            spriteImage.scaleMode = ScaleMode.ScaleToFit;
            spriteImage.sprite = selectedSprite;

            // Add the Image control to the right-hand pane
            m_RightPane.Add(spriteImage);
        }
    }
}