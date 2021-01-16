using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public RifleHandling rifle;
    public PistolHandling pistol;
   // public ShotgunHandling shotgun;

    [SerializeField]
    private GameObject[] allWeapons = null;
    private int currentWeapon;
    public int CurrentWeapon { get { return currentWeapon; } }

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = 0;
        allWeapons[currentWeapon].gameObject.SetActive(true);
        allWeapons[1].gameObject.SetActive(false);
        allWeapons[2].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && pistol.IsSkillActive == false)
        {
            allWeapons[currentWeapon].gameObject.SetActive(false);
            currentWeapon = 0;
            allWeapons[currentWeapon].gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            allWeapons[currentWeapon].gameObject.SetActive(false);
            currentWeapon = 1;
            allWeapons[currentWeapon].gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && rifle.IsSkillActive == false)
        {
            allWeapons[currentWeapon].gameObject.SetActive(false);
            currentWeapon = 2;
            allWeapons[currentWeapon].gameObject.SetActive(true);
        }
    }
}
