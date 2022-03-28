using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystemRequirements : MonoBehaviour
{
    public List<Image> images;
    public TMPro.TextMeshProUGUI completePrompt;
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
        
    }
}
