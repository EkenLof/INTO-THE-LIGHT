using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool lighter = false;

    public GameObject lighterObj;


    void Start()
    {
        lighterObj.SetActive(false);
    }
    void Update()
    {
        bool isLighterKeys = Input.GetKeyDown("f");

        if (isLighterKeys && !lighter)
        {
            lighter = true; 
            LighterSystem();
        }
        else if (isLighterKeys && lighter)
        {
            lighter = false;
            LighterSystem();
        }
    }

    public void LighterSystem()
    {
        if(lighter) lighterObj.SetActive(true);
        if(!lighter) lighterObj.SetActive(false);

    }
}
