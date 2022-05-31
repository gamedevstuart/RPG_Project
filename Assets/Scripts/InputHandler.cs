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
        public float rollInputTimer;

        public bool b_input;
        public bool isInteracting;
        public bool rollFlag;
        public bool sprintFlag;

        PlayerControls inputActions;                                                    // From Player Controller Input Manager.
        CameraHandler cameraHandler;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Start()
        {
            cameraHandler = CameraHandler.singleton;
        }

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);

            }
        }

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
            HandleRollInput(delta);
        }

        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {
            b_input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;

            if (b_input)
            {
                // rollFlag = true;
                rollInputTimer += delta;
                sprintFlag = true;
            }
            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }
    }
}
