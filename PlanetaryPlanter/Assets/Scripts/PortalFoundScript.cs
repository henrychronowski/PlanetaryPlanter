using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFoundScript : MonoBehaviour
{
    public bool portalFound;
    public bool canTeleport;
    Transform player;

    [SerializeField]
    float distanceFromPortalPlant;

    // Start is called before the first frame update
    void Start()
    {
        portalFound = false;
        canTeleport = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceFromPlayer();
    }

    void CheckDistanceFromPlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distanceFromPortalPlant > distance)
        {
            portalFound = true;
            TutorialManagerScript.instance.Unlock("Portal Plant");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canTeleport = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canTeleport = false;
        }
    }
}
