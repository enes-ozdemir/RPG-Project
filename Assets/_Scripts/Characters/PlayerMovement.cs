using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Characters
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpSpeed;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        private void Move()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");

            if (horizontalInput == 0 && verticalInput == 0)
            {
                StopMoving();
                return;
            }

            ContinueMoving(horizontalInput, verticalInput);
        }

        private void StopMoving()
        {
            EventManager.TriggerEvent(GameEvent.PlayerStopped);
            _animator.Play("Idle");
        }

        private void ContinueMoving(float horizontalInput, float verticalInput)
        {
            EventManager.TriggerEvent(GameEvent.PlayerMoving);

            var movement = new Vector3(horizontalInput, 0f, verticalInput);
            movement.Normalize();
            transform.position += movement * (speed * Time.deltaTime);
            _animator.Play("Sprint");
        }


        private void Jump()
        {
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }
}