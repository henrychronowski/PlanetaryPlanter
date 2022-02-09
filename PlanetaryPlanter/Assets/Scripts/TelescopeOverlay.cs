using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class TelescopeOverlay : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public CinemachineBrain brain;
    public GameObject overlay;
    public Observatory observatory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (observatory.inObservatoryView)
        {
            overlay.SetActive(true);
        }
        else
        {
            overlay.SetActive(false);
        }
    }
}
