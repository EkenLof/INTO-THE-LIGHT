using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool inventory;
    public bool light = false;
    public bool isLightEquip; // samma f�r att s�tta layer V�rde
    public bool flashlight;
    public bool lighter;
    [SerializeField] bool timeFreeze;

    public GameObject inventoryMenu;
    public GameObject lighterObj;
    public GameObject flashlightObj;

    public AnimCtrl animCtrl;


    void Start()
    {
        inventoryMenu.SetActive(false);
        lighterObj.SetActive(false);
        flashlightObj.SetActive(false);

        animCtrl = GameObject.FindWithTag("Player").GetComponent<AnimCtrl>();
    }
    void Update()
    {
        bool isLighterKeys = Input.GetKeyDown("f");
        bool isInventory = Input.GetKeyDown("tab");

        if (isLighterKeys && !light && isLightEquip) // om man har ig�ng Lighter eller Light
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
        isLightEquip = true;
        lighter = true;
        flashlight = false;
        animCtrl.isLighterObj = true;
        animCtrl.isFlashlightObj = false;
    }
    public void InventoryFlashlight()
    {
        isLightEquip = true;
        flashlight = true;
        lighter = false;
        animCtrl.isFlashlightObj = true;
        animCtrl.isLighterObj = false;
    }
}
