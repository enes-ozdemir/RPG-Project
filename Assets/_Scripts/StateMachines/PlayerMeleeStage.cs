// using UnityEngine;
//
// namespace _Scripts.StateMachines
// {
//     public class PlayerMeleeStage : PlayerBaseState
//     {
//         private PlayerStateMachine _playerStateMachine;
//         private float _attackDuration = 0.5f;
//
//         private float _lastClickedTime = 0;
//         private float _maxComboDelay = 1;
//         private bool isAttacking = true;
//
//         public PlayerMeleeStage(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
//         {
//             _playerStateMachine = playerStateMachine;
//         }
//
//         public override void Enter()
//         {
//             _playerStateMachine.SetBusy(true);
//
//             Debug.Log("PlayerMeleeStage enter");
//             OnAttack();
//             isAttacking = true;
//         }
//
//         private void OnAttack()
//         {
//             Debug.Log("OnAttack enter");
//             _lastClickedTime = Time.time;
//             _playerStateMachine._animator.Play("Attack0", 0, 0f); // 0f resets the animation to the start
//             isAttacking = true;
//
//          // //   if (noOfClicks == 1)
//          //    {
//          //      //  _playerStateMachine._animator.SetTrigger("AttackStarted");
//          //       // _playerStateMachine._animator.SetTrigger("Attack");
//          //    }
//          //    //
//             // if (noOfClicks == 2)
//             // {
//             //    // _playerStateMachine._animator.SetTrigger("AttackCombo");
//             //
//             //    _playerStateMachine.PlayAnim("Attack1");
//             // }
//             //
//             // if (noOfClicks == 3)
//             // {
//             //   //  _playerStateMachine._animator.SetTrigger("AttackCombo");
//             //     _playerStateMachine.PlayAnim("Attack2");
//             //     noOfClicks = 0;
//             // }
//         }
//
//         public override void Tick(float deltaTime)
//         {
//             // If the player is holding the left button and is not already attacking, initiate an attack.
//             if (Input.GetMouseButton(0) && !isAttacking)
//             {
//                 _lastClickedTime = Time.time;
//                 Debug.Log("hold worked");
//                 OnAttack();
//             }
//
//             // If the attack animation has finished, reset the isAttacking flag.
//             if (isAttacking && _playerStateMachine._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
//             {
//                 isAttacking = false;
//             }
//             //
//             if (Time.time - _lastClickedTime > _maxComboDelay)
//             {
//                 Debug.Log("hold exit");
//                 _playerStateMachine.StopAnim();
//                 _playerStateMachine._animator.SetTrigger("AttackStopped");
//                 _playerStateMachine.SwitchState(new PlayerIdleState(_playerStateMachine));
//             }
//         }
//
//         public override void Exit()
//         {
//             Debug.Log("PlayerMeleeStage Exit");
//             _playerStateMachine.SetBusy(false);
//             isAttacking = false;
//             _playerStateMachine.StopAnim();
//             _playerStateMachine._animator.SetTrigger("AttackStopped");
//         }
//     }
// }