using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Observatory : MonoBehaviour
{
    public static Observatory instance;

    public Transform bottomLeftCorner;
    public int width;
    public int height;
    public float xDistanceBetweenPlanets;
    public float yDistanceBetweenPlanets;

    public GameObject emptySlot;
    public ObservatoryPlanetSpot[,] planetSpotsArray;
    public bool createPlanetsOnStart;

    public GameObject playerCam;
    public bool inObservatoryView;

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (playerCam.activeInHierarchy)
            {
                playerCam.SetActive(false);
                inObservatoryView = true;
            }
            else
            {
                playerCam.SetActive(true);
                inObservatoryView = false;
            }
        }
    }

    void CreateEmptySlots()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = height-1; j >= 0; j--)
            {
                GameObject temp = Instantiate(emptySlot, 
                    new Vector3(bottomLeftCorner.position.x + (xDistanceBetweenPlanets * i), bottomLeftCorner.position.y + (yDistanceBetweenPlanets * j), bottomLeftCorner.position.z),
                    Quaternion.identity,
                    bottomLeftCorner);
                temp.name = "PlanetSpot " + i.ToString() + j.ToString(); 
                planetSpotsArray[i,j] = temp.GetComponent<ObservatoryPlanetSpot>();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        planetSpotsArray = new ObservatoryPlanetSpot[width, height];
        if(createPlanetsOnStart)
        {
            CreateEmptySlots();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }
}
