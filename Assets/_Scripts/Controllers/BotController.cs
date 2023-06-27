using _Scripts.Data;
using _Scripts.Managers;
using _Scripts.SO;
using _Scripts.UI;
using Random = Enca.Extensions.Random;

namespace _Scripts.Controllers
{
    public class BotController : CharController, ICanTalk
    {
        public Dialogue uniqueDialogue;
        private CharInfo _info;
        private Bot _bot = new();

        private void Start()
        {
            _info = Instantiate(charInfo, transform);
            GenerateRandomBot();
            //PlayAnim(Idle.ToString());
        }

        public void Talk()
        {
            if (uniqueDialogue != null)
            {
                _info.Talk(uniqueDialogue.GetRandomDialog(), Random.GetRandomNumber(12f));
            }
        }

        private void GenerateRandomBot() => _info.CreateCharInfo(_bot.level, _bot.name);
    }
}