using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimCtrl : MonoBehaviour
{
    [Header ("Player Anim Controller")]
    [SerializeField] static Animator anim;
    public LayerMask playerLayer;
    [Range(0f, 1f)]
    [SerializeField] float walkableDistance;

    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = .1f;
    [SerializeField] private LayerMask groundMask;

    public bool isLighterObj;
    public bool isFlashlightObj;

    static bool isWalk = false;
    static bool isWalkBack = false;
    static bool isWalkLeft = false;
    static bool isWalkRight = false;

    static bool isRun = false;
    static bool isRunLeft = false;
    static bool isRunRight = false;

    static bool isJump = false;
    static bool isJumpWalk = false;
    static bool isRunJump = false;
    static bool isCrouch = false;

    static bool isCrouchFront = false;
    static bool isCrouchLeft = false;
    static bool isCrouchRight = false;

    public bool isLighter = false;
    public bool isFlashlight = false;

    string isWalkFrontName = "isWalking";
    string isWalkBackName = "isWalkingBack";
    string isWalkLeftName = "isWalkingLeft";
    string isWalkRightName = "isWalkingRight";

    string isRunFrontName = "isRunning";
    string isRunLeftName = "isRunningLeft";
    string isRunRightName = "isRunningRight";

    string isJumpName = "isJumping";
    string isJumpWalkName = "isJumpingFromWalk";
    string isRunJumpName = "isRunningJump";
    string isCrouchName = "isCrouching";
    string isCrouchWalkFrontName = "isCrouchingWalkFront";
    string isCrouchWalkLeftName = "isCrouchingWalkLeft";
    string isCrouchWalkRightName = "isCrouchingWalkRight";

    string isLighterName = "isLighterOn";
    string isFlashlightName = "isFlashlightOn";

    private int leftArmLayer;
    private float leftArmlayerVelocity;
    [SerializeField]private float leftArmlayerSpeed = 0.35f;
    bool isLayer;

    public Inventory inventory;

    void Start()
    {
        anim = GetComponent<Animator>();
        leftArmLayer = anim.GetLayerIndex("Left Arm");
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        bool isWalkKeysFordward = Input.GetKey("w");
        bool isWalkKeysBackward = Input.GetKey("s");
        bool isWalkKeysLeft = Input.GetKey("a");
        bool isWalkKeysRight = Input.GetKey("d");
        bool isRunKeys = Input.GetKey(KeyCode.LeftShift);
        bool isLightKeys = Input.GetKeyDown("f");
        bool isJumpKeys = Input.GetKey("space");
        bool isCrouchKeys = Input.GetKeyDown(KeyCode.LeftControl);
        bool timefreeze = Time.timeScale <= 0;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        AnimationPlay();

        // Walk
        if (isWalkKeysFordward)
        {
            isWalk = true;
            isWalkBack = false;
            isWalkLeft = false;
            isWalkRight = false;

            isRun = false;
            isRunLeft = false;
            isRunRight = false;           
        }
        else if (isWalkKeysBackward)
        {
            isWalk = false;
            isWalkBack = true;
            isWalkLeft = false;
            isWalkRight = false;

            isRun = false;
            isRunLeft = false;
            isRunRight = false;           
        }
        else if (isWalkKeysLeft)
        {
            isWalk = false;
            isWalkBack = false;
            isWalkLeft = true;
            isWalkRight = false;

            isRun = false;
            isRunLeft = false;
            isRunRight = false;            
        }
        else if (isWalkKeysRight)
        {
            isWalk = false;
            isWalkBack = false;
            isWalkLeft = false;
            isWalkRight = true;

            isRun = false;
            isRunLeft = false;
            isRunRight = false;          
        }
        else
        {
            isWalk = false;
            isWalkBack = false;
            isWalkLeft = false;
            isWalkRight = false;

            isRun = false;
            isRunLeft = false;
            isRunRight = false;           
        }
        // Walk
        // Run
        if (isWalkKeysFordward && isRunKeys)
        {
            isWalk = true;
            isWalkBack = false;
            isWalkLeft = false;
            isWalkRight = false;

            isRun = true;
            isRunLeft = false;
            isRunRight = false;           
        }
        else if (isWalkKeysLeft && isRunKeys)
        {
            isWalk = false;
            isWalkBack = false;
            isWalkLeft = true;
            isWalkRight = false;

            isRun = false;
            isRunLeft = true;
            isRunRight = false;          
        }
        else if (isWalkKeysRight && isRunKeys)
        {
            isWalk = false;
            isWalkBack = false;
            isWalkLeft = false;
            isWalkRight = true;

            isRun = false;
            isRunLeft = false;
            isRunRight = true;           
        }
        else
        {
            isRun = false;
            isRunLeft = false;
            isRunRight = false;
        }
        // Run

        // Crouch
        if (isCrouchKeys && !isCrouch) isCrouch = true;
        else if (isCrouchKeys && isCrouch) isCrouch = false;

        if (isCrouch && isWalkKeysFordward)
        {
            isCrouchFront = true;
            isCrouchLeft = false;
            isCrouchRight = false;
        }
        else if (isCrouch && isWalkKeysLeft)
        {
            isCrouchFront = false;
            isCrouchLeft = true;
            isCrouchRight = false;
        }
        else if (isCrouch && isWalkKeysRight)
        {
            isCrouchFront = false;
            isCrouchLeft = false;
            isCrouchRight = true;
        }
        else
        {
            isCrouchFront = false;
            isCrouchLeft = false;
            isCrouchRight = false;
        }
        // Crouch

        // Jump
        if (isJumpKeys) isJump = true;
        else isJump = false;

        if (isJumpKeys && isWalk) isJumpWalk = true;
        else isJumpWalk = false;

        if (isJumpKeys && isRun) isRunJump = true;
        else isRunJump = false;
        // Jump

        // Equip
        if (!timefreeze && isLightKeys && !isLighter && isLighterObj) isLighter = true;
        else if (!timefreeze && isLightKeys && isLighter) isLighter = false;

        if (!timefreeze && isLightKeys && !isFlashlight && isFlashlightObj) isFlashlight = true;
        else if (!timefreeze && isLightKeys && isFlashlight) isFlashlight = false;
        // Equip
        /*
        float currentLeftArmLayer = anim.GetLayerWeight(leftArmLayer);
        float targetLeftArmLayer = 0;
        if (isLightKeys && isFlashlight && isLayer || isLightKeys && isLighter && isLayer)  // Kolla FEELLL???
        {
            anim.SetLayerWeight(leftArmLayer, 0.9f);
            if(inventory.light == false)
                isLayer = false; // Kolla Fel???
        }
        else if (!isFlashlight && !isLayer || !isLighter && !isLayer) 
        {
            anim.SetLayerWeight(leftArmLayer, Mathf.SmoothDamp(currentLeftArmLayer, targetLeftArmLayer, ref leftArmlayerVelocity, leftArmlayerSpeed));
            if (isLightKeys)
                isLayer = true;
        }*/
    }

    void AnimationPlay()
    {
        // Walk
        if (isWalk) anim.SetBool(isWalkFrontName, true);
        if (!isWalk) anim.SetBool(isWalkFrontName, false);

        if (isWalkBack) anim.SetBool(isWalkBackName, true);
        if (!isWalkBack) anim.SetBool(isWalkBackName, false);

        if (isWalkLeft) anim.SetBool(isWalkLeftName, true);
        if (!isWalkLeft) anim.SetBool(isWalkLeftName, false);

        if (isWalkRight) anim.SetBool(isWalkRightName, true);
        if (!isWalkRight) anim.SetBool(isWalkRightName, false);
        // Walk

        // Run
        if (isRun) anim.SetBool(isRunFrontName, true);
        if (!isRun) anim.SetBool(isRunFrontName, false);

        if (isRunLeft) anim.SetBool(isRunLeftName, true);
        if (!isRunLeft) anim.SetBool(isRunLeftName, false);

        if (isRunRight) anim.SetBool(isRunRightName, true);
        if (!isRunRight) anim.SetBool(isRunRightName, false);
        // Run
        // Jump
        if(isJump) anim.SetBool(isJumpName, true);
        if(!isJump) anim.SetBool(isJumpName, false);

        if (isRunJump) anim.SetBool(isRunJumpName, true);
        if (!isRunJump) anim.SetBool(isRunJumpName, false);
        if (isJumpWalk) anim.SetBool(isJumpWalkName, true);
        if (!isJumpWalk) anim.SetBool(isJumpWalkName, false);

        // Crouch
        if (isCrouch) anim.SetBool(isCrouchName, true);
        if (!isCrouch) anim.SetBool (isCrouchName, false);

        if (isCrouchFront) anim.SetBool(isCrouchWalkFrontName, true);
        if (!isCrouchFront) anim.SetBool(isCrouchWalkFrontName, false);

        if (isCrouchLeft) anim.SetBool(isCrouchWalkLeftName, true);
        if (!isCrouchLeft) anim.SetBool(isCrouchWalkLeftName, false);

        if (isCrouchRight) anim.SetBool(isCrouchWalkRightName, true);
        if (!isCrouchRight) anim.SetBool(isCrouchWalkRightName, false);

        // Light
        if (isLighter) anim.SetBool(isLighterName, true);
        if (!isLighter) anim.SetBool(isLighterName, false);

        if (isFlashlight) anim.SetBool(isFlashlightName, true);
        if (!isFlashlight) anim.SetBool(isFlashlightName, false);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (anim)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            RaycastHit hit;
            Ray ray = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, walkableDistance + 1f, playerLayer))
            {
                if (isGrounded)
                {
                    Vector3 footPosition = hit.point;
                    footPosition.y += walkableDistance;
                    anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
            ray = new Ray(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, walkableDistance + 1f, playerLayer))
            {
                if (isGrounded)
                {
                    Vector3 footPosition = hit.point;
                    footPosition.y += walkableDistance;
                    anim.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
        }
    }
}
