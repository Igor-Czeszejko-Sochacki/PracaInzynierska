using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManger : MonoBehaviour
{

    private static ObjectPoolingManger instance;
    public static ObjectPoolingManger Instance { get { return instance; } }

    public GameObject bulletPrefab;

    public int bulletAmount = 30;
    private List<GameObject> bulletsList;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        bulletsList = new List<GameObject>(bulletAmount);
        for (int i=0; i<bulletAmount; i++)
        {
            GameObject prefabInstance = Instantiate(bulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);
            bulletsList.Add(prefabInstance);
        }

    }

    public GameObject SpawnBullet(bool shotByPlayer, int damage)
    {
        //Check bullet list
        foreach (GameObject bullet in bulletsList)
        {
            //If bullet is not active - activate it
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.GetComponent<BulletLogic>().ShotByPlayer = shotByPlayer;
                //Check who shot ht bullet and set damage according to the gun damage
                if (shotByPlayer == true)
                    bullet.GetComponent<BulletLogic>().playerBulletDamage = damage;
                else
                    bullet.GetComponent<BulletLogic>().enemyBulletDamage = damage;
                return bullet;
            }
            
        }

        //If there are no bullets to activate then create new ones
        GameObject prefabInstance = Instantiate(bulletPrefab);
        prefabInstance.transform.SetParent(transform);
        bulletPrefab.GetComponent<BulletLogic>().ShotByPlayer = shotByPlayer;
        //Check who shot ht bullet and set damage according to the gun damage
        if (shotByPlayer == true)
            bulletPrefab.GetComponent<BulletLogic>().playerBulletDamage = damage;
        else
            bulletPrefab.GetComponent<BulletLogic>().enemyBulletDamage = damage;
        bulletsList.Add(prefabInstance);
        return prefabInstance;
    }
}
