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

    // Start is called before the first frame update
    void Start()
    {
        canReturn = false;
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
            canReturn = false;
            returnMessage.text = "";
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            canReturn = true;
            returnMessage.text = "Press F to return to farm.";
        }
    }

    void CheckInput()
    {
        if(canReturn && Input.GetKeyDown(KeyCode.F))
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(returnSpot.transform.position.x, returnSpot.transform.position.y, returnSpot.transform.position.z);
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
