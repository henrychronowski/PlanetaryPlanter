using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureManager : MonoBehaviour
{
    public GameObject temperatureLocation; //location the temperature radiates from
    public float maxTemperature; //max temperature for zone
    public float minTemperature; //min temperature for zone
    public float biomeSize; //size of the biome for this temperature

    float currentTemperature;
    Collider[] objectsInBiome;

    // Start is called before the first frame update
    void Start()
    {
        CalculateTemperatureChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckObjectsInRange()
    {
        objectsInBiome = Physics.OverlapSphere(gameObject.transform.position, biomeSize);
    }

    void CalculateTemperatureChange()
    {
        currentTemperature = Random.Range(minTemperature, maxTemperature);
    }
}