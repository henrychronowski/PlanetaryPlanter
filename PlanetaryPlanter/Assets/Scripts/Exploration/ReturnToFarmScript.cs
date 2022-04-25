using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnToFarmScript : MonoBehaviour
{
    bool canReturn;
    public GameObject returnSpot;
    public GameObject player;
    public Text returnMessage;
    public string returnText = "Hold R to return to farm.";
    public float returnCountdown;

    float currentReturnCountdown;
    bool canCountdown = false;

    // Audio Manager Script is set up here
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;
        canReturn = false;
        currentReturnCountdown = returnCountdown;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            canCountdown = false;
            canReturn = false;
            returnMessage.text = "";
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            canCountdown = true;
            //canReturn = true;
            returnMessage.text = returnText;
        }
    }

    void CheckInput()
    {
        if (canCountdown && Input.GetKey(KeyCode.R))
        {
            currentReturnCountdown -= Time.deltaTime;
        }

        if (currentReturnCountdown <= 0.0f)
        {
            canReturn = true;
        }

        if (canReturn)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(returnSpot.transform.position.x, returnSpot.transform.position.y, returnSpot.transform.position.z);
            player.GetComponent<CharacterController>().enabled = true;
            soundManager.PlaySound("FarmPort");

            currentReturnCountdown = returnCountdown;
        }
    }
}
