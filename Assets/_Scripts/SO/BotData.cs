using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.SO
{
    [CreateAssetMenu(menuName = "BotSystem/BotData")]
    public class BotData : ScriptableObject
    {
        public List<GameObject> botPrefabs;
    }

}