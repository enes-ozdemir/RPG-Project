using _Scripts.Data;
using _Scripts.SO;
using _Scripts.UI;
using _Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;
using Random = Enca.Extensions.Random;

namespace _Scripts.Controllers
{
    public class BotController : CharController, ICanTalk
    {
        public Dialogue uniqueDialogue;
        private CharInfo _info;
        private Bot _bot;
        private NavMeshAgent _agent;
        private float _timer;
        private float _currentActionDuration = 5f;
        private float _walkRadious;

        #region BotBehaviour

        [SerializeField] private float distanceThreshold = 2.5f;

        #endregion

        public void InitializeBot(CharInfo charInformation, Transform cityCenter, float walkRadius)
        {
            _walkRadious = walkRadius;
            _agent = GetComponent<NavMeshAgent>();
            SetBot();
            charInfo = charInformation;
            _info = Instantiate(charInformation, transform);
            GenerateRandomBot();
            SetBotAgent();
            SetBotWalk(cityCenter, walkRadius);
        }

        private void SelectSkin()
        {
            var childCount = transform.childCount;
            var randomIndex = UnityEngine.Random.Range(1, childCount);
            transform.GetChild(randomIndex).gameObject.SetActive(true);
        }

        private void SetBotWalk(Transform cityCenter, float walkRadius)
        {
            _agent.destination = GetRandomPointInCircle(cityCenter.position, walkRadius);
            //set rotation
            var lookPos = _agent.destination - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            _agent.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
            
            AnimatorController.SetBool("IsWalking",true);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            
            if (_timer >= _currentActionDuration)
            {
                _currentActionDuration = GetRandomActionDuration();
                _timer = 0;
                SetBotWalk(transform, _walkRadious);
            }

            if (_agent.remainingDistance < distanceThreshold)
                AnimatorController.SetBool("IsWalking",false);

            if (_agent.speed < 2)
                AnimatorController.SetBool("IsWalking",false);
        }

        private Vector3 GetRandomPointInCircle(Vector3 cityCenterPosition, float walkRadius)
        {
            float angle = Random.GetRandomNumber(360);
            float distance = Random.GetRandomNumber((int)walkRadius);
            float x = cityCenterPosition.x + distance * Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = cityCenterPosition.z + distance * Mathf.Sin(angle * Mathf.Deg2Rad);
            var point = new Vector3(x, cityCenterPosition.y, z);
            //  Debug.Log("Selected point is " + point);
            return point;
        }

        private int GetRandomActionDuration()
        {
            var index = Random.GetRandomNumber(4);
            if (index == 0)
            {
                return Random.GetRandomNumber(45);
            }

            return Random.GetRandomNumber(25);
        }

        private void SetBotAgent() => _agent.speed = _bot.stats.MovementSpeed;

        private void SetBot()
        {
            var level = UnityEngine.Random.Range(1, 99);
            var botName = RpgUtil.GetRandomRpgName();
            //  Log.Info("New Bot generated level: " + level + " name: " + botName);
            var stats = RpgUtil.GetRandomStats(level);
            _bot = new Bot(level, botName, stats);
            SelectSkin();
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