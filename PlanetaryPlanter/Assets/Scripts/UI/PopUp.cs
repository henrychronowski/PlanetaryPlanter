using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PopUpType
{
    Almanac,
    Tutorial,
    Alert
}
public class PopUp : MonoBehaviour
{
    public string text;
    public Sprite icon;

    public PopUp(string newText, Sprite newIcon)
    {
        text = newText;
        icon = newIcon;
    }

    public void FirePopUp()
    {
        PopUpController.instance.NewPopUp(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //icon = GetComponent<IconHolder>().icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
