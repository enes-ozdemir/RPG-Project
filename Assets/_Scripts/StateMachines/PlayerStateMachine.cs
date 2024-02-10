// using _Scripts;
// using _Scripts.Characters;
// using _Scripts.StateMachines;
// using JohnStairs.RCC.Character;
// using JohnStairs.RCC.Character.MMO;
// using UnityEngine;
//
//     public class PlayerStateMachine : StateMachine
//     {
//         public InputReader inputReader;
//         public RPGMotorMMO _rpgMotor;
//         public RPGController RpgController;
//         [SerializeField] private float _movementSpeed;
//         public float _dodgeDuration;
//         public float _dodgeDistance;
//         public Animator _animator;
//         public Transform Transform; 
//         public bool isBusy = false;
//
//         private void Awake()
//         {
//             // RPGController.OnMovementStart += () => SwitchState(new PlayerMoveState(this));
//             // RPGController.OnMovementStop += () => SwitchState(new PlayerIdleState(this));
//             // _rpgMotor = GetComponent<RPGMotorMMO>();
//             // RpgController = GetComponent<RPGController>();
//             // _animator = GetComponent<Animator>();
//             // _movementSpeed = _rpgMotor.RunSpeed;
//             // SwitchState(new PlayerIdleState(this));
//             // ToggleMovement(false);
//         }
//
//         private void Start()
//         {
//             // // Subscribe to the DodgeEvent in inputReader
//             // inputReader.DodgeEvent += OnDodge;
//             // inputReader.AttackEvent += OnAttack;
//             //
//             // // Rest of your existing Start() code
//             // _movementSpeed = _rpgMotor.RunSpeed;
//         }
//
//         private void ToggleMovement(bool canMove) => RpgController.ToggleMovement(canMove);
//
//         public void SetBusy(bool busy)
//         {
//             isBusy = busy;
//             ToggleMovement(!busy);
//         }
//         public void PlayAnim(string animName) => _animator.Play(animName);
//         public void StopAnim() => _animator.StopPlayback();
//
//         private void OnAttack()
//         {
//             if(isBusy) return;
//             SwitchState(new PlayerMeleeStage(this));
//         }
//
//         private void OnDodge()
//         {
//             if(isBusy) return;
//             Transform = transform;
//             SwitchState(new PlayerDodgeState(this));
//         }
//     }
