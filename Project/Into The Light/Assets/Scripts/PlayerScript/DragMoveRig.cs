using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMoveRig : MonoBehaviour
{

    public GameObject playerCamera;

    [Header("Action Values")]
    public float interactionRange = 3;
    public float interactionDoorRange = 3;
    float distance;
    public float holdingDistance;
    public float holdingObjectDistance = 2;
    public float holdingDoorDistance = 1;
    public float forceAmount = 50f;

    public Vector3 lockedRot;

    public bool lookOnObject;

    [SerializeField] bool door;
    [SerializeField] bool obj;
    [SerializeField] bool kasta;
    [SerializeField] bool kastaDoor;
    [SerializeField] bool lockedDoor;

    [Header("Icons")]
    public Texture2D cross;
    public Texture2D grabHand;
    public Texture2D openHand;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;
    
    private GameObject objectHold;
    private bool isObjectHold;

    Rigidbody rgBody;
    Rigidbody rgDoor;

    void Start()
    {
        isObjectHold = false;
        objectHold = null;
        lookOnObject = false;
        setCrossMouse();

        door = false;
        obj = false;
        kasta = false;
        kastaDoor = false;
        lockedDoor = true;

        Cursor.lockState = CursorLockMode.Locked; //?? Låsa i mitten vid start ??

        if (rgDoor != null) rgBody = GameObject.FindGameObjectWithTag("ObjectToPickUp").GetComponent<Rigidbody>(); //2022ITTL
        objectHold = GameObject.FindGameObjectWithTag("ObjectToPickUp");

        Debug.Log(rgDoor);
        if (rgDoor != null) rgDoor = GameObject.FindGameObjectWithTag("Door").GetComponent<Rigidbody>(); //2022ITTL

    }

    void Update()
    {
        
       if (rgDoor != null) distance = Vector3.Distance(playerCamera.transform.position, objectHold.transform.position); //2022ITTL

        // 2022
        if (rgDoor != null) lockedRot = rgDoor.transform.eulerAngles; //2022ITTL
        if (lockedRot.x == 270 && lockedDoor) rgDoor.isKinematic = true;
        else if (rgDoor != null) rgDoor.isKinematic = false; //2022ITTL

        if (Input.GetMouseButton(0))
        {
            if (!isObjectHold) tryPickObject();
            else holdObject();
            
            // 2022
            if (Input.GetMouseButton(1) && kasta)
            {
                if (rgBody != null) rgBody.AddForce(transform.forward * forceAmount, ForceMode.Acceleration);

                isObjectHold = false;

                setCrossMouse();

                objectHold.GetComponent<Rigidbody>().useGravity = true;
            }
            if (Input.GetMouseButton(1) && kastaDoor)
            {
                rgDoor.AddForce(transform.forward * forceAmount, ForceMode.Acceleration);

                isObjectHold = false;

                setCrossMouse();

                objectHold.GetComponent<Rigidbody>().useGravity = true;
            }
            // 2022
        }
        else
        {
            if (distance <= interactionRange) setOpenHand(); else setCrossMouse();
            kasta = false;
            kastaDoor = false;
            lockedDoor = true;
        }

        if (Input.GetMouseButtonUp(0) && isObjectHold)
        {
            isObjectHold = false;

            setCrossMouse();

            objectHold.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void tryPickObject()
    {
        Ray playerAim = playerCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast (playerAim, out hit, interactionRange))
        {
            if (hit.collider.tag == "ObjectToPickUp")
            {
                isObjectHold = true;
                obj = true;
                door = false;

                kasta = true;
                kastaDoor = false;

                if (obj) holdingDistance = holdingObjectDistance;

                objectHold = hit.collider.gameObject;
                objectHold.GetComponent<Rigidbody>().useGravity = false;

                setGrabHand();
            }
        }
        else kasta = false;

        if (Physics.Raycast(playerAim, out hit, interactionDoorRange))
        {
            if (hit.collider.tag == "Door")
            {
                isObjectHold = true;
                door = true;
                obj = false;
                kasta = false;
                lockedDoor = false;
                kastaDoor = true;


                if (door) holdingDistance = holdingDoorDistance;

                objectHold = hit.collider.gameObject;
                objectHold.GetComponent<Rigidbody>().useGravity = false;

                setGrabHand();
            }
        }
    }

    private void holdObject()
    {
        //kasta = true;
        Ray playerAim = playerCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 nextPos = playerCamera.transform.position + playerAim.direction * holdingDistance;
        Vector3 currPos = objectHold.transform.position;

        objectHold.GetComponent<Rigidbody>().velocity = (nextPos - currPos) * 10; 
    }

    public void setCrossMouse()
    {
        Cursor.SetCursor(cross, hotspot, cursorMode);
        Cursor.visible = true;
    }
    public void setOpenHand()
    {
        Cursor.SetCursor(openHand, hotspot, cursorMode);
        Cursor.visible = true;
    }
    public void setGrabHand()
    {
        Cursor.SetCursor(grabHand, hotspot, cursorMode);
        Cursor.visible = true;
    }
}
