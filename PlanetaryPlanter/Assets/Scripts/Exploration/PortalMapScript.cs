using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Linq;

public class PortalMapScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] public List<PortalFoundScript> portals;
    [SerializeField] List<GameObject> activePortalButtons;
    [SerializeField] GameObject teleportButton;
    [SerializeField] CinemachineVirtualCamera portalMapCam;
    [SerializeField] GameObject playerIcon;
    [SerializeField] GameObject canvas;
    public bool portalInColdBiome = false;
    public bool portalInHotBiome = false;
    [SerializeField] Transform tempColdCamTransform;
    [SerializeField] Transform tempHotCamTransform;
    [SerializeField] Transform allBiomesCamTransform;
    [SerializeField] Transform temperateCamTransform;
    [SerializeField] PortalFoundScript openedPortal;
    [SerializeField] SoundManager soundManager;

    public bool mapActive;
    public void OpenMap(PortalFoundScript p) 
    {
        openedPortal = p;
        if(!mapActive)
        {
            //unlock mouse
            NewInventory.instance.SetSpacesActive(true);

            mapActive = true;
            portalMapCam.gameObject.SetActive(true);

            foreach (PortalFoundScript portal in portals)
            {
                if(portal == openedPortal)
                {
                    playerIcon.SetActive(true);
                    playerIcon.transform.position = Camera.main.WorldToScreenPoint(portal.gameObject.transform.position);

                    if (portal.location == PortalFoundScript.Biome.Hot)
                        portalInHotBiome = true;
                    if (portal.location == PortalFoundScript.Biome.Cold)
                        portalInColdBiome = true;
                }
                else if(portal.portalFound)
                {
                    GameObject newButton = Instantiate(teleportButton, canvas.transform, false);
                    newButton.transform.position = Camera.main.WorldToScreenPoint(portal.gameObject.transform.position);
                    activePortalButtons.Add(newButton);
                    newButton.GetComponent<Button>().onClick.AddListener(portal.Teleport);
                    newButton.GetComponent<Button>().onClick.AddListener(PlayTeleportSound);

                    if (portal.location == PortalFoundScript.Biome.Hot)
                        portalInHotBiome=true;
                    if(portal.location == PortalFoundScript.Biome.Cold)
                        portalInColdBiome=true;
                }
            }
            //Set the camera pos based on what portals have been unlocked
            if (portalInColdBiome && !portalInHotBiome)
            {
                portalMapCam.transform.position = tempColdCamTransform.position;
                portalMapCam.transform.rotation = tempColdCamTransform.rotation;
            }
            else if (!portalInColdBiome && portalInHotBiome)
            {
                portalMapCam.transform.position = tempHotCamTransform.position;
                portalMapCam.transform.rotation = tempHotCamTransform.rotation;
            }
            else if(portalInHotBiome && portalInHotBiome)
            {
                portalMapCam.transform.position = allBiomesCamTransform.position;
                portalMapCam.transform.rotation = allBiomesCamTransform.rotation;
            }
            else
            {
                portalMapCam.transform.position = temperateCamTransform.position;
                portalMapCam.transform.rotation = temperateCamTransform.rotation;
            }

        }

    }

    void PlayTeleportSound()
    {
        soundManager.PlaySound("FarmPort");
    }

    public void CloseMap()
    {
        NewInventory.instance.SetSpacesActive(false);
        foreach(GameObject portal in activePortalButtons)
        {
            Destroy(portal);
        }
        activePortalButtons.Clear();
        mapActive = false;
        portalMapCam.gameObject.SetActive(false);
        openedPortal = null;
    }

    void UpdateUIPosition()
    {
        if (!mapActive)
            return;

        //Force inventory on so that the player can't move in this state
        NewInventory.instance.SetSpacesActive(true);


        int i = 0;
        foreach (PortalFoundScript portal in portals)
        {
            if (portal == openedPortal)
            {
                playerIcon.SetActive(true);
                playerIcon.transform.position = Camera.main.WorldToScreenPoint(portal.gameObject.transform.position);
            }
            else if (portal.portalFound)
            {
                activePortalButtons[i].transform.position = Camera.main.WorldToScreenPoint(portal.gameObject.transform.position);
                i++;
            }
        }
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mapActive)
                CloseMap();
        }
    }

    public bool[] GetPortalsFound()
    {
        List<bool> result = new List<bool>();

        for(int i = 0; i < portals.Count; i++)
        {
            result.Add(portals[i].portalFound);
        }
        

        return result.ToArray();
    }

    private void Start()
    {
        portals = new List<PortalFoundScript>();
        portals.AddRange(GameObject.FindObjectsOfType<PortalFoundScript>().OrderBy(gameObject => gameObject.name).ToArray());

        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        UpdateUIPosition();
    }
}

