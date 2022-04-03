using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystemInfo : MonoBehaviour
{
    public Observatory observatory;
    public SolarSystemRequirements requirements;
    public Button button;
    public void UpdateRequirementsPanel()
    {
        List<Sprite> sprites = new List<Sprite>();
        List<Observatory> observatories = new List<Observatory>();
        if(observatory == null)
        {
            char[] chars = gameObject.name.ToCharArray();
            int index = int.Parse(chars[chars.Length - 1].ToString());
            observatory = transform.parent.Find("SSView" + index.ToString()).gameObject.GetComponent<Observatory>();
        }
        if(requirements == null)
        {
            requirements = GameObject.Find("SolarSystemRequirements").GetComponent<SolarSystemRequirements>();
        }
        observatories.AddRange(observatory.gameObject.transform.parent.GetComponentsInChildren<Observatory>(true));
        foreach(Observatory obs in observatories)
        {
            foreach(ObservatoryPlanetSpot spot in obs.GetPlanetSpots())
            {
                if(!spot.filled)
                    sprites.Add(spot.GetComponent<PlanetInformationScript>().ReturnSpriteToDisplay(spot.species, spot.type));
            }
        }
        requirements.UpdateInfo(sprites);
    }

    void SetConstellationCamActive()
    {
        observatory.transform.Find("TestConstCam").gameObject.SetActive(true);
        TutorialManagerScript.instance.Unlock("Solar System");
    }

    private void OnMouseEnter()
    {
        UpdateRequirementsPanel();
    }
    // Start is called before the first frame update
    void Start()
    {
        char[] chars = gameObject.name.ToCharArray();
        button = GetComponent<Button>();
        int index = int.Parse(chars[chars.Length - 1].ToString());
        observatory = transform.parent.Find("SSView" + index.ToString()).gameObject.GetComponent<Observatory>();
        requirements = GameObject.Find("SolarSystemRequirements").GetComponent<SolarSystemRequirements>();
        button.onClick.AddListener(SetConstellationCamActive);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
