﻿using System.Collections.Generic;
using _Scripts.Controllers;
using _Scripts.SO;
using _Scripts.UI;
using Enca.Debug;
using Enca.Extensions;
using UnityEngine;

namespace _Scripts.Managers
{
    public class BotManager : MonoBehaviour
    {
        [SerializeField] private int maxBotSize = 20;
        [SerializeField] private int startBotSize = 20;

        [SerializeField] private CharInfo charInfo;
        [SerializeField] private BotData botData;
        [SerializeField] private List<Transform> botGenerationPoints;

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
            var controller = bot.AddComponent<BotController>();
            controller.InitializeBot(charInfo);

            EventManager.TriggerEvent(GameEvent.OnBotGenerated);
        }

        private GameObject GetRandomBotPrefab()
        {
            return botData.botPrefabs.SelectRandomItem();
        }
    }
}