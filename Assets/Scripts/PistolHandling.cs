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
    public RifleHandling rifle;
    public ShotgunHandling shotgun;
    public PauseMenu pause;

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

    //Waiting for another shot
    private float totalWaitTime = 0;
    private float waitTime = 1f;

    //Skill recharge
    private bool isSkillRechargeActive;
    public bool IsSkillRechargeActive { get { return isSkillRechargeActive; } set { isSkillRechargeActive = value; } }

    private float skillRechargeTimer = 5;
    public float SkillRechargeTimer { get { return skillRechargeTimer; } set { skillRechargeTimer = value; } }


    //Skill activation and timer
    private bool isSkillActive = false;
    public bool IsSkillActive { get { return isSkillActive; } set { isSkillActive = value; } }

    private float skillTimer = 0;
    public float SkillTimer { get { return skillTimer; } }


    void Start()
    {
        bulletTransformation = new Vector3(1, 0, 0);
        gunSound = GetComponent<AudioSource>();
        pistolAmmunition = initialPistolAmmunition;
        totalWaitTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Recharge of other guns
        if (rifle.IsSkillRechargeActive == true)
        {
            rifle.SkillRechargeTimer += Time.deltaTime;
            if (rifle.SkillRechargeTimer >= 5)
            {
                rifle.IsSkillRechargeActive = false;
            }
        }
        if (shotgun.IsSkillRechargeActive == true)
        {
            shotgun.SkillRechargeTimer += Time.deltaTime;
            if (shotgun.SkillRechargeTimer >= 5)
            {
                shotgun.IsSkillRechargeActive = false;
            }
        }

        //Turning on the skill
        if (Input.GetKeyDown(KeyCode.Q) && isSkillActive == false && skillRechargeTimer >= 5)
        {
            isSkillActive = true;
            skillRechargeTimer = 0;
        }

        //Recharging the skill
        if (isSkillRechargeActive == true)
        {
            skillRechargeTimer += Time.deltaTime;
            if (skillRechargeTimer >= 5)
            {
                isSkillRechargeActive = false;
            }
        }
        //Skill effect
        if (IsSkillActive == true)
        {
            damage = 300;
        }

        totalWaitTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && pause.isGamePaused == false)
        {
            if (pistolAmmunition > 0 && totalWaitTime >= waitTime)
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

                //Checking if skill was active - if yes then turn it off and disable its effect and start recharge
                if (isSkillActive == true)
                {
                    isSkillActive = false;
                    isSkillRechargeActive = true;
                    damage = 80;
                }
                //Reseting time to shoot another bullet
                totalWaitTime = 0;
            }
        }
    }
}
