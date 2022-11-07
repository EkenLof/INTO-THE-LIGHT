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

    public float speed = 1.6f;
    public float runSpeed = 3.75f;
    public float jumpHeight = 4f;

    public Transform groundCheck;
    public Transform wallCheck;
    public float groundDistance = .1f;
    public float wallDistance = .3f;
    public LayerMask groundMask;
    public LayerMask wallMask;

    public float smoothRunTransition = 3f;

    Vector3 velocity;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isWall;

    void Update()
    {
        bool isRunKeys = Input.GetKey(KeyCode.LeftShift);
        bool isJump = Input.GetButtonDown("Jump");

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isWall = Physics.CheckSphere(wallCheck.position, wallDistance, wallMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        // RIGID
        controllInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        MovePlayer();

        void MovePlayer()
        {
            Vector3 moveVector;

            if (!isWall && isGrounded || isWall && isGrounded || isGrounded)
            {
                if (isRunKeys && isGrounded && !isCrouch) moveVector = transform.TransformDirection(controllInput) * runSpeed;
                else moveVector = transform.TransformDirection(controllInput) * speed;

                controllerSystem.velocity = new Vector3(moveVector.x, controllerSystem.velocity.y, moveVector.z);
            }
            // Sprinting controll Smooth
            //for (float i = 1.2f; i < runSpeed; i+=1 * Time.deltaTime)
            //{
                //Debug.Log(i);
            //}


            if (isJump && isGrounded && !isCrouch) controllerSystem.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
}
