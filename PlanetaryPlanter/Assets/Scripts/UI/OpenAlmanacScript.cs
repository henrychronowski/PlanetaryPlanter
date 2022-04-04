using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAlmanacScript : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject almanac;

    // Audio Manager Script is set up here
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckPause(); ALL PAUSING IS DONE IN CloseAllMenus.cs NOW this just holds functions
    }

    void CheckPause()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(isPaused == false)
            {
                NewInventory.instance.SetSpacesActive(true);
                isPaused = true;
                Time.timeScale = 0;
                GameObject.FindObjectOfType<CharacterMovement>().canMove = false;
                pauseMenu.SetActive(true);
            }
            else
            {
                NewInventory.instance.SetSpacesActive(false);
                isPaused = false;
                Time.timeScale = 1;
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                if (!GameObject.FindObjectOfType<ObservatoryMaster>().inObservatoryView)
                {
                    GameObject.FindObjectOfType<CharacterMovement>().canMove = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isPaused == false)
            {
                NewInventory.instance.SetSpacesActive(true);
                isPaused = true;
                Time.timeScale = 0;
                GameObject.FindObjectOfType<CharacterMovement>().canMove = false;
                almanac.SetActive(true);
            }
            else
            {
                NewInventory.instance.SetSpacesActive(false);
                isPaused = false;
                Time.timeScale = 1;
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                if (!GameObject.FindObjectOfType<ObservatoryMaster>().inObservatoryView)
                {
                    GameObject.FindObjectOfType<CharacterMovement>().canMove = true;
                }
            }
        }
    }

    public void PauseGame()
    {
        NewInventory.instance.SetSpacesActive(true);
        isPaused = true;
        Time.timeScale = 0;
        GameObject.FindObjectOfType<CharacterMovement>().canMove = false;
        pauseMenu.SetActive(true);
        soundManager.PlaySound("OpenMenu");
    }

    public void OpenAlmanac()
    {
        if(isPaused && almanac.activeInHierarchy)
        {
            UnpauseGame();
            return;
        }
        NewInventory.instance.SetSpacesActive(true);
        isPaused = true;
        Time.timeScale = 0;
        GameObject.FindObjectOfType<CharacterMovement>().canMove = false;
        almanac.SetActive(true);
        soundManager.PlaySound("OpenMenu");
    }

    public void UnpauseGame()
    {
        NewInventory.instance.SetSpacesActive(false);
        isPaused = false;
        Time.timeScale = 1;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        if(!GameObject.FindObjectOfType<ObservatoryMaster>().inObservatoryView)
        {
            GameObject.FindObjectOfType<CharacterMovement>().canMove = true;
        }
    }
}
