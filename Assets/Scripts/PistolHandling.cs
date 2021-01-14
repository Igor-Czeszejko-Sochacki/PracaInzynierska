using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolHandling : MonoBehaviour
{ 
    public GameObject bullet;
    public GameObject gun;
    public Camera playerCamera;
    public MouseMovement mouseMovement;
    public Vector3 bulletTransformation;
    public AudioSource gunSound;
    public ParticleSystem muzzleFlash;

    //Ammunition
    public int initialPistolAmmunition = 90;
    private int pistolAmmunition;
    public int PistolAmmunition { get { return pistolAmmunition; } set { pistolAmmunition = value; } }

    //Gun damage
    public int damage = 80;
    public float bulletLifeTime = 20f;

    //Recoil
    private float recoilX;
    private float recoilY;

    void Start()
    {
        bulletTransformation = new Vector3(1, 0, 0);
        gunSound = GetComponent<AudioSource>();
        pistolAmmunition = initialPistolAmmunition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (pistolAmmunition > 0)
            {
                //Substracting ammo
                pistolAmmunition--;

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
                recoilY = Random.Range(3, 6);
                mouseMovement.MouseRecoilX = recoilX;
                mouseMovement.MouseRecoilY = recoilY;
            }
        }
    }
}
