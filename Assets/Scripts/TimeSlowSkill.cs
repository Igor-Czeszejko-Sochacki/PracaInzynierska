using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowSkill : MonoBehaviour
{
    public float slowDownFactor = 0.2f;
    public float normalTime = 1f;
    public bool timeCheck = false;

    void Update()
    {
        //Checking if the skill was used and timeScale if lower than normal. If yes then increasing it
        if (Time.timeScale <= normalTime && timeCheck == true)
        {
            Time.timeScale += 0.00001f;
        }
        if (Time.timeScale == normalTime)
            timeCheck = false;
    }

    //Slowing down time
    public void slowDownTime()
    {
        Time.timeScale = slowDownFactor;
    }

    //
    public void stopSlowMotion()
    {
        Time.timeScale = normalTime;
        timeCheck = true;
    }
}
