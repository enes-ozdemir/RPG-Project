// using _Scripts.Managers;
// using DG.Tweening;
// using UnityEngine;
//
// namespace _Scripts.StateMachines
// {
//     public class PlayerDodgeState : PlayerBaseState
//     {
//         private PlayerStateMachine _playerStateMachine;
//         private Vector2 dodgeDirection;
//         private Vector2 inputDirection;
//         private float _dodgeCooldown;
//         private float dodgeDistance;
//         private float dodgeDuration = 0.5f;
//
//         public PlayerDodgeState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
//         {
//             _playerStateMachine = playerStateMachine;
//         }
//
//         public override void Enter()
//         {
//             Debug.Log("Dodge enter");
//             _playerStateMachine.SetBusy(true);
//             // _playerStateMachine.inputReader.DodgeEvent += OnDodge;
//             EventManager.TriggerEvent(GameEvent.PlayerDodge);
//             _playerStateMachine.PlayAnim("Dodge");
//             OnDodge();
//         }
//
//         private void OnDodge()
//         {
//             Debug.Log("OnDodge enter");
//             _dodgeCooldown = StateMachine._dodgeDuration;
//             var distance = StateMachine._dodgeDistance;
//
//             var dir = CalculateMovement();
//             var currentPos = _playerStateMachine.transform.position;
//             var newPos = currentPos + dir * distance;
//             //add check for cooldown
//             _playerStateMachine.Transform.DOMove(newPos, dodgeDuration).SetEase(Ease.Linear).onComplete += () =>
//             {
//                 StateMachine.SwitchState(new PlayerIdleState(StateMachine));
//             };
//         }
//
//
//         public override void Tick(float deltaTime)
//         {
//             _dodgeCooldown -= deltaTime;
//             if (_dodgeCooldown <= 0)
//             {
//                 StateMachine.SwitchState(new PlayerIdleState(StateMachine));
//             }          
//         }
//
//         private Vector3 CalculateMovement()
//         {
//             Debug.Log("CalculateMovement enter");
//             var movement = _playerStateMachine.transform.forward;
//             return movement;
//         }
//
//         public override void Exit()
//         {
//             _playerStateMachine.SetBusy(false);
//             _playerStateMachine.StopAnim();
//         }
//     }
// }