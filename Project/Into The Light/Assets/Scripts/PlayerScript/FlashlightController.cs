using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    private float xRotation = 0f;
    [SerializeField] float mouseSens = 100f;
    [SerializeField] float lookUp = -20f;
    [SerializeField] float lookDown = 20f;
    public GameObject flasjlightLight;
    bool islightOn;

    void Update()
    {
        bool isflashlightLight = Input.GetMouseButtonDown(1);
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, lookUp, lookDown);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (isflashlightLight && !islightOn)
        {
            flasjlightLight.SetActive(false);
            islightOn = true;
        }
        else if (isflashlightLight && islightOn)
        { 
            flasjlightLight.SetActive(true);
            islightOn = false;
        }
    }
}
