using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public GameObject playerDoorCtrl;
    public DragMoveRig dragMoveRig;
    public bool openDoor = false;
    public bool unlockDoor = false;
    public bool keyToDoor;

    string doorLockClose = "ClosedOrLocked";
    string doorUnlockOpen = "OpenOrUnlocked";

    [Header("Asign")]
    public GameObject doorLock;
    public GameObject doorUnlock;

    public bool isLockDoor;
    public bool isUnlockDoor;


    void Start()
    {
        dragMoveRig = playerDoorCtrl.GetComponent<DragMoveRig>();
    }

    void FixedUpdate()
    {
        if(!openDoor) dragMoveRig.lockedDoor = true; // Closed - Locked
        else if (openDoor) dragMoveRig.lockedDoor = false; // Open - Unlocked

        if(dragMoveRig.doorOpen == true) // DoorSystem Door Open
        {
            doorLock.SetActive(false);
            doorUnlock.SetActive(true);
        }
        else //DoorSystem Door Closed
        {
            doorLock.SetActive(true);
            doorUnlock.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == doorLockClose) // Closed - Locked
        {           
            openDoor = false;
            if (!keyToDoor) unlockDoor = false;
        }
        if (other.gameObject.tag == doorUnlockOpen) // Open - Unlocked
        {
            openDoor = true;
            if (!keyToDoor) unlockDoor = false;
        }
    }
}
