using System;
using UnityEngine;

namespace _Scripts
{
    public class GameSettings : ScriptableObject
    {
        [Header("Npc Settings")]
        public NpcSettings npcSettings;
    }

    [Serializable]
    public class NpcSettings
    {
        public static float npcTalkTimer = 30f;
    }
}