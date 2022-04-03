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
        if (Input.GetKey(KeyCode.B))
        {
            if (Vector3.Distance(gameObject.transform.position, thief.transform.position)
                < 2.0f)
            {
                recoveredObject = thief.GetComponent<MoveAIThief>().GetStolenObject();
                thief.GetComponent<MoveAIThief>().DropItem();

                inventory.GetComponent<NewInventory>().AddItem(recoveredObject);
            }
        }
    }
}
