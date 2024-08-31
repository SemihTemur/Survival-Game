using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseKontrolleri : MonoBehaviour
{ 
    public float mouseSensivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;
    public Camera mainCamera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (EnvanterSistemiKontrolleri.Instance.acikMi == false && ÝþçilikSistemiKontrolleri.Instance.açýkMý==false)
        {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

                xRotation -= mouseY;

                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                yRotation += mouseX;

                transform.localRotation = Quaternion.Euler(transform.rotation.x, yRotation, 0f);
                mainCamera.transform.localRotation = Quaternion.Euler(xRotation, transform.rotation.y, 0f);


        }  

    }

}
