using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float mouseSens = 100f;
    float smoothZoom = 2;

    public Transform playerBody;

    float xRotation = 0f;

    [SerializeField] float lookUp = -90f;
    [SerializeField] float lookDown = 90f;

    int fovDeffault = 60;
    int fovZoom = 20;

    bool isFovZoom;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isFovZoom = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, lookUp, lookDown);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if(Input.GetKeyDown(KeyCode.C))
        {
            isFovZoom = !isFovZoom;
        }
        if(isFovZoom)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, fovZoom, Time.deltaTime * smoothZoom);
        }
        else
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, fovDeffault, Time.deltaTime * smoothZoom);
        }
    }
}
