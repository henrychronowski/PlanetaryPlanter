using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFoundScript : MonoBehaviour
{
    public enum Biome
    {
        Temperate,
        Hot,
        Cold,
        Farm
    }
    
    public bool portalFound;
    public bool canTeleport;
    CharacterMovement player;
    public Biome location;
    public GameObject openModel, closedModel;

    [SerializeField]
    float distanceFromPortalPlant;

    [SerializeField] PortalMapScript portalMap;

    public AudioSource farmSource;
    public AudioSource hotSource;
    public AudioSource coldSource;
    public AudioSource temperateSource;



    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindObjectOfType<CharacterMovement>();
        portalMap = GameObject.FindObjectOfType<PortalMapScript>();
        farmSource = GameObject.Find("FarmMusic").GetComponent<AudioSource>();
        hotSource = GameObject.Find("HotBiomeMusic").GetComponent<AudioSource>();
        coldSource = GameObject.Find("ColdBiomeMusic").GetComponent<AudioSource>();
        temperateSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
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
            openPlant(true);
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
        

        ChangeMusic();

        portalMap.CloseMap();
    }

    void ChangeMusic()
    {
        coldSource.Stop();
        hotSource.Stop();
        farmSource.Stop();
        temperateSource.Stop();

        switch (location)
        {
            case Biome.Farm:
                {
                    farmSource.Play();
                    break;
                }
            case Biome.Cold:
                {
                    coldSource.Play();
                    break;
                }
            case Biome.Hot:
                {
                    hotSource.Play();
                    break;
                }
            case Biome.Temperate:
                {
                    temperateSource.Play();
                    break;
                }
        }
    }

    public void Interact()
    {
        portalMap.OpenMap(this);
    }

    public void openPlant(bool open)
    {
        if(open)
        {
            openModel.SetActive(true);
            closedModel.SetActive(false);
        }
        else
        {
            openModel.SetActive(false);
            closedModel.SetActive(true);
        }
    }
}
