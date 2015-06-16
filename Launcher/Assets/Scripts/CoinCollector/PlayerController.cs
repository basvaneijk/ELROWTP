using System;
using System.Collections;
using UnityEngine;


    [RequireComponent(typeof (Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        //Movement speed
        private const float WalkSpeed = 5f;
        private const float SprintSpeed = 13f;
        private const float BackwardMovementModifier = 0.7f;
        private const float ForwardMovementModifier = 1.5f;

        //Jumping
        private const float JumpForce = 320f;

        //Mouse sensitivity (camera)
        private const float MouseSensitivityX = 250f;
        private const float MouseSensitivityY = 250f;

        //Others
        private bool grounded;
        private Vector3 moveAmount;
        private Vector3 smoothMoveVelocity;
        private float verticalLookRotation;

        GameObject[] array;

        private void Start()
        {
            array = GameObject.FindGameObjectsWithTag("nitro");
          
        }

        private void Update()
        {
            

            //Camera
            CameraControl();

            //Movement
            MovementControl();

            //Jumping
            JumpControl();
        }


        private void FixedUpdate()
        {
            GetComponent<Rigidbody>()
                .MovePosition(GetComponent<Rigidbody>().position +
                              transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        }

        private void CameraControl()
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime *
                             MouseSensitivityX);
            verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivityY;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
      
        }

        private void MovementControl()
        {
            Vector3 moveDirection =
                new Vector3(Input.GetAxisRaw("Vertical"), 0,
                            Input.GetAxisRaw("Horizontal"))
                    .normalized;
            Vector3 targetMoveAmount = moveDirection * GetMovementSpeed();
            moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
        }

        private float GetMovementSpeed()
        {
            float movementSpeed = WalkSpeed;
            if (IsSprinting)
            {
                movementSpeed = SprintSpeed;
           
                foreach(GameObject o in array){
                    //o.GetComponent<ParticleSystem>().Play();
                    o.GetComponent<ParticleSystem>().enableEmission = true;
                }

            }
            else
            {
                foreach (GameObject o in array)
                {
                    //o.GetComponent<ParticleSystem>().Play();
                    o.GetComponent<ParticleSystem>().enableEmission = false;
                }
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                movementSpeed = movementSpeed * ForwardMovementModifier;
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                movementSpeed = movementSpeed * BackwardMovementModifier;
            }
            return movementSpeed;
        }

        public bool IsSprinting
        {
            get
            {
                return (Input.GetButton("Sprint") && !Input.GetButton("Crouch"));
            }
        }

        private void JumpControl()
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (grounded)
                {
                    GetComponent<Rigidbody>().AddForce(transform.up * JumpForce);
                }
            }

            grounded = false;
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;
          
        }

        
    }
