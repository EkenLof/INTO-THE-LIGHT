using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DragMoveRig : MonoBehaviour
{

    public GameObject playerCamera;

    [Header("Action Values")]
    public float interactionRange = 1.5f;
    public float interactionDoorRange = 2.5f;
    public float holdingObjectDistance = 1.2f;
    public float holdingDoorDistance = 1;
    [SerializeField] float holdingDistance;
    public float forceAmount = 50f;
    float distance;
    float distanceDoor;

    public Vector3 lockedRot;

    public bool lookOnObject = false;

    [SerializeField] bool door = false;
    [SerializeField] bool obj = false;
    [SerializeField] bool kasta = false;
    [SerializeField] bool lockedDoor = true;

    [Header("Icons")]
    public Texture2D cross;
    public Texture2D grabHand;
    public Texture2D openHand;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;
    
    private GameObject objectHold;
    private bool isObjectHold = false;

    Rigidbody rgBody;
    Rigidbody rgDoor;

    private string objName = "ObjectToPickUp";
    private string doorName = "Door";

    void Start()
    {
        objectHold = null;
        setCrossMouse(true);

        Cursor.lockState = CursorLockMode.Locked; //?? Låsa i mitten vid start ??
        rgBody = GameObject.FindGameObjectWithTag(objName).GetComponent<Rigidbody>(); //2022ITTL
        objectHold = GameObject.FindGameObjectWithTag(objName);
        rgDoor = GameObject.FindGameObjectWithTag(doorName).GetComponent<Rigidbody>(); //2022ITTL
    }

    void Update()
    {
        distance = Vector3.Distance(playerCamera.transform.position, objectHold.transform.position); //2022ITTL
        distanceDoor = Vector3.Distance(playerCamera.transform.position, objectHold.transform.position); //2022ITTL

        // 2022
        lockedRot = rgDoor.transform.eulerAngles;
        if (lockedRot.x == 270 && lockedDoor) rgDoor.isKinematic = true;
        rgDoor.isKinematic = false;

        if (Input.GetMouseButton(0))
        {
            if (!isObjectHold) tryPickObject();
            else holdObject();
            
            // 2022
            if (Input.GetMouseButton(1) && kasta)
            {
                rgBody.AddForce(transform.forward * forceAmount, ForceMode.Acceleration); //2022ITTL
                isObjectHold = false;
                setCrossMouse(true);
                objectHold.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        else
        {
            if (distance <= interactionRange || distanceDoor <= interactionDoorRange) setOpenHand(true); 
            else setCrossMouse(true);
            kasta = false;
            lockedDoor = true;
        }

        if (Input.GetMouseButtonUp(0) && isObjectHold)
        {
            isObjectHold = false;
            setCrossMouse(true);
            objectHold.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void tryPickObject()
    {
        Ray playerAim = playerCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast (playerAim, out hit, interactionRange))
        {
            if (hit.collider.tag == objName)
            {
                isObjectHold = true;
                obj = true;
                door = false;
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
                isObjectHold = true;
                door = true;
                obj = false;
                kasta = false;
                lockedDoor = false;
                if (door) holdingDistance = holdingDoorDistance;
                objectHold = hit.collider.gameObject;
                objectHold.GetComponent<Rigidbody>().useGravity = false;
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
