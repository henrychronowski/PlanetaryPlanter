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

    float FullRevolutionAngle;
    public Text DayCounter;

    // Start is called before the first frame update
    void Start()
    {
        FullRevolutionAngle = CurrentAngle;
        DayCounter.text = "Day " + CurrentDay;
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        CheckDay();
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
}
