using System;
using JohnStairs.RCC.Character;
using UnityEngine;

namespace _Scripts.Characters
{
    public class CharacterAnimation : MonoBehaviour
    {
        private Animator _anim;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int IsSprinting = Animator.StringToHash("IsSprinting");
        private RPGController _rpgController;

        private void Awake()
        {
            _rpgController = GetComponent<RPGController>();
            _anim = GetComponent<Animator>();
            RPGController.OnMovementStart += () => MoveAnimation(true);
            RPGController.OnMovementStop += () => MoveAnimation(false);
            RPGController.OnJump += JumpAnimation;
            RPGController.IsSprinting += PlaySprintAnimation;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                DoRoll();
            }
        }

        private void DoRoll()
        {
            _rpgController.Disable();
            Debug.Log("DoRoll called");
            _anim.SetBool("IsRolling", true);
            Vector3 direction = transform.forward; // Modify this based on your needs.
            float rollSpeed = 5f; // Adjust the speed as necessary.

            transform.Translate(direction * rollSpeed * Time.deltaTime, Space.World);
        }

        public void RollEnded()
        {
            Debug.Log("RollEnded called");
            _rpgController.Enable();
            _anim.SetBool("IsRolling", false);
        }

        private void PlaySprintAnimation(bool isSprinting)
        {
            Debug.Log("Sprint animation called with " + isSprinting);
            _anim.SetBool(IsSprinting, isSprinting);
        }

        private void JumpAnimation(bool obj)
        {
            Debug.Log("Jump animation");
            _anim.SetBool(IsJumping, true);
        }

        private void MoveAnimation(bool isMoving)
        {
            Debug.Log("Move animation called with " + isMoving);
            _anim.SetBool(IsMoving, isMoving);
        }
    }
}