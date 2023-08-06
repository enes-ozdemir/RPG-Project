using UnityEngine;

namespace _Scripts.StateMachines
{
    public class PlayerMeleeStage : PlayerBaseState
    {
        private PlayerStateMachine _playerStateMachine;
        private float _attackDuration = 0.5f;

        public float cooldownTime = 2f;
        private float nextFireTime = 0;
        public static int noOfClicks = 0;
        private float _lastClickedTime = 0;
        private float _maxComboDelay = 1;

        public PlayerMeleeStage(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
            _playerStateMachine = playerStateMachine;
        }

        public override void Enter()
        {
            Debug.Log("PlayerMeleeStage enter");

            // _playerStateMachine.inputReader.DodgeEvent += OnDodge;
            //EventManager.TriggerEvent(GameEvent.pl);
            OnAttack();
        }

        private void OnAttack()
        {
            Debug.Log("OnAttack enter");

            _lastClickedTime = Time.time;
            noOfClicks++;

            if (noOfClicks == 1)
            {
                _playerStateMachine.PlayAnim("Attack0");
            }

            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

            if (noOfClicks >= 2 &&
                _playerStateMachine._animator.GetCurrentAnimatorStateInfo(0).IsName("Attack0")
                && _playerStateMachine._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                _playerStateMachine.PlayAnim("Attack1");
            }

            if (noOfClicks >= 3 &&
                _playerStateMachine._animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")
                && _playerStateMachine._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                _playerStateMachine.PlayAnim("Attack2");
                
            }
         
        }

        public override void Tick(float deltaTime)
        {
            if (_playerStateMachine._animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
                && _playerStateMachine._animator.GetCurrentAnimatorStateInfo(0).IsName("Attack0"))
            {
                _playerStateMachine.PlayAnim("Attack0");
            }

            if (_playerStateMachine._animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
                && _playerStateMachine._animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                _playerStateMachine.PlayAnim("Attack1");
            }

            if (_playerStateMachine._animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
                && _playerStateMachine._animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                _playerStateMachine.PlayAnim("Attack2");
                noOfClicks = 0;
            }

            if (Time.time - _lastClickedTime > _maxComboDelay)
            {
                noOfClicks = 0;
                _playerStateMachine._animator.SetTrigger("AttackStopped");
                _playerStateMachine.SwitchState(new PlayerIdleState(_playerStateMachine));
            }
               
            if (Time.time > nextFireTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //nextFireTime = Time.time + cooldownTime;
                    OnAttack();
                }
            }
        }

        public override void Exit()
        {
            Debug.Log("PlayerMeleeStage Exit");
            _playerStateMachine.StopAnim();
        }
    }
}