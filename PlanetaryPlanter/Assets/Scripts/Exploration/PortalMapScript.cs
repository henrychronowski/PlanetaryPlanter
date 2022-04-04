using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMapScript : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TeleportPlayer(GameObject location)
    {
        player.transform.position = new Vector3(location.transform.position.x, location.transform.position.y, location.transform.position.z);
        NewInventory.instance.SetSpacesActive(false);
    }
}
