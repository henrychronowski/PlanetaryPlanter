using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Achievement : MonoBehaviour
{
    public bool unlocked;
    public string title;
    public string description;
    public Sprite popUpIcon;

    [System.Serializable]
    public class AchievementEvent : UnityEvent { };

    public AchievementEvent achievementEvent = new AchievementEvent();

    public AchievementEvent onAchievementEvent { get { return achievementEvent; } set { achievementEvent = value; } }

    public void InteractableEventTriggered()
    {
        if(!unlocked)
        {
            unlocked = true;
            PopUpController.instance.NewPopUp(description, popUpIcon);
            //((MonoBehaviour)achievementEvent.GetPersistentTarget(0)).SendMessage(achievementEvent.GetPersistentMethodName(0));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
