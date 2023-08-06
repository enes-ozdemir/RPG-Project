using System;
using _Scripts.Data;
using UnityEngine;

namespace _Scripts.SO
{
    [Serializable]
    public class Character : ScriptableObject
    {
        public string name;
        public int level;
        public Stats stats;
        public Dialogue dialogue;
    }
}