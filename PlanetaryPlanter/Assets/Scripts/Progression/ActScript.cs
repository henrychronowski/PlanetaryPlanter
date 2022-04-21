using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActScript : MonoBehaviour
{
    public bool unlocked;
    public string actName;

    public TextMeshProUGUI actText;
    public void TriggerActEnd()
    {
        if(!unlocked)
        {
            unlocked = true;
            actText.text = "End of " + actName;
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
