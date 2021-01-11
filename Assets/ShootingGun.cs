using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGun : MonoBehaviour
{

    public GameObject bullet;
    public GameObject gun;
    public Camera playerCamera;
    public Vector3 bulletTransformation;
    // Start is called before the first frame update
    void Start()
    {
        bulletTransformation = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletObject = Instantiate(bullet);
            bulletObject.transform.position = playerCamera.transform.position + (playerCamera.transform.forward*2) + (playerCamera.transform.right/5) - (playerCamera.transform.up/6);
            bulletObject.transform.rotation = gun.transform.rotation;
            bulletObject.transform.Rotate(Vector3.right * -90);
        }

    }
}
