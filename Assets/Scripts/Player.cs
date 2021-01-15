﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject bullet;
    public GameObject gun;
    public GameObject cylinder;  
    public RifleHandling rifleAmmo;
    public ShotgunHandling shotgunAmmo;
    public PistolHandling pistolAmmo;

    //Time passed from last hit from enemy
    public float timeFromAttack = 0;

    //Health
    public int healthRegeneration = 5;
    public int initialHealth = 100;
    private float health;
    public float Health { get { return health; } }


    void Start()
    {
        health = initialHealth;
    }

    void Update()
    {
        //Tracking time from the last hit taken
        if (timeFromAttack <= 5)
        {
            timeFromAttack += Time.deltaTime;
        }   

        //If enough time passed - regenerate health
        if (health < initialHealth && health > 0 && timeFromAttack >= 5)
        {
            health += healthRegeneration * Time.deltaTime;
        }
    }

    //Getting hit by enemy bullet
    void OnTriggerEnter(Collider characterCollider)
    {  
        if (characterCollider.GetComponent<BulletLogic>() != null)
        {
            BulletLogic bullet = characterCollider.GetComponent<BulletLogic>();
            if (bullet.ShotByPlayer == false)
            {
                bullet.gameObject.SetActive(false);
                health -= bullet.enemyBulletDamage;
                timeFromAttack = 0;
            }
        }
    }

    //Checking collision with ammo pickups and adding ammunition to all guns
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.GetComponent<AmmoPickup>() != null)
        {  
            AmmoPickup ammoPickup = hit.collider.GetComponent<AmmoPickup>();
            rifleAmmo.RifleAmmunition += ammoPickup.ammo;
            pistolAmmo.PistolAmmunition += (int)ammoPickup.ammo/2;
            shotgunAmmo.ShotgunAmmunition += (int)ammoPickup.ammo/4;
            Destroy(ammoPickup.gameObject);
        }
    }
}