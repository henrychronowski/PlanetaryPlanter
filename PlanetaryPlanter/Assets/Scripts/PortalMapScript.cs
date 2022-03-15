using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMapScript : MonoBehaviour
{
    public PortalFoundScript[] portalObj;
    int portalListSize;

    // Start is called before the first frame update
    void Awake()
    {
        portalObj = FindObjectsOfType<PortalFoundScript>();
        portalListSize = portalObj.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
