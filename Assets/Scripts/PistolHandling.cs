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

    //Skills
    private bool isSkillRechargeActive;
    public bool IsSkillRechargeActive { get { return isSkillRechargeActive; } set { isSkillRechargeActive = value; } }

    private float skillTimer = 0;
    public float SkillTimer { get { return skillTimer; } }


    private float skillRechargeTimer = 5;
    public float SkillRechargeTimer { get { return skillRechargeTimer; } set { skillRechargeTimer = value; } }


    private bool isSkillActive = false;
    public bool IsSkillActive { get { return isSkillActive; } set { isSkillActive = value; } }


    void Start()
    {
        bulletTransformation = new Vector3(1, 0, 0);
        gunSound = GetComponent<AudioSource>();
        pistolAmmunition = initialPistolAmmunition;
    }

    // Update is called once per frame
    void Update()
    {
        if (rifle.IsSkillRechargeActive == true)
        {
            rifle.SkillRechargeTimer += Time.deltaTime;
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

        if (IsSkillActive == true)
        {
            damage = 300;
        }

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

                isSkillActive = false;
                isSkillRechargeActive = true;
                damage = 80;
            }
        }
    }
}
