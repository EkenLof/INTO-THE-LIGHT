using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Events : MonoBehaviour
{
    [Header("Active value")]
    public int steps;
    public bool isFlashlight;
    public bool isLighter;
    public bool isKey1;
    public bool isFuse10A;
    public bool isFuse16A;
    public bool isFalling = false;
    public bool isF2PhoneRinging = false;
    public bool isToF2Stairway = false;
    public bool isToGfPhone = false;
    public bool isItemPickedUp = false;

    public bool isKey1PickUp;
    public bool isFlashlightPickUp;
    public bool isLighterPickUp;
    public bool isFuse10aPickUp;
    public bool isFuse16aPickUp;
    public bool isKeyCardPickUp;

    float interactionRange = 1.75f;
    float distance;
    private string objName = "ActionObject";
    private string itemName = "Items";
    private string key1Name = "Key1";
    private string flashlightName = "flashlightObject";
    private string lighterName = "lighterObject";
    private string fuse10AName = "Fuse10AName";
    private string fuse16AName = "Fuse16AName";
    private string fuse10AHolderName = "Fuse10AHolderName";
    private string fuse16AHolderName = "Fuse16AHolderName";

    private string doorsLockedName1 = "DoorLocked1"; // Office fuse 10A
    private string doorsLockedName2 = "DoorLocked2"; // Office fuse 16A
    private string doorsLockedNameF2 = "DoorLockedF2"; // To F2
    private string doorsLockedNameGfDouble = "DoorLockedGFDD"; // Double door GF
    private string doorsLockedNameEnd = "DoorLockedEnd"; // Last Door END
    private string airductLockedName = "AirDuctLocked"; // Airduct Locked

    [Header("Timers")]
    [SerializeField] private float itemPickUpTime = 1f;
    [SerializeField] private float maxItemPickUpTime = 1f;

    [Header("checkboxes")]
    [SerializeField] bool isIconsInScene;

    [Header("Asign")]
    [SerializeField] GameObject flashlight;
    [SerializeField] GameObject lighter;
    [SerializeField] GameObject flashlightInventory;
    [SerializeField] GameObject lighterInventory;
    [SerializeField] GameObject fuse10AToBox;
    [SerializeField] GameObject fuse16AToBox;
    [Header("-Doors-")]
    [SerializeField] GameObject doorUnlocked1;
    [SerializeField] GameObject doorLocked1;
    [SerializeField] GameObject doorUnlocked2;
    [SerializeField] GameObject doorLocked2;
    [SerializeField] GameObject doorUnlockedF2;
    [SerializeField] GameObject doorLockedF2;
    [SerializeField] GameObject doorUnlockedGfDoubleL;
    [SerializeField] GameObject doorUnlockedGfDoubleR;
    [SerializeField] GameObject doorLockedGfDouble;
    [SerializeField] GameObject doorUnlockedEnd;
    [SerializeField] GameObject doorLockedEnd;
    [SerializeField] GameObject airductUnlocked;
    [SerializeField] GameObject airductLocked;
    [Header("-Lights-")]
    [SerializeField] GameObject lightsGF;
    //[SerializeField] GameObject lightsGFOff; //Senare
    [SerializeField] GameObject lightsF1;
    [SerializeField] GameObject lightsF1Off;
    [SerializeField] GameObject lightsF1Halls;
    [SerializeField] GameObject lightsF1HallsOff;
    [SerializeField] GameObject lightsF2;
    [SerializeField] GameObject lightsF2Off;
    [SerializeField] GameObject lightsWasherRoom;
    //[SerializeField] GameObject lightsWasherRoomOff; /Fixa
    [SerializeField] GameObject lightsDressingRoom;
    //[SerializeField] GameObject lightsDressingRoomOff; /Fixa
    [SerializeField] GameObject lightsNurseRoom;
    //[SerializeField] GameObject lightsNurseRoomOff; /Fixa
    [SerializeField] GameObject lightsLab;
    //[SerializeField] GameObject lightsLabOff; /Fixa
    [SerializeField] GameObject lightsOffice1;
    //[SerializeField] GameObject lightsOffice1Off; /Fixa
    [SerializeField] GameObject lightsOffice2;
    //[SerializeField] GameObject lightsOffice2Off; /Fixa
    [Header("-Items-")]
    [SerializeField] GameObject key;
    [SerializeField] GameObject fuse_10A;
    [SerializeField] GameObject fuse_16A;
    [Header("-Obejcts-")]
    [SerializeField] GameObject phoneDefault;
    [SerializeField] GameObject phoneDead;

    [Header("Player")]
    public GameObject cameraPlayer;
    public DragMoveRig iconsPlayer;
    public AnimCtrl animPlayer;

    Ray playerAim;
    RaycastHit hit;

    void Start()
    {
        Debug.Log("start!");
        steps = 0;

        if (isIconsInScene) iconsPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<DragMoveRig>();
        if (isIconsInScene) animPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<AnimCtrl>();

        doorUnlocked1.SetActive(false);
        doorUnlocked2.SetActive(false);
        doorUnlockedF2.SetActive(false);
        doorUnlockedGfDoubleL.SetActive(false);
        doorUnlockedGfDoubleR.SetActive(false);
        doorUnlockedEnd.SetActive(false);
        airductUnlocked.SetActive(false);

        lightsF1.SetActive(false);
        lightsF1Halls.SetActive(false);
        lightsF2.SetActive(false);

        lightsF1Off.SetActive(true);
        lightsF1HallsOff.SetActive(true);
        lightsF2Off.SetActive(true);

        lightsWasherRoom.SetActive(false);
        lightsDressingRoom.SetActive(false);
        lightsNurseRoom.SetActive(false);
        lightsLab.SetActive(false);
        lightsOffice1.SetActive(false);
        lightsOffice2.SetActive(false);
        phoneDead.SetActive(false);
    }

    
    void Update()
    {
        bool leftClick = Input.GetMouseButton(0);
        bool leftClickDown = Input.GetMouseButtonDown(0);

        playerAim = cameraPlayer.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (itemPickUpTime <= -0.6f) isItemPickedUp = false;

        if (Physics.Raycast(playerAim, out hit, interactionRange))
        {
            if (hit.collider.tag == objName && distance <= interactionRange)
            {
                iconsPlayer.setOpenHand(true);
                if(leftClick && steps == 2 || leftClick && steps == 4 || leftClick && steps == 6 || leftClick && steps == 8) steps++;
            }
            // Key
            else if (hit.collider.tag == key1Name && distance <= interactionRange ||
                    hit.collider.tag == fuse10AName && distance <= interactionRange ||
                    hit.collider.tag == fuse16AName && distance <= interactionRange)
            { // && steps == 3 //10
                iconsPlayer.setOpenHand(true);
                if (leftClickDown) 
                {
                    // ANIM PLAY
                    animPlayer.isPickUpItem = true;
                    // Time Bool Active
                    isItemPickedUp = true;
                }
                if (itemPickUpTime <= 0.1f)
                {
                    Debug.Log("key Collected");
                    key.SetActive(false); //Key
                    isKey1 = true;
                    steps++;
                }
                if (itemPickUpTime <= 0.1f && steps == 3)
                {
                    Debug.Log("fuse 10A Collected");
                    fuse_10A.SetActive(false); //fuse10a
                    isFuse10A = true;
                    steps++;
                }
                if (itemPickUpTime <= 0.1f && steps == 10)
                {
                    Debug.Log("fuse 16A Collected");
                    fuse_16A.SetActive(false); //fuse16a
                    isFuse16A = true;
                    steps++;
                }
                if (itemPickUpTime <= 0f) // TIME 0
                {
                    // RESET TIME // ANIM STOP
                    isItemPickedUp = false;
                    animPlayer.isPickUpItem = false;
                }
            }
            // Flashlight
            else if (hit.collider.tag == flashlightName && distance <= interactionRange)
            {
                iconsPlayer.setOpenHand(true);
                if (leftClickDown) 
                {
                    // ANIM PLAY
                    animPlayer.isPickUpItem = true;
                    // Time Bool Active
                    isItemPickedUp = true;
                }
                if (itemPickUpTime <= 0.1f)
                {
                    flashlight.SetActive(false);
                    Debug.Log("Flashlight Collected");
                    flashlightInventory.SetActive(true);
                    isFlashlight = true;
                }
                if (itemPickUpTime <= 0f) // TIME 0
                {
                    // RESET TIME // ANIM STOP
                    isItemPickedUp = false;
                    animPlayer.isPickUpItem = false;
                }
            }
            // Lighter
            else if (hit.collider.tag == lighterName && distance <= interactionRange)
            {
                iconsPlayer.setOpenHand(true);
                if (leftClickDown)
                {
                    // ANIM PLAY
                    animPlayer.isPickUpItem = true;
                    // Time Bool Active
                    isItemPickedUp = true;
                }
                if (itemPickUpTime <= 0.1f)
                {
                    lighter.SetActive(false);
                    Debug.Log("Lighter Collected");
                    lighterInventory.SetActive(true);
                    isLighter = true;
                }
                if (itemPickUpTime <= 0f) // TIME 0
                {
                    // RESET TIME // ANIM STOP
                    isItemPickedUp = false;
                    animPlayer.isPickUpItem = false;
                }
            }          
            // Fuses
            else if (hit.collider.tag == fuse10AHolderName && distance <= interactionRange)
            {
                iconsPlayer.setOpenHand(true);
                if (leftClickDown && isFuse10A)
                {
                    // ANIM PLAY
                    animPlayer.isPickUpItem = true;
                    // Time Bool Active
                    isItemPickedUp = true;
                }
                if (itemPickUpTime <= 0.1f)
                {
                    fuse10AToBox.SetActive(true);
                    Debug.Log("Fuse 10A Placed");
                    steps++;
                    isFuse10A = false;
                }
                if (itemPickUpTime <= 0f) // TIME 0
                {
                    // RESET TIME // ANIM STOP
                    isItemPickedUp = false;
                    animPlayer.isPickUpItem = false;
                }
            }

            else if (hit.collider.tag == fuse16AHolderName && distance <= interactionRange)
            {
                iconsPlayer.setOpenHand(true);
                if (leftClickDown && isFuse16A)
                {
                    // ANIM PLAY
                    animPlayer.isPickUpItem = true;
                    // Time Bool Active
                    isItemPickedUp = true;
                }
                if (itemPickUpTime <= 0.1f)
                {
                    fuse16AToBox.SetActive(true);
                    Debug.Log("Fuse 16A Placed");
                    steps++;
                    isFuse16A = false;
                }
                if (itemPickUpTime <= 0f) // TIME 0
                {
                    // RESET TIME // ANIM STOP
                    isItemPickedUp = false;
                    animPlayer.isPickUpItem = false;
                }
            }
            // Fuses
            // Doors
            else if (hit.collider.tag == doorsLockedName1 && distance <= interactionRange) // After KEY1 collected
            {
                iconsPlayer.setOpenHand(true);
                if (leftClick && isKey1)
                {
                    doorUnlocked1.SetActive(true);
                    doorLocked1.SetActive(false);
                    Debug.Log("Door Unlocked");
                    isKey1 = false;
                }
            }
            else
            {
                iconsPlayer.setCrossMouse(true);
                // ANIM STOP
                animPlayer.isPickUpItem = false;
            }
            // Doors

            // Timer for ALL items to be Picked Up.
            if (isItemPickedUp) itemPickUpTime -= Time.deltaTime;
            else itemPickUpTime = maxItemPickUpTime;
            // Timer for ALL items to be Picked Up.

        }
        else iconsPlayer.setCrossMouse(true); // Kanske rätt lagd, ANNARS innanför (ovan)

        // STEPS starting
        if (steps == 5) // Fuse 10A in box
        {
            Debug.Log("fuse 10A in locker");
            lightsF1.SetActive(true);
            lightsF1Halls.SetActive(true);
            lightsF1Off.SetActive(false);
            lightsF1HallsOff.SetActive(false);
            lightsOffice1.SetActive(true);
            lightsOffice2.SetActive(true);
            doorUnlockedF2.SetActive(true);
            doorLockedF2.SetActive(false);
            lightsF2.SetActive(true);
            Debug.Log("DoorF2 Opens");
            steps++;
        }

        if (steps == 6 && isToF2Stairway) // Door to F2 + sounds BOX TRIGGERD
        {
            Debug.Log("Kvävt Hostande -Cole-");

        }

        if (isF2PhoneRinging) // Phone rings BOX TRIGGERD
        {
            Debug.Log("Phone Rings");
            //AudioPlay
            phoneDefault.SetActive(false);
            phoneDead.SetActive(true);
            steps++;
        }

        if (steps == 7 && isToGfPhone) // Phones dead, but haning over table, blood mark on the Table // Trigger Box
        {
            Debug.Log("Sees Phone dead and hanging, Blood on Table");
            steps++;
        }

        if (steps == 8)
        {
            Debug.Log("Scream!!! From Upstairs Office 2");
            steps++;
        }

        if(steps == 9) // Door Open Office 2 // 
        {
            Debug.Log("Door Open Office 2");
            doorUnlocked2.SetActive(true);
            doorLocked2.SetActive(false);
        }

        if (steps == 11) // Fuse 16A in box
        {
            Debug.Log("fuse 16A in locker");
            
        }
    }

    /*void OnTriggerEnter(Collider other) // if (col1.CompareTag(player))
    {

        if (other.CompareTag(player))
        {

            Debug.Log("Enter");
            Destroy(step1);
            steps++;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == player)
        {
            Debug.Log("Exit");
            
        }
    }*/
}
