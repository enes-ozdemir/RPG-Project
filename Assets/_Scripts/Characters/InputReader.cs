using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Characters
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        private Controls _controls;
        public Action DodgeEvent;
        public Action AttackEvent;

        private void Start()
        {
            SetControls();
        }

        private void SetControls()
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
            _controls.Player.Enable();
        }

        public void OnDodge(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            Debug.Log("Dodge Invoked");
            DodgeEvent?.Invoke();
        }
        
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            Debug.Log("Attack Invoked");
            AttackEvent?.Invoke();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

    }
}