using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public Rigidbody controllerSystem;
    private Vector3 controllInput;


    public GameObject playerScale;

    public bool isCrouch = false;
    public bool isForward;

    public float speed = 1.2f;
    public float runSpeed = 3.75f;
    //public float gravity = -18.81f;
    public float jumpHeight = 4f;

    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    //bool isWalkKeys = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
    //bool isRunKeys = Input.GetKey(KeyCode.LeftShift);

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        // RIGID
        controllInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        MovePlayer();

        /* Vector3 move = transform.right * x + transform.forward * z;

        //------------------------------------------------------------------------------------------------------------------
        /*if (Input.GetKey(KeyCode.LeftShift) && !isCrouch) controller.Move(move * runSpeed * Time.deltaTime);
        else controller.Move(move * speed * Time.deltaTime);*/
        //------------------------------------------------------------------------------------------------------------------

        /*if (Input.GetButtonDown("Jump") && isGrounded && !isCrouch) velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);*/

        /*if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            playerScale.gameObject.transform.localScale += new Vector3(0, -1, 0);
            isCrouch = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            playerScale.gameObject.transform.localScale += new Vector3(0, 1, 0);
            isCrouch = false;
        }*/

        void MovePlayer()
        {
            Vector3 moveVector;

            if (Input.GetKey(KeyCode.LeftShift)) 
            {
                moveVector = transform.TransformDirection(controllInput) * runSpeed;

            } else  moveVector = transform.TransformDirection(controllInput) * speed;

            
            controllerSystem.velocity = new Vector3(moveVector.x, controllerSystem.velocity.y, moveVector.z);

            if (Input.GetButtonDown("Jump") && isGrounded && !isCrouch) controllerSystem.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
}
