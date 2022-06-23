using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPortal : MonoBehaviour
{
    [SerializeField] GameObject levelDesignPrefab;
    [SerializeField] float timeValue;
    [SerializeField] string levelName;
    [SerializeField] public GameObject reward;

    public void Interact()
    {
        EnvironmentManager.Instance.TransitionToBonus();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
