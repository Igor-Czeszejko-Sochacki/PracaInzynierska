using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public GameObject ammoPickup;
    public int ammo;

    void Start()
    {
        //Assigning numer of bullets to ammo pickup
        ammo = Random.Range(16, 64);
    }

    void Update()
    {
        
    }
}
