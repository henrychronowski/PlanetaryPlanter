using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSeedScript : MonoBehaviour
{
    public bool canCollect = false;
    public GameObject seed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CollectSeed();
    }

    void OnTriggerEnter()
    {
        canCollect = true;
    }

    void OnTriggerExit()
    {
        canCollect = false;
    }

    void CollectSeed()
    {
        if(canCollect == true && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(seed);
        }
    }
}
