using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGun : MonoBehaviour
{

    public GameObject bullet;
    public GameObject gun;
    public Camera playerCamera;
    public Vector3 bulletTransformation;
    public AudioSource gunSound;
    public GameObject cylinder;
    public AmmoPickup ammo;

    public int initialAmmunition = 100;
    private int ammunition;
    public int Ammunition { get { return ammunition; } }


    public int initialHealth = 100;
    private int health;
    public int Health { get { return health; } }

    // Start is called before the first frame update
    void Start()
    {
        bulletTransformation = new Vector3(1, 0, 0);
        gunSound = GetComponent<AudioSource>();
        ammunition = initialAmmunition;
        health = initialHealth;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          if (ammunition > 0)
            {
                ammunition--;
                GameObject bulletObject = ObjectPoolingManger.Instance.SpawnBullet(true);
                bulletObject.transform.position = playerCamera.transform.position + (playerCamera.transform.forward * 2) + (playerCamera.transform.right / 5) - (playerCamera.transform.up / 6);
                bulletObject.transform.rotation = gun.transform.rotation;
                bulletObject.transform.Rotate(Vector3.right * -90);
                gunSound.Play();
            }
            
        }
    }

    void OnTriggerEnter(Collider characterCollider)
    {  
        if (characterCollider.GetComponent<BulletLogic>() != null)
        {
            BulletLogic bullet = characterCollider.GetComponent<BulletLogic>();
            if (bullet.ShotByPlayer == false)
            {
                bullet.gameObject.SetActive(false);
                health -= bullet.enemyBulletDamage;
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.collider.name);
        if (hit.collider.GetComponent<AmmoPickup>() != null)
        {
            
            AmmoPickup ammoPickup = hit.collider.GetComponent<AmmoPickup>();
            ammunition += ammoPickup.ammo;
            Destroy(ammoPickup.gameObject);
        }
    }
}
