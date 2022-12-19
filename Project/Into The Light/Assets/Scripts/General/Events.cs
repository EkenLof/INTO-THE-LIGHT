using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    //string player = "Player";

    [Header("active value")]
    public int steps;

    float interactionRange = 1.75f;
    float distance;
    string objName = "ActionObject";
    string flashlightName = "flashlightObject";
    string lighterName = "lighterObject";

    [Header("checkboxes")]
    [SerializeField] bool isIconsInScene;

    [Header("Asign")]
    [SerializeField] GameObject flashlight;
    [SerializeField] GameObject lighter;
    [SerializeField] GameObject flashlightInventory;
    [SerializeField] GameObject lighterInventory;

    [SerializeField] GameObject key;
    [SerializeField] GameObject fuse_10A;
    [SerializeField] GameObject fuse_16A;
    [SerializeField] GameObject step4;
    [SerializeField] GameObject step5;
    [SerializeField] GameObject step6;

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
                    }
                }
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
                    }
                }
            }
        }
        else iconsPlayer.setCrossMouse(true);


        if (steps == 1) 
        {
            Debug.Log("key Collected");
            key.SetActive(false); //Key
            steps++;
        }
        if (steps == 3)
        {
            Debug.Log("fuse 10A Collected");
            fuse_10A.SetActive(false); //fuse10a
            steps++;
        }
        if (steps > 4)
        {
            Debug.Log("fuse 16A in locker");
        }
        if (steps == 5)
        {
            Debug.Log("fuse 16A Collected");
            fuse_16A.SetActive(false); //fuse16a
            steps++;
        }
        if (steps == 7)
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
