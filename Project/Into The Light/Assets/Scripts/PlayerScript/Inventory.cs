using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool inventory;
    public bool light = false;
    [SerializeField] bool lightLighter;
    [SerializeField] bool lightFlashlight;
    public bool isLightEquip; // samma för att sätta layer Värde
    public bool flashlight;
    public bool lighter;
    [SerializeField] bool timeFreeze;

    public GameObject inventoryMenu;
    public GameObject lighterObj;
    public GameObject flashlightObj;

    public AnimCtrl animCtrl;
    public Animator animator;

    string lightLighterName = "isLighterOn";
    string lightFlashlightName = "isFlashlightOn";

    void Start()
    {
        inventoryMenu.SetActive(false);
        lighterObj.SetActive(false);
        flashlightObj.SetActive(false);

        animCtrl = GameObject.FindWithTag("Player").GetComponent<AnimCtrl>();
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }
    void Update()
    {
        bool isLighterKeys = Input.GetKeyDown("f");
        bool isInventory = Input.GetKeyDown("tab");

        if (isLighterKeys && !light && isLightEquip && !timeFreeze) // om man har igång Lighter eller Light
        {
            light = true; 
            LighterSystem();
        }
        else if (isLighterKeys && light && !timeFreeze)
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

        if (animator.GetBool(lightLighterName) == true || animator.GetBool(lightFlashlightName) == true)
            light = true;
        else light = false;
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
        if(light && lighter && !flashlight) lighterObj.SetActive(true);
        else if(!light && lighter && !flashlight) lighterObj.SetActive(false);

        if (light && flashlight && !lighter) flashlightObj.SetActive(true);
        else if (!light && flashlight && !lighter) flashlightObj.SetActive(false);
    }

    public void InventoryLighter()
    {
        flashlightObj.SetActive(false);
        lighterObj.SetActive(true);
        isLightEquip = true;
        lighter = true;
        flashlight = false;
        animCtrl.isLighterObj = true;
        animCtrl.isFlashlightObj = false;
        animCtrl.isFlashlight = false;
        animCtrl.isLighter = false;
    }
    public void InventoryFlashlight()
    {
        lighterObj.SetActive(false);
        flashlightObj.SetActive(true);
        isLightEquip = true;
        flashlight = true;
        lighter = false;
        animCtrl.isFlashlightObj = true;
        animCtrl.isLighterObj = false;
        animCtrl.isFlashlight = false;
        animCtrl.isLighter = false;
    }
}
