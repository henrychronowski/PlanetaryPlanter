using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTooltip : MonoBehaviour
{
    RectTransform rect;
    public float xOffset;
    public float yOffset;
    public TextMeshProUGUI text;
    public Image panel;
    public void SetNewText(string newText)
    {
        text.text = newText;
    }

    void CheckForItemInCursor()
    {
        if(NewInventory.instance.itemInCursor)
        {
            text.enabled = false;
            SetPanelActive(false);
        }
        else
        {
                text.enabled = true;
                
                SetPanelActive(text.text != "");
        }
    }

    public void SetPanelActive(bool enabled)
    {
        panel.enabled = enabled;
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tooltipPanelPos = new Vector3(Input.mousePosition.x + (xOffset * Screen.width*0.01f), Input.mousePosition.y + (yOffset * Screen.height*0.01f));
        rect.position = tooltipPanelPos;
        CheckForItemInCursor();
    }
}
