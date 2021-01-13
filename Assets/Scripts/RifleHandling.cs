using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleHandling : MonoBehaviour
{
    public GameObject bullet;
    public GameObject gun;
    public Camera playerCamera;
    public MouseMovement mouseMovement;
    public Vector3 bulletTransformation;
    public AudioSource gunSound;
    public ParticleSystem muzzleFlash;

    //Waiting for another shot
    private float totalWaitTime = 0;
    private float waitTime = 0.1f;

    //Gun damage
    public int damage = 20;

    //Recoil
    private float recoilX;
    private float recoilY;

    //Ammunition
    public int initialRifleAmmunition = 100;
    private int rifleAmmunition;
    public int RifleAmmunition { get { return rifleAmmunition; } set { rifleAmmunition = value; } }

    // Start is called before the first frame update
    void Start()
    {
        bulletTransformation = new Vector3(1, 0, 0);
        gunSound = GetComponent<AudioSource>();
        rifleAmmunition = initialRifleAmmunition;
        totalWaitTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            totalWaitTime += Time.deltaTime;
            if (rifleAmmunition > 0 && totalWaitTime >= waitTime)
            {
                //Substracting ammunition
                rifleAmmunition--;

                //Shooting bullet
                GameObject bulletObject = ObjectPoolingManger.Instance.SpawnBullet(true,damage);
                bulletObject.transform.position = playerCamera.transform.position + (playerCamera.transform.forward * 2);
                bulletObject.transform.rotation = gun.transform.rotation;
                bulletObject.transform.Rotate(Vector3.right * -90);

                //Playing gun sound and particules
                gunSound.Play();
                muzzleFlash.Play();
                
                //Applaying recoil
                recoilX = Random.Range(-2, 2);
                recoilY = Random.Range(0, 4);
                mouseMovement.MouseRecoilX = recoilX;
                mouseMovement.MouseRecoilY = recoilY;

                //Reseting time to shoot another bullet
                totalWaitTime = 0;
            }
        }
    }
}
