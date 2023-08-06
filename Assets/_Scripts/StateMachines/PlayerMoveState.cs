using _Scripts.Characters;
using UnityEngine;

namespace _Scripts.StateMachines
{
    public class PlayerMoveState : PlayerBaseState
    {
        private PlayerStateMachine _playerStateMachine;

        public PlayerMoveState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            _playerStateMachine = playerStateMachine;
        }

        public override void Enter()
        {
            Debug.Log("PlayerMoveState enter");
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void Exit()
        {
            Debug.Log("PlayerMoveState Exit");
        }
    }
}