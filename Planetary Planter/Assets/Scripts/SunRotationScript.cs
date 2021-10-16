using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunRotationScript : MonoBehaviour
{
    public GameObject sun;
    float CurrentAngle = -30.0f;
    float RotationScale = 0.1f;
    public int CurrentDay = 1;
    public int CurrentHour = 0;

    float FullRevolutionAngle;
    float HourAngle;
    public Text DayCounter;
    public Text HourCounter;

    // Start is called before the first frame update
    void Start()
    {
        FullRevolutionAngle = CurrentAngle;
        HourAngle = CurrentAngle + 15.0f;

        DayCounter.text = "Day " + CurrentDay;
        HourCounter.text = "Time: " + CurrentHour + ":00";
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        CheckDay();
        CheckHour();
    }

    void Rotation()
    {
        CurrentAngle += RotationScale;
        if(CurrentAngle >= 180.0f)
        {
            CurrentAngle = -CurrentAngle;
        }
        sun.transform.rotation = Quaternion.Euler(0, CurrentAngle, 0);
    }

    void CheckDay()
    {
        if(CurrentAngle <= FullRevolutionAngle + 0.01 &&
            CurrentAngle >= FullRevolutionAngle - 0.01)
        {
            CurrentDay++;
            DayCounter.text = "Day " + CurrentDay;
        }
    }

    void CheckHour()
    {
        if(CurrentAngle <= HourAngle + 0.01 &&
            CurrentAngle >= HourAngle - 0.01)
        {
            HourAngle += 15.0f;
            CurrentHour++;

            if(HourAngle >= 180.0f)
            {
                HourAngle = -HourAngle;
            }

            if(CurrentHour == 24)
            {
                CurrentHour = 0;
            }

            HourCounter.text = "Time: " + CurrentHour + ":00";
        }
    }
}
