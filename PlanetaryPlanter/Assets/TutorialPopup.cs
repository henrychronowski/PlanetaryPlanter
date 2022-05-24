using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    public CanvasGroup group;
    public float totalFadeInTime;
    public float totalFadeOutTime;
    public float timeSinceFadeInStart;
    public float timeSinceFadeOutStart;

    public bool fadeIn;
    public bool fadeOut;

    // This is a scuffed way of having the fade in not be framerate dependant
    // by getting the deltaTime before pausing the game, should work well enough
    public float setDeltaTime = 0f;

    public void FadeInStart()
    {
        group.alpha = 0f;
        fadeIn = true;
        fadeOut = false;
        timeSinceFadeInStart = 0;
    }

    public void FadeOutStart()
    {
        group.alpha = 1f;
        fadeOut = true;
        fadeIn = false;
        timeSinceFadeOutStart = 0;
    }

    void FadeInUpdate()
    {
        if (fadeIn && group.alpha <= 1)
        {
            Debug.Log(Time.time);
            timeSinceFadeInStart += setDeltaTime;
            group.alpha = timeSinceFadeInStart / totalFadeInTime;
        }
        if(timeSinceFadeInStart > totalFadeInTime)
        {
            group.alpha = 1;
            fadeIn = false;
        }
    }


    void FadeOutUpdate()
    {
        if (fadeOut && group.alpha >= 0)
        {
            timeSinceFadeOutStart += setDeltaTime;
            group.alpha = 1 - (timeSinceFadeOutStart / totalFadeOutTime);
        }
        if (timeSinceFadeOutStart > totalFadeOutTime)
        {
            group.alpha = 0;
            fadeOut = false;
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeIn)
        {
            FadeInUpdate();
        }
        else if(fadeOut)
        {
            FadeOutUpdate();
        }
    }
}
