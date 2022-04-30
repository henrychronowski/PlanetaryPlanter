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

            if (button.gameObject.TryGetComponent<InventorySpace>(out InventorySpace space))
            {
                button.onClick.AddListener(PlayButtonSound);
                continue;
            }
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


    float bigHeadSize = 3f;
    float timeSinceGrowthStart;
    float totalGrowthTime = 1f;
    bool bigHeadEnabled = false;

    void BigHead()
    {
        timeSinceGrowthStart += Time.deltaTime;
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(bigHeadSize, bigHeadSize, bigHeadSize), timeSinceGrowthStart / totalGrowthTime);
    }

    void BigHeadCheck()
    {
        if (Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.T) && !bigHeadEnabled)
        {
            if(Input.GetKeyDown(KeyCode.LeftControl))
                bigHeadEnabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (bigHeadEnabled && timeSinceGrowthStart < totalGrowthTime)
            BigHead();
        BigHeadCheck();
    }
}
