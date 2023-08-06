using _Scripts;
using _Scripts.Characters;
using _Scripts.StateMachines;
using JohnStairs.RCC.Character;
using JohnStairs.RCC.Character.MMO;
using UnityEngine;

    public class PlayerStateMachine : StateMachine
    {
        public InputReader inputReader;
        public RPGMotorMMO _rpgMotor;
        [SerializeField] private float _movementSpeed;
        public float _dodgeDuration;
        public float _dodgeDistance;
        public Animator _animator;
        public Transform Transform; 

        private void Awake()
        {
            RPGController.OnMovementStart += () => SwitchState(new PlayerMoveState(this));
            RPGController.OnMovementStop += () => SwitchState(new PlayerIdleState(this));
            _rpgMotor = GetComponent<RPGMotorMMO>();
            _animator = GetComponent<Animator>();
            _movementSpeed = _rpgMotor.RunSpeed;
            SwitchState(new PlayerIdleState(this));
        }

        private void Start()
        {
            // Subscribe to the DodgeEvent in inputReader
            inputReader.DodgeEvent += OnDodge;
            inputReader.AttackEvent += OnAttack;

            // Rest of your existing Start() code
            _rpgMotor = GetComponent<RPGMotorMMO>();
            _animator = GetComponent<Animator>();
            _movementSpeed = _rpgMotor.RunSpeed;
        }
        
        public void PlayAnim(string animName) => _animator.Play(animName);
        public void StopAnim() => _animator.StopPlayback();

        private void OnAttack()
        {
            SwitchState(new PlayerMeleeStage(this));
        }

        private void OnDodge()
        {
            Transform = transform;
            SwitchState(new PlayerDodgeState(this));
        }
    }
