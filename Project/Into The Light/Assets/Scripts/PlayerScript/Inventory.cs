using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Prefab:")]
    public GameObject inventoryMenu;

    [Header ("Items from Inventory:")]
    public GameObject lighterObj;
    public GameObject flashlightObj;

    [Header ("Player:")]
    public AnimCtrl animCtrl;
    public Animator animator;

    [Header("Actions:")]
    public bool inventory;
    public bool lights = false;
    [SerializeField] bool lightLighter;
    [SerializeField] bool lightFlashlight;
    public bool isLightEquip; // samma för att sätta layer Värde
    public bool flashlight;
    public bool lighter;
    [SerializeField] bool timeFreeze;

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

        if (isLighterKeys && !lights && isLightEquip && !timeFreeze) // om man har igång Lighter eller Light
        {
            lights = true; 
            LighterSystem();
        }
        else if (isLighterKeys && lights && !timeFreeze)
        {
            lights = false;
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

        if (animator.GetBool(lightLighterName) == true || animator.GetBool(lightFlashlightName) == true) lights = true;
        else lights = false;
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
        if(lights && lighter && !flashlight) lighterObj.SetActive(true);
        else if(!lights && lighter && !flashlight) lighterObj.SetActive(false);

        if (lights && flashlight && !lighter) flashlightObj.SetActive(true);
        else if (!lights && flashlight && !lighter) flashlightObj.SetActive(false);
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
