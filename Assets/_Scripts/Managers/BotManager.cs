using System;
using System.Collections.Generic;
using _Scripts.Controllers;
using _Scripts.SO;
using _Scripts.UI;
using Enca.Debug;
using Enca.Extensions;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Managers
{
    public class BotManager : MonoBehaviour
    {
        [SerializeField] private int maxBotSize = 20;
        [SerializeField] private int startBotSize = 20;
        [SerializeField] private float botWalkRadius = 40f;

        [SerializeField] private CharInfo charInfo;
        [SerializeField] private BotData botData;
        [SerializeField] private List<Transform> botGenerationPoints;

        private void OnDrawGizmos()
        {
            //draw sphere
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, botWalkRadius);
        }

        private void Start()
        {
            GenerateStarterBots();
        }

        private void GenerateStarterBots()
        {
            for (int i = 0; i < startBotSize; i++)
            {
                GenerateBot();
            }
        }

        private void GenerateBot()
        {
            var prefab = GetRandomBotPrefab();
            var bot = Instantiate(prefab, botGenerationPoints.SelectRandomItem().position, Quaternion.identity);
            var controller = bot.GetComponent<BotController>();
            controller.InitializeBot(charInfo,transform, botWalkRadius);

            EventManager.TriggerEvent(GameEvent.OnBotGenerated);
        }

        private GameObject GetRandomBotPrefab()
        {
            return botData.botPrefabs.SelectRandomItem();
        }
    }
}