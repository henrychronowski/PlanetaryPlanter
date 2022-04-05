using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkAIThief : MonoBehaviour
{
    public GameObject thief;
    public GameObject inventory;

    GameObject recoveredObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RecoverStolenItem();
    }

    void RecoverStolenItem()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Vector3.Distance(gameObject.transform.position, thief.transform.position)
                < 2.0f)
            {
                recoveredObject = thief.GetComponent<MoveAIThief>().GetStolenObject();
                thief.GetComponent<MoveAIThief>().DropItem();
                if(recoveredObject != null)
                    inventory.GetComponent<NewInventory>().AddItem(recoveredObject);
            }
        }
    }

    public void RecoverStolenItemWithBonk()
    {
        recoveredObject = thief.GetComponent<MoveAIThief>().GetStolenObject();
        thief.GetComponent<MoveAIThief>().DropItem();
        if (recoveredObject != null)
            inventory.GetComponent<NewInventory>().AddItem(recoveredObject);
    }
}
