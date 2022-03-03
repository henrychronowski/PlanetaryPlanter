using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunRotationScript : MonoBehaviour
{
    public static SunRotationScript instance;
    public GameObject sun;
    [SerializeField]
    float CurrentAngle = -30.0f;
    [SerializeField]
    float RotationScale = 0.05f;
    public int CurrentDay = 1;
    public int CurrentHour = 0;

    float FullRevolutionAngle;
    float HourAngle;
    public Text DayCounter;
    public Text HourCounter;
    public int totalElapsedTime;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

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
        
    }

    private void FixedUpdate()
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
        sun.transform.rotation = Quaternion.Euler(30, CurrentAngle, 0);
    }

    void CheckDay()
    {
        if(CurrentAngle <= FullRevolutionAngle + 0.01 &&
            CurrentAngle >= FullRevolutionAngle - 0.01)
        {
            CurrentDay++;
            //DayCounter.text = "Day " + CurrentDay;
        }
    }

    void CheckHour()
    {
        if(CurrentAngle <= HourAngle + 0.01 &&
            CurrentAngle >= HourAngle - 0.01)
        {
            HourAngle += 15.0f;
            CurrentHour++;
            totalElapsedTime++;

            if (HourAngle >= 180.0f)
            {
                HourAngle = -HourAngle;
            }

            if(CurrentHour == 24)
            {
                CurrentHour = 0;
            }

            //Debug.Log(((CurrentDay - 1) * 24) + CurrentHour);
            HourCounter.text = "Time: " + CurrentHour + ":00";
        }
    }

    public int GetHoursPassedSinceTime(int dayToCompare, int hourToCompare)
    {
        if(dayToCompare == CurrentDay && hourToCompare == CurrentHour)
        return 0;


        int hoursPassedAtComparedTime = ((dayToCompare - 1) * 24) + hourToCompare;

        return totalElapsedTime - hoursPassedAtComparedTime;
    }

    public int GetTotalElapsedTime()
    {
        return totalElapsedTime;
    }
}
