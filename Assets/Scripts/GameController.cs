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
    public Image skillShowcase;

    void Update()
    {
        //Displaying health and ammo
        heathText.text = "Health: " + (int)player.Health;

        if (currentWeapon.CurrentWeapon == 0)
            ammunitionText.text = "Ammo: " + rifle.RifleAmmunition;
        else if (currentWeapon.CurrentWeapon == 1)
            ammunitionText.text = "Ammo: " + shotgun.ShotgunAmmunition;
        else if (currentWeapon.CurrentWeapon == 2)
            ammunitionText.text = "Ammo: " + pistol.PistolAmmunition;

        //Displaying rifle skill bar
        if (rifle.IsSkillActive == false && currentWeapon.CurrentWeapon == 0 && rifle.IsSkillRechargeActive == false)
        {
            skillShowcase.fillAmount = 1;
        }

        if (rifle.IsSkillActive == true && currentWeapon.CurrentWeapon == 0)
        {
            skillShowcase.fillAmount = rifle.SkillTimer / 5;
        }

        if (rifle.IsSkillRechargeActive == true && currentWeapon.CurrentWeapon == 0)
        {
            skillShowcase.fillAmount = rifle.SkillRechargeTimer / 5;
        }

        //Displaying shotgun skill bar
        if (shotgun.IsSkillActive == false && currentWeapon.CurrentWeapon == 1 && shotgun.IsSkillRechargeActive == false)
        {
            skillShowcase.fillAmount = 1;
        }

        if (shotgun.IsSkillActive == true && currentWeapon.CurrentWeapon == 1)
        {
            skillShowcase.fillAmount = shotgun.SkillTimer / 5;
        }

        if (shotgun.IsSkillRechargeActive == true && currentWeapon.CurrentWeapon == 1)
        {
            skillShowcase.fillAmount = shotgun.SkillRechargeTimer / 5;
        }

        //Displaying pistol skill bar
        if (pistol.IsSkillActive == false && currentWeapon.CurrentWeapon == 2 && pistol.IsSkillRechargeActive == false)
        {
            skillShowcase.fillAmount = 1;
        }

        if (pistol.IsSkillActive == true && currentWeapon.CurrentWeapon == 2)
        {
            skillShowcase.fillAmount = pistol.SkillTimer / 5;
        }

        if (pistol.IsSkillRechargeActive == true && currentWeapon.CurrentWeapon == 2)
        {
            skillShowcase.fillAmount = pistol.SkillRechargeTimer / 5;
        }
    }
}
