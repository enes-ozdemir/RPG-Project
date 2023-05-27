using System;
using UnityEngine;

namespace _Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private string name;
        [SerializeField] private int money;

        private Animator _animator;


        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
            movement.Normalize(); // Normalize the movement vector to ensure consistent speed

            transform.position += movement * speed * Time.deltaTime;
            
            _animator.Play("Sprint");
            
        }
    }
}