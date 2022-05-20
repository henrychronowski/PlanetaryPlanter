using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Random = UnityEngine.Random;

public class GoldSquimbusRoll : MonoBehaviour
{
    public int maxRoll = 100;
    public int squimbusRoll = 0;

    public Renderer squimbus;
    public Material goldMat;
   
    //Generate random number to determine if golden
    void Start()
    {
       squimbusRoll = Random.Range(0, maxRoll);
       if (squimbusRoll == maxRoll-1)
          CreateGolden();
    }
    
    //Set material to golden squimbus if golden
    void CreateGolden()
    {
       squimbus.material = goldMat;
    }
}
