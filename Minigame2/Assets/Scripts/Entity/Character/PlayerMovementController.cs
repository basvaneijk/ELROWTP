using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Entity.Character
{
    [RequireComponent(typeof (Rigidbody))]
    public class PlayerMovementController : MonoBehaviour
    {
        //Speed/Sensitivity
        private const float MouseSensitivityX = 250f;
        private const float MouseSensitivityY = 250f;

        private static float MovementSpeed
        {
            get
            {
                const float baseMovementSpeed = 10f;
                float movementSpeed = baseMovementSpeed;

                //This is for debugging
                if (Input.GetButton(KeyBindingHelper.Boost))
                {
                    movementSpeed = baseMovementSpeed * 10f;
                }

                return movementSpeed;
            }
        }

        //Others
        private Vector3 moveAmount;
        private Vector3 smoothMoveVelocity;
        private float verticalLookRotation;

        private void Update()
        {
            //Camera
            CameraControl();

            //Movement
            MovementControl();
        }

        private void FixedUpdate()
        {
            GetComponent<Rigidbody>()
                .MovePosition(GetComponent<Rigidbody>().position +
                              transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        }

        private void CameraControl()
        {
            transform.Rotate(Vector3.up * Input.GetAxis(KeyBindingHelper.MouseX) * Time.deltaTime *
                             MouseSensitivityX);
            verticalLookRotation += Input.GetAxis(KeyBindingHelper.MouseY) * Time.deltaTime * MouseSensitivityY;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
            GameObject.FindGameObjectWithTag(TagHelper.MainCamera).transform.localEulerAngles = Vector3.left *
                                                                                                verticalLookRotation;
        }

        private void MovementControl()
        {
            Vector3 moveDirection =
                new Vector3(Input.GetAxisRaw(KeyBindingHelper.Horizontal), 0,
                            Input.GetAxisRaw(KeyBindingHelper.Vertical))
                    .normalized;
            moveAmount = Vector3.SmoothDamp(moveAmount, moveDirection * MovementSpeed, ref smoothMoveVelocity, .15f);
        }
    }
}