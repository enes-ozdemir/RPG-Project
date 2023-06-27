using UnityEngine;

namespace _Scripts.SO
{
    [CreateAssetMenu(fileName = "New NPC", menuName = "Game/NPC")]
    public class NpcData :Character
    {
        public GameObject npcPrefab;
        public Sprite npcSprite;

    }
}