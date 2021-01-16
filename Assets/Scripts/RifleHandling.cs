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
    public PistolHandling pistol;
    public ShotgunHandling shotgun;

    //Waiting for another shot
    private float totalWaitTime = 0;
    private float waitTime = 0.1f;

    //Skill
    private bool isSkillRechargeActive;
    public bool IsSkillRechargeActive { get { return isSkillRechargeActive; } set { isSkillRechargeActive = value; } }

    private float skillTimer = 5;
    public float SkillTimer { get { return skillTimer; } }


    private float skillRechargeTimer = 5;
    public float SkillRechargeTimer { get { return skillRechargeTimer; } set { skillRechargeTimer = value; } }


    private bool isSkillActive = false;
    public bool IsSkillActive { get { return isSkillActive; } set { isSkillActive = value; } }

    //Gun damage
    public int damage = 20;
    public float bulletLifeTime = 20f;

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
        //Recharging other guns
        if (pistol.IsSkillRechargeActive == true)
        {
            pistol.SkillRechargeTimer += Time.deltaTime;
            if (pistol.SkillRechargeTimer >= 5)
            {
                pistol.IsSkillRechargeActive = false;
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
        //Truning off the skill
        if (isSkillActive == true)
        {      
            skillTimer -= Time.deltaTime;
            if (skillTimer <= 0)
            {
                isSkillRechargeActive = true;
                isSkillActive = false;
                skillTimer = 5;
            }
        }

       // Recharging the skill
        if (isSkillRechargeActive == true)
        {
            skillRechargeTimer += Time.deltaTime;
            if (skillRechargeTimer >= 5)
            {
                isSkillRechargeActive = false;
            }
        }

        //Shooting
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
                
                //Checking for skill and aplaying the recoil
                if (isSkillActive == false)
                {
                    recoilX = Random.Range(-2, 2);
                    recoilY = Random.Range(0, 4);
                }
                else
                {
                    recoilX = 0;
                    recoilY = 0;
                }
                mouseMovement.MouseRecoilX = recoilX;
                mouseMovement.MouseRecoilY = recoilY;

                //Reseting time to shoot another bullet
                totalWaitTime = 0;
            }
        }
    }
}
