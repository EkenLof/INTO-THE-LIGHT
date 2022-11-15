using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool inventory;
    public bool light = false;
    [SerializeField] bool flashlight;
    [SerializeField] bool lighter;
    [SerializeField] bool timeFreeze;

    public GameObject inventoryMenu;
    public GameObject lighterObj;
    public GameObject flashlightObj;


    void Start()
    {
        inventoryMenu.SetActive(false);
        lighterObj.SetActive(false);
        flashlightObj.SetActive(false);
    }
    void Update()
    {
        bool isLighterKeys = Input.GetKeyDown("f");
        bool isInventory = Input.GetKeyDown("tab");

        if (isLighterKeys && !light)
        {
            light = true; 
            LighterSystem();
        }
        else if (isLighterKeys && light)
        {
            light = false;
            LighterSystem();
        }

        if (isInventory && !inventory)
        {
            inventory = true;
            timeFreeze = true;
            TimeFreeze();
        }
        else if (isInventory && inventory)
        {
            inventory = false;
            timeFreeze = false;
            TimeFreeze();
        }
    }

    public void TimeFreeze()
    {
        if(timeFreeze)
        {
            inventoryMenu.SetActive(true);
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if(!timeFreeze)
        {
            inventoryMenu.SetActive(false);
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    public void LighterSystem()
    {
        if(light && lighter) lighterObj.SetActive(true);
        else if(!light && lighter) lighterObj.SetActive(false);

        if (light && flashlight) flashlightObj.SetActive(true);
        else if (!light && flashlight) flashlightObj.SetActive(false);
    }

    public void InventoryLighter()
    {
        lighter = true;
        flashlight = false;
    }
    public void InventoryFlashlight()
    {
        flashlight = true;
        lighter = false;
    }
}
