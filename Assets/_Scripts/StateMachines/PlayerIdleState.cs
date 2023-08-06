using UnityEngine;

namespace _Scripts.StateMachines
{
    public class PlayerIdleState : PlayerBaseState
    {
        private PlayerStateMachine _playerStateMachine;

        public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            _playerStateMachine = playerStateMachine;
        }

        public override void Enter()
        {
            _playerStateMachine.PlayAnim("Idle");
            Debug.Log("Entered Idle State");
        }

        public override void Tick(float deltaTime)
        {
            
        }

        public override void Exit()
        {
            Debug.Log("Exited Idle State");
            _playerStateMachine.StopAnim();
        }
    }
}