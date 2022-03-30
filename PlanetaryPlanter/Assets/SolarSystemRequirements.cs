using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystemRequirements : MonoBehaviour
{
    public List<Image> images;
    public TMPro.TextMeshProUGUI completePrompt;

    public RectTransform openPos;
    public RectTransform closedPos;
    public float timeSpentLerping;
    public float totalLerpTime;
    bool opening = false;
    public void UpdateInfo(List<Sprite> newSprites)
    {
        bool oneActive = false;
        for(int i = 0; i < images.Count; i++)
        {
            if(i < newSprites.Count)
            {
                oneActive = true;
                images[i].enabled = true;
                images[i].sprite = newSprites[i];
            }
            else
            {
                images[i].enabled = false;
            }
        }
        completePrompt.enabled = !oneActive; //shows complete text if none are active
    }

    void Lerp()
    {
        timeSpentLerping += Time.deltaTime;
        if(NewInventory.instance.inventoryActive)
        {
            if (!opening)
            {
                opening = true;
                timeSpentLerping = 0;
            }
            if (timeSpentLerping > totalLerpTime)
                transform.position = openPos.position;
            else
                transform.position = Vector3.Lerp(transform.position, openPos.position, timeSpentLerping/totalLerpTime);
        }
        else
        {
            if(opening)
            {
                opening = false;
                timeSpentLerping = 0;
            }
            if(timeSpentLerping > totalLerpTime)
                transform.position = closedPos.position;
            else
                transform.position = Vector3.Lerp(transform.position, closedPos.position, timeSpentLerping / totalLerpTime);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Lerp();
    }
}
