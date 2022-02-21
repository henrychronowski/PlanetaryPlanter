using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAllMenus : MonoBehaviour
{
    public OpenAlmanacScript pauseControl;
    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    void Close()
    {
        GameObject[] menus = GameObject.FindGameObjectsWithTag("Menu");
        bool menusClosed = false;
        if (GameObject.Find("Observatory 1").GetComponent<Observatory>().inObservatoryView)
        {
//            GameObject.Find("Observatory 1").GetComponent<Observatory>().EnterObservatory();
        }
        foreach(GameObject m in menus)
        {
            menusClosed = true;
            m.SetActive(false);
            pauseControl.UnpauseGame();
        }
        if(menusClosed || Time.timeScale == 0)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
            NewInventory.instance.SetSpacesActive(false);
        }
        else
        {
            pauseControl.PauseGame();
        }
        
    }

    public bool AreMenusOpen()
    {
        if (GameObject.Find("Observatory 1").GetComponent<Observatory>().inObservatoryView)
        {
            return true;
        }

        GameObject[] menus = GameObject.FindGameObjectsWithTag("Menu");
        
        foreach (GameObject m in menus)
        {
            if (m.activeInHierarchy)
                return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }
}
