using _Scripts.Data;
using _Scripts.SO;
using _Scripts.UI;
using _Scripts.Utils;
using Enca.Debug;
using Enca.Extensions;
using Random = Enca.Extensions.Random;

namespace _Scripts.Controllers
{
    public class BotController : CharController, ICanTalk
    {
        public Dialogue uniqueDialogue;
        private CharInfo _info;
        private Bot _bot;

        public void InitializeBot(CharInfo charInformation)
        {
            SetBot();
            charInfo = charInformation;
            _info = Instantiate(charInformation, transform);
            GenerateRandomBot();
        }

        private void SetBot()
        {
            var level = Random.GetRandomNumber(99);
            var botName = RpgUtil.GetRandomRpgName();
            Log.Info("New Bot generated level: " + level + " name: " + botName);
            var stats = RpgUtil.GetRandomStats(level);
            _bot = new Bot(level, botName, stats);
        }

        public void Talk()
        {
            if (uniqueDialogue != null)
            {
                _info.Talk(uniqueDialogue.GetRandomDialog(), Random.GetRandomNumber(12));
            }
        }

        private void GenerateRandomBot() => _info.CreateCharInfo(_bot.level, _bot.name);
    }
}