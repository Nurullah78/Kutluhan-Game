using System;
using UnityEngine;
using Animations;
using UnityEngine.AI;
using WarScene;

namespace Movement
{
    public class MoveCharacter : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField]
         float maximumSpeed, rotationSpeed;
        [SerializeField]
         Transform cameraTransform;
        [SerializeField]
         VariableJoystick joystick;

        [NonSerialized]
         public Vector3 movementDirection;

        AnimationController animationController => GetComponent<AnimationController>();
        CharacterController CharacterController => GetComponent<CharacterController>();
        NavMeshAgent navMeshAgent => GetComponent<NavMeshAgent>();
        Fighter fighter => GetComponent<Fighter>();
        Mover mover => GetComponent<Mover>();

        bool isKeyboard, isJoystick;


//---------------Metotlar---------------

        public void MoveAndRotate(Vector3 movementDirection)
        {
            if (navMeshAgent.velocity != Vector3.zero)
            {
                CharacterController.enabled = false;
                navMeshAgent.enabled = true;
            }
            else
            {
                CharacterController.enabled = true;
                navMeshAgent.enabled = false;
            }

            float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
            
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                inputMagnitude /= 2.5f;
            }
            
            animationController.AnimSetInputMagnitude(inputMagnitude * maximumSpeed);
            
            movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y,Vector3.up) * movementDirection;

            movementDirection.Normalize();
            
            if (movementDirection != Vector3.zero)
            {
                animationController.AnimSetIsMoving(true);
                
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                        toRotation, rotationSpeed * Time.deltaTime);
            }
        }

        void StartMove(float horizontalInput, float verticalInput)
        {
            movementDirection = new Vector3(horizontalInput, 0, verticalInput);

            navMeshAgent.enabled = false;
            fighter.Cancel();
            MoveAndRotate(movementDirection);
        }

        bool MoveWithJoystick()
        {
            float horizontalInput = joystick.Horizontal;
            float verticalInput = joystick.Vertical;

            if (!isKeyboard && horizontalInput != 0 || verticalInput != 0)
            {
                isJoystick = true;
                StartMove(horizontalInput, verticalInput);
                return true;
            }
            else
            {
                CharacterController.enabled = true;
                navMeshAgent.enabled = true;
            }

            if (horizontalInput == 0 && verticalInput == 0 && isJoystick)
            {
                isJoystick = false;
                movementDirection = Vector3.zero;
                animationController.AnimSetIsMoving(false);
            }
            return false;
        }

        bool MoveWithKeyboard()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (!isJoystick && horizontalInput != 0 || verticalInput != 0)
            {
                isKeyboard = true;
                StartMove(horizontalInput, verticalInput);
                return true;
            }
            else
            {
                CharacterController.enabled = true;
                navMeshAgent.enabled = true;
            }

            if (horizontalInput == 0 &&  verticalInput == 0 && isKeyboard)
            {
                isKeyboard = false;
                movementDirection = Vector3.zero;
                animationController.AnimSetIsMoving(false);
            }
            return false;
        }

        void OnAnimatorMove()
        {
            Vector3 velocity = GetComponent<Animator>().deltaPosition;
                
            CharacterController.Move(velocity);
        }

        void FixedUpdate()
        {
            if (MoveWithKeyboard()) return;
            if (MoveWithJoystick()) return;
        }
    }
}
