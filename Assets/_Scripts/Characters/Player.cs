using System;
using _Scripts.Managers;
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

        private void OnEnable()
        {
            EventManager.AddListener(GameEvent.PlayerMoved,MoveDebug);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(GameEvent.PlayerMoved,MoveDebug);
        }

        private void MoveDebug()
        {
            Debug.Log("Moveeeee");
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            EventManager.TriggerEvent(GameEvent.PlayerMoved);

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            var movement = new Vector3(horizontalInput, 0f, verticalInput);
            movement.Normalize(); // Normalize the movement vector to ensure consistent speed

            transform.position += movement * speed * Time.deltaTime;
            
            _animator.Play("Sprint");
            
        }
        
        
    }
}