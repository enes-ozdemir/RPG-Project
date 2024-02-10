using System;
using UnityEngine;

namespace Editor
{
    [Serializable]
    public class EditorError
    {
        public ErrorType errorType;
        public Sprite errorImage;
    }
}