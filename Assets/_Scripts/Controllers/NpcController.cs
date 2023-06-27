using _Scripts.SO;
using _Scripts.UI;
using UnityEngine;
using static _Scripts.CommonAnims;

namespace _Scripts.Controllers
{
    [RequireComponent(typeof(CharInfo))]
    [RequireComponent(typeof(NpcData))]
    public class NpcController : CharController,ICanTalk
    {
        public NpcData npcData;
        public Dialogue uniqueDialogue;
        public float talkTimer = 30f;

        private void Start()
        {
            var info = Instantiate(charInfo,transform);
            info.CreateCharInfo(npcData.level, npcData.name);
            PlayAnim(Idle.ToString());
        }

        private void Update()
        {
            talkTimer -= Time.deltaTime;
            if (talkTimer <= 0)
            {
                Talk();
                talkTimer = 30f;
            }
        }

        public void Talk()
        {
            var randomTime = Random.Range(5f, 12f);
            if (uniqueDialogue != null)
            {
                charInfo.Talk(uniqueDialogue.GetRandomDialog(), randomTime);
            }
            else if (npcData.dialogue != null)
            {
                charInfo.Talk(npcData.dialogue.GetRandomDialog(), randomTime);
            }
        }
    }
}