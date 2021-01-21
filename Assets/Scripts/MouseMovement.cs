using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public Transform playerBody;
    public MenuController menu;
    public float mouseSensitivity = 200f;
    float xRotation = 0f;

    //Recoil
    private float mouseRecoilX = 0;
    public float MouseRecoilX { get { return mouseRecoilX;} set { mouseRecoilX = value; } }

    private float mouseRecoilY = 0;
    public float MouseRecoilY { get { return mouseRecoilY; } set { mouseRecoilY = value; } }
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
            float mouseXaxis = mouseRecoilX + Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseYaxis = mouseRecoilY + Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            xRotation -= mouseYaxis;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseXaxis);
            mouseRecoilX = 0;
            mouseRecoilY = 0;
    }
}
