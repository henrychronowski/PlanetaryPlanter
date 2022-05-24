using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Random = UnityEngine.Random;

public class GoldSquimbusRoll : MonoBehaviour
{
    public int maxRoll = 100;
    private int squimbusRoll = 0;
    public bool gold = false;

    public Renderer squimbus;
    public Material goldMat;
    public GameObject goldParticles;
   
    //Generate random number to determine if golden
    void Start()
    {
       squimbusRoll = Random.Range(0, maxRoll);
       if (squimbusRoll == maxRoll-1 || gold == true)
          CreateGolden();
    }
    
    //Set material to golden squimbus if golden
    void CreateGolden()
    {
       gold = true;
       squimbus.material = goldMat;
       Instantiate(goldParticles, gameObject.transform);
    }
}
