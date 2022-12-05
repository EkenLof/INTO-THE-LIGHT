using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DragMoveRig : MonoBehaviour
{

    public GameObject playerCamera;

    [Header("Action Values")]
    [SerializeField] float interactionRange = 1.5f;
    [SerializeField] float interactionDoorRange = 2.5f;
    [SerializeField] float holdingObjectDistance = 1.2f;
    [SerializeField] float holdingDoorDistance = 1;
    [SerializeField] float holdingDistance;
    [SerializeField] float forceAmount = 50f;

    float distance;
    float distanceDoor;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotspot = Vector2.zero;

    [Header ("Actions")]
    //[SerializeField] bool lookOnObject = false; // Replace - Raycast view
    [SerializeField] bool door = false;
    [SerializeField] bool obj = false;
    [SerializeField] bool kasta = false;
    public bool lockedDoor;

    public bool doorOpen;

    [Header("Icons")]
    public Texture2D cross;
    public Texture2D grabHand;
    public Texture2D openHand;

    private GameObject objectHold;
    private GameObject doorHold;
    private bool isObjectHold = false;
    private bool isDoorHold = false;

    private Rigidbody rgBody = null;
    private Rigidbody rgDoor = null;

    private string objName = "ObjectToPickUp";
    private string doorName = "Door";

    [Header("Asign")]

    [Header("Checkboxes")]
    public bool isObjectsInScene;
    public bool isDoorsInScene;

    Ray playerAim;
    RaycastHit hit;

    void Start()
    {
        objectHold = null;
        setCrossMouse(true);
        doorOpen = false;

        Cursor.lockState = CursorLockMode.Locked; //?? Låsa i mitten vid start ??

        if (isObjectsInScene)
            rgBody = GameObject.FindGameObjectWithTag(objName).GetComponent<Rigidbody>(); //2022ITTL
        if (isObjectsInScene)
            objectHold = GameObject.FindGameObjectWithTag(objName);
        if (isDoorsInScene)
            rgDoor = GameObject.FindGameObjectWithTag(doorName).GetComponent<Rigidbody>(); //2022ITTL
        if (isDoorsInScene)
            doorHold = GameObject.FindGameObjectWithTag(doorName);
    }

    void LateUpdate()
    {
        bool leftClick = Input.GetMouseButton(0);
        bool rightClick = Input.GetMouseButton(1);
        bool leftClickUp = Input.GetMouseButtonUp(0);

        playerAim = playerCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (isObjectsInScene) distance = Vector3.Distance(playerCamera.transform.position, objectHold.transform.position); //2022ITTL
        if(isDoorsInScene) distanceDoor = Vector3.Distance(playerCamera.transform.position, doorHold.transform.position); //2022ITTL

        if(isDoorsInScene) // 2022 Lock???
        {
            // Sepparat script har värdet för låsning av dörren. lockedDoor true/false;
            if (lockedDoor) rgDoor.isKinematic = true;
            else rgDoor.isKinematic = false;

            if (Physics.Raycast(playerAim, out hit, interactionDoorRange))
            {
                if (hit.collider.tag == doorName)
                {
                    if (leftClick) doorOpen = true; //DMR Door Open
                    else doorOpen = false; //DMR Door closed
                }
            }
        }

        if(isObjectsInScene)
        {
            if (leftClick)
            {
                if (!isObjectHold) tryPickObject();
                else holdObject();
                
                if (rightClick && kasta)
                {
                    rgBody.AddForce(transform.forward * forceAmount, ForceMode.Acceleration); //2022ITTL
                    isObjectHold = false;
                    setCrossMouse(true);
                    objectHold.GetComponent<Rigidbody>().useGravity = true;
                }
            }
            else
            {
                if (Physics.Raycast(playerAim, out hit, interactionRange))
                {
                    if (hit.collider.tag == objName)
                    {
                        if (distance <= interactionRange) setOpenHand(true); ///
                    }
                }
                else setCrossMouse(true); ///
                kasta = false;
            }

            if (leftClickUp && isObjectHold)
            {
                isObjectHold = false;
                setCrossMouse(true);
                objectHold.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        if (isDoorsInScene)
        {
            if (leftClick)
            {
                if (!isDoorHold) tryPickObject();
                else holdDoor();
            }
            else
            {
                if (Physics.Raycast(playerAim, out hit, interactionDoorRange))
                {
                    if (hit.collider.tag == doorName)
                    {
                        if (distanceDoor <= interactionDoorRange) setOpenHand(true); ///
                    }
                }
                
                else setCrossMouse(true); ///
            }

            if (leftClickUp && isDoorHold)
            {
                isDoorHold = false;
                setCrossMouse(true);
                doorHold.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    private void tryPickObject()
    {
        playerAim = playerCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast (playerAim, out hit, interactionRange))
        {
            if (hit.collider.tag == objName)
            {
                isObjectHold = true;
                obj = true;
                kasta = true;
                if (obj) holdingDistance = holdingObjectDistance;
                objectHold = hit.collider.gameObject;
                objectHold.GetComponent<Rigidbody>().useGravity = false;
                setGrabHand(true);
            }
        }
        else kasta = false;

        if (Physics.Raycast(playerAim, out hit, interactionDoorRange))
        {
            if (hit.collider.tag == doorName)
            {
                isDoorHold = true;
                door = true;
                lockedDoor = false;
                if (door) holdingDistance = holdingDoorDistance;
                doorHold = hit.collider.gameObject;
                doorHold.GetComponent<Rigidbody>().useGravity = false;
                setGrabHand(true);
            }
        }
    }

    private void holdObject()
    {
        Ray playerAim = playerCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 nextPos = playerCamera.transform.position + playerAim.direction * holdingDistance;
        Vector3 currPos = objectHold.transform.position;

        objectHold.GetComponent<Rigidbody>().velocity = (nextPos - currPos) * 10; 
    }

    private void holdDoor()
    {
        Ray playerAim = playerCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 nextPos = playerCamera.transform.position + playerAim.direction * holdingDistance;
        Vector3 currPos = doorHold.transform.position;

        doorHold.GetComponent<Rigidbody>().velocity = (nextPos - currPos) * 10;
    }

    public void setCrossMouse(bool isCrossMouse)
    {
        if (isCrossMouse)
        {
            Cursor.SetCursor(cross, hotspot, cursorMode);
            Cursor.visible = true;
        }
    }
    public void setOpenHand(bool isOpenHand)
    {
        if (isOpenHand)
        {
            Cursor.SetCursor(openHand, hotspot, cursorMode);
            Cursor.visible = true;
        }
    }
    public void setGrabHand(bool isGrabHand)
    {
        if (isGrabHand)
        { 
            Cursor.SetCursor(grabHand, hotspot, cursorMode);
            Cursor.visible = true;
        }
    }
}
