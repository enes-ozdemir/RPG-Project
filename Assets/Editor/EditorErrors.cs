using UnityEngine;

namespace Editor
{
    [CreateAssetMenu(fileName = "EncaEditor/EditorErrors", menuName = "EditorErrors", order = 0)]
    public class EditorErrors : ScriptableObject
    {
        public EditorError[] errors;
    }
}