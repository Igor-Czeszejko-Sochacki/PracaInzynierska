using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorController : MonoBehaviour
{
    public Light lightObject;
    // Start is called before the first frame update
    void Start()
    {
        lightObject.color = new Color(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
       // lightObject.color -= 
    }
}
