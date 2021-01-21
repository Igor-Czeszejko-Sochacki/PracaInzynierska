using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Image healthShowcase;
    public BossEnemy boss;
    public float bossMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        bossMaxHealth = boss.health;   
    }

    // Update is called once per frame
    void Update()
    {
            healthShowcase.fillAmount = boss.health / bossMaxHealth;
    }
}
