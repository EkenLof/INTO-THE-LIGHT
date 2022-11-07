using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody controllerSystem;
    private Vector3 controllInput;

    public GameObject playerScale;

    public bool isCrouch = false;
    public bool isForward;

    public float speed = 1.2f;
    public float runSpeed = 3.75f;
    public float jumpHeight = 4f;

    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        bool isRunKeys = Input.GetKey(KeyCode.LeftShift);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        // RIGID
        controllInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        MovePlayer();

        void MovePlayer()
        {
            Vector3 moveVector;

            if (isRunKeys && isGrounded && !isCrouch) moveVector = transform.TransformDirection(controllInput) * runSpeed;
            else moveVector = transform.TransformDirection(controllInput) * speed;

            controllerSystem.velocity = new Vector3(moveVector.x, controllerSystem.velocity.y, moveVector.z);

            // Sprinting controll Smooth
            //for (float i = 1.2f; i < runSpeed; i+=1 * Time.deltaTime)
            //{
                //Debug.Log(i);
            //}


            if (Input.GetButtonDown("Jump") && isGrounded && !isCrouch) controllerSystem.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
}
