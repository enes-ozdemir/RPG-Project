using System;
using MalbersAnimations;
using MalbersAnimations.Controller;
using UnityEngine;

namespace _Scripts.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private MalbersInput _inputSystem;
        private MAnimal _animalController;

        public static Action<bool> onInputToggle;
        public static Action<string, bool> OnInputActionToggle;

        private void OnEnable()
        {
            onInputToggle += ToggleInput;
            OnInputActionToggle += ToggleInputAction;
        }

        private void OnDisable()
        {
            onInputToggle -= ToggleInput;
            OnInputActionToggle -= ToggleInputAction;
        }

        private void Awake()
        {
            _inputSystem = GetComponent<MalbersInput>();
            _animalController = GetComponent<MAnimal>();
        }

        #region Input System

        private void ToggleInput(bool isInputAllowed)
        {
            if (_inputSystem == null)
            {
                Debug.LogError("Input System is not assigned");
                return;
            }
            _inputSystem.enabled = isInputAllowed;
        }

        private void ToggleInputAction(string inputName, bool isEnabled)
        {
            if (inputName == null)
            {
                Debug.LogError("Input System is not assigned");
                return;
            }
            
            if (isEnabled) _inputSystem.EnableInput(inputName);
            else _inputSystem.DisableInput(inputName);
        }
        #endregion

        #region State

        public void SetAnimationState(int state)
        {
            var stateID = _animalController.states[state].ID;
            _animalController.State_Activate(stateID);
        }

        public int GetAnimationState()
        {
            return _animalController.ActiveState.ID;
        }

        #endregion

        #region Modes

        public void SetMode(int mode)
        {
            _animalController.SetModeStatus(mode);
        }
        

        #endregion

        

    }
}