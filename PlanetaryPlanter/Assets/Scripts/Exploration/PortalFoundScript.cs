using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFoundScript : MonoBehaviour
{
    public enum Biome
    {
        Temperate,
        Hot,
        Cold
    }
    
    public bool portalFound;
    public bool canTeleport;
    CharacterMovement player;
    public Biome location;

    [SerializeField]
    float distanceFromPortalPlant;

    [SerializeField] PortalMapScript portalMap;

    // Start is called before the first frame update
    void Start()
    {
        portalFound = false;
        canTeleport = false;

        player = GameObject.FindObjectOfType<CharacterMovement>();
        portalMap = GameObject.FindObjectOfType<PortalMapScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceFromPlayer();
    }

    void CheckDistanceFromPlayer()
    {
        float distance = Vector3.Distance(player.gameObject.transform.position, transform.position);
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

    public void Teleport()
    {
        player.Teleport(transform.position);
        portalMap.CloseMap();
    }

    

    public void Interact()
    {
        portalMap.OpenMap(this);
    }

}
