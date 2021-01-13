using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Player player;
    public RifleHandling rifle;
    public ShotgunHandling shotgun;
    public PistolHandling pistol;
    public WeaponManager currentWeapon;
    public Text ammunitionText;
    public Text heathText;
    void Update()
    {
        heathText.text = "Health: " + (int)player.Health;

        if (currentWeapon.CurrentWeapon == 0)
            ammunitionText.text = "Ammo: " + rifle.RifleAmmunition;
        else if (currentWeapon.CurrentWeapon == 1)
            ammunitionText.text = "Ammo: " + shotgun.ShotgunAmmunition;
        else if (currentWeapon.CurrentWeapon == 2)
            ammunitionText.text = "Ammo: " + pistol.PistolAmmunition;
    }
}
