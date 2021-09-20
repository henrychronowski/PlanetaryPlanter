using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitchScript : MonoBehaviour
{
    public bool canSwitch = false;
    public bool isSwitched = false;

    public Renderer pos1;
    public Renderer pos2;

    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UseSwitch();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            canSwitch = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            canSwitch = false;
        }
    }

    void UseSwitch()
    {
        if(canSwitch == true && Input.GetKeyDown(KeyCode.Q))
        {
            if(isSwitched == false)
            {
                isSwitched = true;

                pos1.enabled = false;
                pos2.enabled = true;

                Destroy(obj);
            }

        }
    }
}
