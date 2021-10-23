using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CollectSeedScript : MonoBehaviour
{
    public bool canCollect = false;
    public GameObject seed;
    public GameObject fertilizer;

    public GameObject plant;
    //public bool collected = false;

    // Start is called before the first frame update
    void Start()
    {
        NewInventory.instance.AddItem(fertilizer);
    }

    // Update is called once per frame
    void Update()
    {
        CollectSeed();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            canCollect = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            canCollect = false;
        }
    }

    void CollectSeed()
    {
        if(canCollect && Input.GetKeyDown(KeyCode.E))
        {
            UnityEngine.Debug.Log("1");
            //SeedInventoryScript inventory = gameObject.GetComponent<SeedInventoryScript>();
            GameObject temp = Instantiate(seed, transform.parent, false);
            //SeedInventoryScript.instance.AddSeed(temp);
            GameObject.FindObjectOfType<NewInventory>().AddItem(temp);
            //Destroy(seed);
        }
    }
}
