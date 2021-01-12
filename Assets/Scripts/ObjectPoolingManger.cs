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

   public GameObject SpawnBullet(bool shotByPlayer)
    {
        foreach (GameObject bullet in bulletsList)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.GetComponent<BulletLogic>().ShotByPlayer = shotByPlayer;
                return bullet;
            }
        }
        GameObject prefabInstance = Instantiate(bulletPrefab);
        prefabInstance.transform.SetParent(transform);
        bulletPrefab.GetComponent<BulletLogic>().ShotByPlayer = shotByPlayer;
        bulletsList.Add(prefabInstance);
        return prefabInstance;
    }
}
