using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stu_Dunan_RPG
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        PlayerControls inputActions;                                                    // From Player Controller Input Manager.

        Vector2 movementInput;
        Vector2 cameraInput;

        public void OnEnable()                                                          // Enable Player Control Input Manager.
        {
            if (inputActions == null)
            {   // controls
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions
                    => movementInput = inputActions.ReadValue<Vector2>();
                // camera
                inputActions.PlayerMovement.Camera.performed += i =>
                cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();                                                     // Disable input actions.
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
        }

        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
    }
}
