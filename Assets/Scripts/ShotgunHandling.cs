using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunHandling : MonoBehaviour
{
    public GameObject bullet;
    public GameObject gun;
    public Camera playerCamera;
    public MouseMovement mouseMovement;
    public Vector3 bulletTransformation;
    public AudioSource gunSound;
    public ParticleSystem muzzleFlash;
    public RifleHandling rifle;
    public PistolHandling pistol;
    public TimeSlowSkill timeSlowSkill;
    public PauseMenu pause;

    //Waiting for another shot
    private float totalWaitTime = 0;
    private float waitTime = 1f;

    //Gun damage and bullets spread
    public int damage = 10;
    private int numerOfBullets = 8;
    private float bulletspreadX;
    private float bulletspreadY;
    private float bulletspreadZ;
    private Vector3 spread;

    //Recoil
    private float recoilX;
    private float recoilY;

    //Ammunition
    public int initialShotgunAmmunition = 30;
    private int shotgunAmmunition;
    public int ShotgunAmmunition { get { return shotgunAmmunition; } set { shotgunAmmunition = value; } }

    //Skill
    private bool isSkillRechargeActive;
    public bool IsSkillRechargeActive { get { return isSkillRechargeActive; } set { isSkillRechargeActive = value; } }

    private float skillTimer = 5;
    public float SkillTimer { get { return skillTimer; } }


    private float skillRechargeTimer = 5;
    public float SkillRechargeTimer { get { return skillRechargeTimer; } set { skillRechargeTimer = value; } }


    private bool isSkillActive = false;
    public bool IsSkillActive { get { return isSkillActive; } set { isSkillActive = value; } }

    void Start()
    {
        bulletTransformation = new Vector3(1, 0, 0);
        gunSound = GetComponent<AudioSource>();
        shotgunAmmunition = initialShotgunAmmunition;
        totalWaitTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Recharging other guns
        if (pistol.IsSkillRechargeActive == true)
        {
            pistol.SkillRechargeTimer += Time.unscaledDeltaTime;
            if (pistol.SkillRechargeTimer >= 5)
            {
                pistol.IsSkillRechargeActive = false;
            }
        }

        if (rifle.IsSkillRechargeActive == true)
        {
            rifle.SkillRechargeTimer += Time.unscaledDeltaTime;
            if (rifle.SkillRechargeTimer >= 5)
            {
                rifle.IsSkillRechargeActive = false;
            }
        }

        //Turning on the skill
        if (Input.GetKeyDown(KeyCode.Q) && isSkillActive == false && skillRechargeTimer >= 5)
        {
            isSkillActive = true;
            skillRechargeTimer = 0;
            timeSlowSkill.slowDownTime();
        }

        //Truning off the skill
        if (isSkillActive == true)
        {
            skillTimer -= Time.unscaledDeltaTime;
            if (skillTimer <= 0)
            {
                timeSlowSkill.stopSlowMotion();
                isSkillRechargeActive = true;
                isSkillActive = false;
                skillTimer = 5;
            }
        }

        // Recharging the skill
        if (isSkillRechargeActive == true)
        {
            skillRechargeTimer += Time.unscaledDeltaTime;
            if (skillRechargeTimer >= 5)
            {
                isSkillRechargeActive = false;
            }
        }



        //Shooting the gun
        totalWaitTime += Time.unscaledDeltaTime;
        if (Input.GetMouseButtonDown(0) && pause.isGamePaused == false)
        {
            if (shotgunAmmunition > 0 && totalWaitTime >= waitTime)
            {
                //Substracting ammunition
                shotgunAmmunition--;

                //Shooting bullet
                for (int i=0; i<numerOfBullets; i++)
                {
                    bulletspreadX = Random.Range(-1f, 1f);
                    bulletspreadY = Random.Range(-1f, 1f);
                    bulletspreadZ = Random.Range(-1f, 1f);
                    spread = new Vector3(bulletspreadX, bulletspreadY, bulletspreadZ);
                    GameObject bulletObject = ObjectPoolingManger.Instance.SpawnBullet(true, damage);
                    bulletObject.transform.position = playerCamera.transform.position + (playerCamera.transform.forward * 2)+spread;
                    bulletObject.transform.rotation = gun.transform.rotation;
                    bulletObject.transform.Rotate(Vector3.right * -90);

                }

                //Playing gun sound and particules
                gunSound.Play();
                muzzleFlash.Play();

                //Applaying recoil
                recoilX = Random.Range(-1, 1);
                recoilY = Random.Range(4, 10);
                mouseMovement.MouseRecoilX = recoilX;
                mouseMovement.MouseRecoilY = recoilY;

                //Reseting time to shoot another bullet
                totalWaitTime = 0;
            }
        }
    }
}
