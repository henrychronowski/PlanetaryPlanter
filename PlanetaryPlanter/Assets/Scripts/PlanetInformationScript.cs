using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInformationScript : MonoBehaviour
{
    public TextMeshProUGUI infoText;

    public Sprite[] asteroid;

    public Sprite[] planet;

    public Sprite[] star;

    public Image preview;

    public bool isHovering;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText();
    }

    void OnMouseOver()
    {
        isHovering = true;
    }

    void OnMouseExit()
    {
        isHovering = false;
    }

    void DisplayText()
    {
        if(isHovering == true)
        {
            if(GetComponent<ObservatoryPlanetSpot>().filled)
            {
                
                infoText.gameObject.SetActive(true);
                infoText.text = "Object in place.";
            }
            else
            {
                infoText.gameObject.SetActive(true);
                infoText.text = "Object Species: " + GetComponent<ObservatoryPlanetSpot>().species +
                    "\nObject Type: " + GetComponent<ObservatoryPlanetSpot>().type;
                ChooseImageToDisplay(GetComponent<ObservatoryPlanetSpot>().species, GetComponent<ObservatoryPlanetSpot>().type);
            }
        }
        else
        {
            //infoText.gameObject.SetActive(false);
        }
    }

    void ChooseImageToDisplay(PlanetSpecies species, PlanetType type)
    {
        switch(species)
        {
            case PlanetSpecies.Asteroid:
                {
                    preview.sprite = asteroid[(int)type];
                    break;
                }
            case PlanetSpecies.Planet:
                {
                    preview.sprite = planet[(int)type];
                    break;
                }
            case PlanetSpecies.Star:
                {
                    preview.sprite = star[(int)type];
                    break;
                }
        }
    }
}