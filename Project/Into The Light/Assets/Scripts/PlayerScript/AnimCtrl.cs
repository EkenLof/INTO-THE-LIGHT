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

    [Header("Setup")]
    public Inventory menuHInventory;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float leftArmlayerSpeed = 0.35f;
    private int leftArmLayer;
    private float leftArmlayerVelocity;
    [Header("Check Boxes")]
    [SerializeField] bool isCutscene = false;
    [SerializeField] bool isInventoryInScene = false;

    bool isLayer;

    [Header("Actions")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundDistance = .1f;
    
    public bool isLighterObj;
    public bool isFlashlightObj;

    static bool isWalk = false;
    static bool isWalkBack = false;
    static bool isWalkLeft = false;
    static bool isWalkRight = false;

    static bool isWalkFrontLeft = false;
    static bool isWalkFrontRight = false;

    static bool isRun = false;
    static bool isRunLeft = false;
    static bool isRunRight = false;

    static bool isJump = false;
    static bool isJumpWalk = false;
    static bool isRunJump = false;
    static bool isCrouch = false;

    static bool isCrouchWithLight = false;

    static bool isCrouchFront = false;
    static bool isCrouchBack = false;
    static bool isCrouchLeft = false;
    static bool isCrouchRight = false;

    static bool isCrawl = false;
    static bool isCrawlFront = false;

    public bool isLighter = false;
    public bool isFlashlight = false;

    public bool isPickUpItem = false;

    public bool isFallClimb = false;

    string iSittingCarName = "isSittingCar";

    string isWalkFrontName = "isWalking";
    string isWalkBackName = "isWalkingBack";
    string isWalkLeftName = "isWalkingLeft";
    string isWalkRightName = "isWalkingRight";

    string isWalkFrontLeftName = "isWalkingLF";
    string isWalkFrontRightName = "isWalkingRF";

    string isRunFrontName = "isRunning";
    string isRunLeftName = "isRunningLeft";
    string isRunRightName = "isRunningRight";

    string isJumpName = "isJumping";
    string isJumpWalkName = "isJumpingFromWalk";
    string isRunJumpName = "isRunningJump";
    string isCrouchName = "isCrouching";
    string isCrouchWalkFrontName = "isCrouchingWalkFront";
    string isCrouchWalkBackName = "isCrouchingWalkBack";
    string isCrouchWalkLeftName = "isCrouchingWalkLeft";
    string isCrouchWalkRightName = "isCrouchingWalkRight";

    string isCrawlName = "isCrawling";
    string isCrawlFrontName = "isCrawlingForward";

    string isLighterName = "isLighterOn";
    string isFlashlightName = "isFlashlightOn";
    string isLightsName = "isLights";

    string isPickUpItemName = "isPickUpItems";

    string isFallClimbName = "isFallingClimbing";



    [Header("Timers")]
    [SerializeField] private float jumpTime = .8f;
    [SerializeField] private float maxJumpTime = .8f;
    [SerializeField] private float itemPickUpTime = .8f;
    [SerializeField] private float maxItemPickUpTime = .8f;
    [SerializeField] private float crawlingTime = 3f;
    [SerializeField] private float maxCrawlingTime = 3f;

    void Start()
    {
        anim = GetComponent<Animator>();
        leftArmLayer = anim.GetLayerIndex("Left Arm");
        if(isInventoryInScene) menuHInventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
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
        bool isCrouchDownKeys = Input.GetKey(KeyCode.LeftControl);
        bool isCrouchUpKeys = Input.GetKeyUp(KeyCode.LeftControl);
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

            isWalkFrontLeft = false;
            isWalkFrontRight = false;

            isRun = false;
            isRunLeft = false;
            isRunRight = false;           
        }
        // Walk
        // Walk Front-Sideways
        if (isWalkKeysFordward && isWalkKeysLeft) // Front Left
        {
            isWalk = true;
            isWalkBack = false;
            isWalkLeft = true; // ??
            isWalkRight = false;

            isRun = false;
            isRunLeft = false;
            isRunRight = false;
            isWalkFrontLeft = true;
        }
        else if (isWalkKeysFordward && isWalkKeysRight) // Front Right
        {
            isWalk = true;
            isWalkBack = false;
            isWalkLeft = false;
            isWalkRight = true; // ??

            isRun = false;
            isRunLeft = false;
            isRunRight = false;
            isWalkFrontRight = true;
        }
        else
        {
            isWalkFrontLeft = false;
            isWalkFrontRight = false;
        }
        // Walk Front-Sideways
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
        if (isCrouchKeys && !isCrouch)
        {
            isCrouch = true;
            
            
        }
        else if (isCrouchKeys && isCrouch) 
        {
            isCrouch = false;
            isCrawl = false;
        }

        // Crawl
        if (!isCrouchUpKeys && isCrouchDownKeys && isCrouch)
        {
            crawlingTime -= Time.deltaTime;
            if (crawlingTime <= 0f)
            {
                isCrawl = true;
            }
        }
        if (isCrouchUpKeys) crawlingTime = maxCrawlingTime;
        if (isCrawl) crawlingTime = maxCrawlingTime;

        // Crawl Move
        if (isCrawl && isWalkKeysFordward) isCrawlFront = true;
        else isCrawlFront = false;

        // Crouch Move
        if (isCrouch && isWalkKeysFordward)
        {
            isCrouchFront = true;
            isCrouchBack = false;
            isCrouchLeft = false;
            isCrouchRight = false;
        }
        else if (isCrouch && isWalkKeysLeft)
        {
            isCrouchFront = false;
            isCrouchBack = false;
            isCrouchLeft = true;
            isCrouchRight = false;
        }
        else if (isCrouch && isWalkKeysRight)
        {
            isCrouchFront = false;
            isCrouchBack = false;
            isCrouchLeft = false;
            isCrouchRight = true;
        }
        else if (isCrouch && isWalkKeysBackward)
        {
            isCrouchFront = false;
            isCrouchBack = true;
            isCrouchLeft = false;
            isCrouchRight = false;
        }
        else
        {
            isCrouchFront = false;
            isCrouchBack = false;
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

        // Weight Ctrl
        float currentLeftArmLayer = anim.GetLayerWeight(leftArmLayer);
        float targetLeftArmLayer = 0;
        if (isLighter && anim.GetBool(isLightsName) || isFlashlight && anim.GetBool(isLightsName)) anim.SetLayerWeight(leftArmLayer, 0.9f);
        else if (!anim.GetBool(isLightsName)) anim.SetLayerWeight(leftArmLayer, Mathf.SmoothDamp(currentLeftArmLayer, targetLeftArmLayer, ref leftArmlayerVelocity, leftArmlayerSpeed));

        if (isLighter && isLightKeys && !isLayer || isFlashlight && isLightKeys && !isLayer)
        {
            anim.SetBool(isLightsName, true);
            isLayer = true;
        }
        else if (isLightKeys && isLayer)
        {
            anim.SetBool(isLightsName, false);
            isLayer = false;
        }
        // Weight Ctrl
    }

    void AnimationPlay()
    {
        if (isCutscene) anim.SetBool(iSittingCarName, true);
        if (!isCutscene) anim.SetBool(iSittingCarName, false);

        // Walk
        if (!isCutscene && isWalk) anim.SetBool(isWalkFrontName, true);
        if (!isCutscene && !isWalk) anim.SetBool(isWalkFrontName, false);

        if (!isCutscene && isWalkBack) anim.SetBool(isWalkBackName, true);
        if (!isCutscene && !isWalkBack) anim.SetBool(isWalkBackName, false);

        if (!isCutscene && isWalkLeft) anim.SetBool(isWalkLeftName, true);
        if (!isCutscene && !isWalkLeft) anim.SetBool(isWalkLeftName, false);

        if (!isCutscene && isWalkRight) anim.SetBool(isWalkRightName, true);
        if (!isCutscene && !isWalkRight) anim.SetBool(isWalkRightName, false);
        // Walk
        // Walk Left-Front
        if (!isCutscene && isWalkFrontLeft) anim.SetBool(isWalkFrontLeftName, true);
        if (!isCutscene && !isWalkFrontLeft) anim.SetBool(isWalkFrontLeftName, false);
        // Walk Left-Front
        // Walk Right-Front
        if (!isCutscene && isWalkFrontRight) anim.SetBool(isWalkFrontRightName, true);
        if (!isCutscene && !isWalkFrontRight) anim.SetBool(isWalkFrontRightName, false);
        // Walk Right-Front
        // Run
        if (!isCutscene && isRun) anim.SetBool(isRunFrontName, true);
        if (!isCutscene && !isRun) anim.SetBool(isRunFrontName, false);

        if (!isCutscene && isRunLeft) anim.SetBool(isRunLeftName, true);
        if (!isCutscene && !isRunLeft) anim.SetBool(isRunLeftName, false);

        if (!isCutscene && isRunRight) anim.SetBool(isRunRightName, true);
        if (!isCutscene && !isRunRight) anim.SetBool(isRunRightName, false);
        // Run
        // Jump
        if(!isCutscene && isJump) anim.SetBool(isJumpName, true);
        if(!isCutscene && !isJump) anim.SetBool(isJumpName, false);

        if (!isCutscene && isRunJump) 
        {
            anim.SetBool(isRunJumpName, true);
            jumpTime -= Time.deltaTime;

            if (jumpTime <= 0f)
            {
                anim.SetBool(isRunJumpName, false);
            }
        }
        if (!isCutscene && !isRunJump) 
        {
            anim.SetBool(isRunJumpName, false);
            jumpTime = maxJumpTime;
        }

        if (!isCutscene && isJumpWalk) anim.SetBool(isJumpWalkName, true);
        if (!isCutscene && !isJumpWalk) anim.SetBool(isJumpWalkName, false);

        // Crouch
        if (!isCutscene && isCrouch) anim.SetBool(isCrouchName, true);
        if (!isCutscene && !isCrouch) anim.SetBool (isCrouchName, false);

        if (!isCutscene && isCrouchFront) anim.SetBool(isCrouchWalkFrontName, true);
        if (!isCutscene && !isCrouchFront) anim.SetBool(isCrouchWalkFrontName, false);

        if (!isCutscene && isCrouchBack) anim.SetBool(isCrouchWalkBackName, true);
        if (!isCutscene && !isCrouchBack) anim.SetBool(isCrouchWalkBackName, false);

        if (!isCutscene && isCrouchLeft) anim.SetBool(isCrouchWalkLeftName, true);
        if (!isCutscene && !isCrouchLeft) anim.SetBool(isCrouchWalkLeftName, false);

        if (!isCutscene && isCrouchRight) anim.SetBool(isCrouchWalkRightName, true);
        if (!isCutscene && !isCrouchRight) anim.SetBool(isCrouchWalkRightName, false);

        // Crawl
        if (!isCutscene && isCrawl) anim.SetBool(isCrawlName, true);
        if (!isCutscene && !isCrawl) anim.SetBool(isCrawlName, false);

        if (!isCutscene && isCrouchFront) anim.SetBool(isCrawlFrontName, true);
        if (!isCutscene && !isCrouchFront) anim.SetBool(isCrawlFrontName, false);

        // Light
        if (!isCutscene && isLighter) anim.SetBool(isLighterName, true);
        if (!isCutscene && !isLighter) anim.SetBool(isLighterName, false);

        if (!isCutscene && isFlashlight) anim.SetBool(isFlashlightName, true);
        if (!isCutscene && !isFlashlight) anim.SetBool(isFlashlightName, false);

        // Pick up Item
        if (!isCutscene && isPickUpItem) anim.SetBool(isPickUpItemName, true); // TRIGGERD by other Scripts
        if (!isCutscene && !isPickUpItem) anim.SetBool(isPickUpItemName, false); // TRIGGERD by other Scripts

        // FallingScene
        if (!isCutscene && isFallClimb) anim.SetBool(isFallClimbName, true); // TRIGGERD by other Scripts
        if (!isCutscene && !isFallClimb) anim.SetBool(isFallClimbName, false); // TRIGGERD by other Scripts
    }
    
    /*
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
    }*/
}
