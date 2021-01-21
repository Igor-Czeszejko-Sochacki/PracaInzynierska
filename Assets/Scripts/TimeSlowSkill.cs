using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowSkill : MonoBehaviour
{
    public float slowDownFactor = 0.2f;
    public float normalTime = 1f;
    public bool timeCheck = false;
    public MenuController gameisPaused;
    void Update()
    {
        //Checking if the skill was used and timeScale if lower than normal. If yes then increasing it
        if (Time.timeScale <= normalTime && timeCheck == true && gameisPaused.isGamePaused == false)
        {
            Time.timeScale += 0.00001f;
        }
        if (Time.timeScale == normalTime)
            timeCheck = false;
    }

    //Slowing down time
    public void slowDownTime()
    {
        if (gameisPaused.isGamePaused == false)
            Time.timeScale = slowDownFactor;
    }

    //
    public void stopSlowMotion()
    {
        if (gameisPaused.isGamePaused == false)
        {
            Time.timeScale = normalTime;
            timeCheck = true;
        }   
    }
}
