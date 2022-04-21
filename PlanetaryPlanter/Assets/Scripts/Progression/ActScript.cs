using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class ActScript : MonoBehaviour
{
    public bool unlocked;
    public string actName;
    public GameObject act;

    public TextMeshProUGUI actText;

    public class ActManagerScript : UnityEventQueueSystem { }
    public ActManagerScript actEvent = new ActManagerScript();
    public ActManagerScript onActEvent { get { return actEvent; } set { actEvent = value; } }

    public void TriggerActEnd()
    {
        if (!unlocked)
        {
            unlocked = true;
            act.SetActive(true);
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