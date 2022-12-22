using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    //string player = "Player";

    [Header("Active value")]
    public int steps;
    public bool isFlashlight;
    public bool isLighter;
    public bool isKey1;
    public bool isFuse10A;
    public bool isFuse16A;

    float interactionRange = 1.75f;
    float distance;
    string objName = "ActionObject";
    string flashlightName = "flashlightObject";
    string lighterName = "lighterObject";
    string fuse10AHolderName = "Fuse10AHolderName";
    string fuse16AHolderName = "Fuse16AHolderName";

    string doorsLockedName1 = "DoorLocked1"; // Office fuse 10A
    string doorsLockedName2 = "DoorLocked2"; // Office fuse 16A
    string doorsLockedNameF2 = "DoorLockedF2"; // To F2
    string doorsLockedNameGfDouble = "DoorLockedGFDD"; // Double door GF
    string doorsLockedNameEnd = "DoorLockedEnd"; // Last Door

    [Header("checkboxes")]
    [SerializeField] bool isIconsInScene;

    [Header("Asign")]
    [SerializeField] GameObject flashlight;
    [SerializeField] GameObject lighter;
    [SerializeField] GameObject flashlightInventory;
    [SerializeField] GameObject lighterInventory;
    [SerializeField] GameObject fuse10AToBox;
    [SerializeField] GameObject fuse16AToBox;
    [Header("")]
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

    [SerializeField] GameObject key;
    [SerializeField] GameObject fuse_10A;
    [SerializeField] GameObject fuse_16A;
    //[SerializeField] GameObject step4;
    //[SerializeField] GameObject step5;
    //[SerializeField] GameObject step6;

    [Header("Player")]
    public GameObject cameraPlayer;
    public DragMoveRig iconsPlayer;

    Ray playerAim;
    RaycastHit hit;

    void Start()
    {
        Debug.Log("start!");
        steps = 0;

        if (isIconsInScene) iconsPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<DragMoveRig>();

        doorUnlocked1.SetActive(false);
        doorUnlocked2.SetActive(false);
        doorUnlockedF2.SetActive(false);
        doorUnlockedGfDoubleL.SetActive(false);
        doorUnlockedGfDoubleR.SetActive(false);
        doorUnlockedEnd.SetActive(false);
    }

    
    void Update()
    {
        bool leftClick = Input.GetMouseButton(0);

        playerAim = cameraPlayer.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(playerAim, out hit, interactionRange))
        {
            if (hit.collider.tag == objName)
            {
                if (distance <= interactionRange) 
                {
                    iconsPlayer.setOpenHand(true);
                    if(leftClick || leftClick && steps == 2 || leftClick && steps == 4 || leftClick && steps == 6 || leftClick && steps == 8) steps++;
                }
                else iconsPlayer.setCrossMouse(true);
            }

            else if (hit.collider.tag == flashlightName)
            {
                if (distance <= interactionRange)
                {
                    iconsPlayer.setOpenHand(true);
                    if (leftClick) 
                    {
                        flashlight.SetActive(false);
                        Debug.Log("Flashlight Collected");
                        flashlightInventory.SetActive(true);
                        isFlashlight = true;
                    }
                }
                else iconsPlayer.setCrossMouse(true);
            }
            else if (hit.collider.tag == lighterName)
            {
                if (distance <= interactionRange)
                {
                    iconsPlayer.setOpenHand(true);
                    if (leftClick) 
                    {
                        lighter.SetActive(false);
                        Debug.Log("Lighter Collected");
                        lighterInventory.SetActive(true);
                        isLighter = true;
                    }
                }
                else iconsPlayer.setCrossMouse(true);
            }
            // Fuses
            else if (hit.collider.tag == fuse10AHolderName)
            {
                if (distance <= interactionRange)
                {
                    iconsPlayer.setOpenHand(true);
                    if (leftClick && isFuse10A)
                    {
                        fuse10AToBox.SetActive(true);
                        Debug.Log("Fuse 10A Placed");
                        isFuse10A = false;
                    }
                }
                else iconsPlayer.setCrossMouse(true);
            }
            else if (hit.collider.tag == fuse16AHolderName)
            {
                if (distance <= interactionRange)
                {
                    iconsPlayer.setOpenHand(true);
                    if (leftClick && isFuse16A)
                    {
                        fuse16AToBox.SetActive(true);
                        Debug.Log("Fuse 16A Placed");
                        isFuse16A = false;
                    }
                }
                else iconsPlayer.setCrossMouse(true);
            }
            // Fuses
            // Doors
            else if (hit.collider.tag == doorsLockedName1) // After KEY1 collected
            {
                Debug.Log("L hit");
                if (distance <= interactionRange)
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
                else iconsPlayer.setCrossMouse(true);
            }
            // Doors

        }
        else iconsPlayer.setCrossMouse(true); // Kanske rätt lagd, ANNARS innanför (ovan)

        // STEPS starting
        if (steps == 1) 
        {
            Debug.Log("key Collected");
            key.SetActive(false); //Key
            isKey1 = true;
            steps++;
        }
        if (steps == 3)
        {
            Debug.Log("fuse 10A Collected");
            fuse_10A.SetActive(false); //fuse10a
            isFuse10A = true;
            steps++;
        }
        if (steps > 4)
        {
            Debug.Log("fuse 10A in locker");
        }
        if (steps == 5)
        {
            Debug.Log("fuse 16A Collected");
            fuse_16A.SetActive(false); //fuse16a
            isFuse16A = true;
            steps++;
        }
        /*if (steps == 7)
        {
            Debug.Log("item Collected");
            step4.SetActive(false); //Key
            steps++;
        }
        if (steps == 9)
        {
            Debug.Log("item Collected");
            step5.SetActive(false); //Key
            steps++;
        }
        if (steps == 11)
        {
            Debug.Log("item Collected");
            step6.SetActive(false); //Key
            steps++;
        }*/
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
