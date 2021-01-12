using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public ShootingGun player;
    public Text ammunitionText;
    public Text heathText;
    void Update()
    {
        ammunitionText.text = "Ammo: " + player.Ammunition;
        heathText.text = "Health: " + player.Health;
    }
}
