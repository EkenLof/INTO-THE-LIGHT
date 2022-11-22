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


    void Start()
    {
        dragMoveRig = playerDoorCtrl.GetComponent<DragMoveRig>();
    }

    void FixedUpdate()
    {
        if(!openDoor)
        {
            dragMoveRig.lockedDoor = true;
        }
        else if (openDoor)
        {
            dragMoveRig.lockedDoor = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == doorLockClose)
        {           
            Debug.Log("Locked" + openDoor);
            openDoor = false;

            if (!keyToDoor) unlockDoor = false;
        }
        if (other.gameObject.tag == doorUnlockOpen)
        {
            Debug.Log("Unlock" + openDoor);
            openDoor = true;

            if (!keyToDoor) unlockDoor = false;
        }
    }
}
