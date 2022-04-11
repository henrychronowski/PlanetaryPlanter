using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastForwardScript : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public GameObject gifCanvas;
    public GameObject comicCanvas;


    public OpenAlmanacScript pauseControl;
    public float fastForwardSpeed = 4;
    public GameObject fastForwardImage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if(tutorialCanvas.activeInHierarchy || pauseControl.isPaused || gifCanvas.activeInHierarchy || comicCanvas.activeInHierarchy)
        {
            fastForwardImage.SetActive(false);
            Time.timeScale = 0;
            return;
        }
        else if (Input.GetKey(KeyCode.F))
        {
            Time.timeScale = fastForwardSpeed;
            fastForwardImage.SetActive(true);
            return;
        }
        else
        {
            fastForwardImage.SetActive(false);
            Time.timeScale = 1;
            return;
        }
    }
}
