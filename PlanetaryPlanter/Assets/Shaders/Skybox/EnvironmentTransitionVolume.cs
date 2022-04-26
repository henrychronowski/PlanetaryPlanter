using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTransitionVolume : MonoBehaviour
{
    public EnvironmentManager.EnvironmentType type = new EnvironmentManager.EnvironmentType();
    private bool toggle;

    void Start()
    {
        toggle = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(toggle && other.CompareTag("Player"))
        {
            switch(type)
            {
                case EnvironmentManager.EnvironmentType.Cold:
                EnvironmentManager.Instance.TransitionToCold();
                break;
                case EnvironmentManager.EnvironmentType.Hot:
                EnvironmentManager.Instance.TransitionToHot();
                break;
                case EnvironmentManager.EnvironmentType.Temperate:
                EnvironmentManager.Instance.TransitionToTemperate();
                break;
            }

            toggle = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        toggle = true;
    }
}
