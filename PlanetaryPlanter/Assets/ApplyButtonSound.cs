using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ApplyButtonSound : MonoBehaviour
{
    List<Button> allButtons;
    SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        allButtons = new List<Button>();
        allButtons.AddRange(FindObjectsOfType<Button>(true));
        soundManager = FindObjectOfType<SoundManager>();
        foreach(Button button in allButtons)
        {
            if (button.gameObject.tag == "Inventory")
                continue;

            button.onClick.AddListener(PlayButtonSound);
            var pointerHover = new EventTrigger.Entry();
            pointerHover.eventID = EventTriggerType.PointerEnter;
            pointerHover.callback.AddListener((e) => this.PlayButtonHoverSound());
            button.gameObject.AddComponent<EventTrigger>().triggers.Add(pointerHover);
            
        }
    }

    public void PlayButtonSound()
    {
        soundManager.PlaySound("ButtonPress");
    }

    public void PlayButtonHoverSound()
    {
        soundManager.PlaySound("ButtonOver");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
