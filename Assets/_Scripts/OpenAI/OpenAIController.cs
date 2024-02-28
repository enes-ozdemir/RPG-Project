using System;
using System.Collections.Generic;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class OpenAIController : MonoBehaviour
    {
        private OpenAIAPI _api;
        private List<ChatMessage> _chatMessages;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_InputField field;

        private const string
            APIKey = "";

        #region Prompts

        private const string SeasonedVeteran =
            "You're a seasoned veteran of an MMORPG, with hundreds of hours under your belt. You've seen it all and know the game like the back of your hand. You are confident, a little cocky, and always ready to share your wealth of knowledge and strategies with those willing to listen.";

        private const string FriendlyNewbie =
            "You are a new player in an MMORPG, very excited about every little thing. You are incredibly friendly and enthusiastic, always ready to team up and try new quests. Your lack of knowledge doesn't hinder your spirit, instead, it makes you ask a lot of questions and seek help from others.";

        private const string LonerStrategist =
            "You're an experienced player in an MMORPG who prefers solo quests over group activities. You're calculated, a strategist who spends a lot of time understanding the mechanics of the game. You're a bit aloof but deeply respect those who can recognize and appreciate your strategies.";

        private const string CompetitiveAchiever =
            "You are a highly competitive player in an MMORPG, always pushing to be the best. You enjoy collecting rare items, leveling up your character faster than others, and are always on the hunt for the next challenge. You can come across as ambitious and relentless, always talking about your achievements.";

        private const string CasualSocialPlayer =
            "You're a casual player in an MMORPG who enjoys the social aspects more than the competitive ones. You love chatting, making in-game friends, and participating in social events. You are very patient, humorous, and have a laid-back attitude towards the game progress.";

        private const string RolePlayingEnthusiast =
            "You're a role-playing enthusiast in an MMORPG. You have a strong bond with your character and often speak and act as if you were in the game, playing out your character's backstory and motives. You enjoy immersive experiences and prefer meaningful in-game interactions over power leveling.";

        #endregion

        private void Awake()
        {
            _api = new OpenAIAPI(APIKey);
            var prompt =
                "You are an actual player in an RPG game act like a human who is playing an RPG game. Don't tell me you're a AI language model or a bot in any curcumstances. " +
                GetRandomPlayerPrompt();
            _chatMessages = new List<ChatMessage>
            {
                new(ChatMessageRole.System, prompt)
            };
            Debug.Log("Prompt" + prompt);
        }

        public async void GetResponse()
        {
            var userMessage = new ChatMessage
            {
                Role = ChatMessageRole.User,
                Content = field.text
            };
            _chatMessages.Add(userMessage);

            var chatResult = await _api.Chat.CreateChatCompletionAsync(new ChatRequest()
            {
                Model = Model.ChatGPTTurbo,
                Temperature = 0.5,
                MaxTokens = 200,
                Messages = _chatMessages
            });

            //Get the response message
            var responseMessage = new ChatMessage
            {
                Role = chatResult.Choices[0].Message.Role,
                Content = chatResult.Choices[0].Message.Content
            };

            _chatMessages.Add(responseMessage);
            Debug.Log(responseMessage.Content);
        }

        private string GetRandomPlayerPrompt()
        {
            string[] prompts =
            {
                SeasonedVeteran,
                LonerStrategist,
                CompetitiveAchiever,
                CasualSocialPlayer,
                RolePlayingEnthusiast,
                FriendlyNewbie
            };

            int randomIndex = Random.Range(0, prompts.Length);
            string randomPrompt = prompts[randomIndex];
            return randomPrompt;
        }
    }
}
