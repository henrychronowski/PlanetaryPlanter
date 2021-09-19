using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyTest : MonoBehaviour
{
    
    public List<BuoyancyPoint> buoyancyTests;
    private Rigidbody rgd;
    public float clampValue;

    void UpdateBuoyancy()
    {
        //Vector3 bForce = Vector3.up;
        float bFactorTotal = 0;
        //foreach(BuoyancyPoint point in buoyancyTests)
        //{
        //    if(point.submerged)
        //    {
        //        bFactorTotal += point.buoyancyFactor;
        //    }
        //}

        for(int i = buoyancyTests.Count-1; i >= 0; i--)
        {
            if(buoyancyTests[i].submerged)
            {
                bFactorTotal += buoyancyTests[i].buoyancyFactor;
                for (int j = i-1; j >= 0; j--)
                {
                    bFactorTotal += buoyancyTests[j].buoyancyFactor; //add the factor of all points below this one even if they are not colliding with terrain
                    buoyancyTests[j].submerged = true; //submerge the lower points
                }
            }
        }
        
        if(bFactorTotal == 0)
        {
            //Vector3.ClampMagnitude(rgd.velocity, clampValue);
            rgd.AddForce(Vector3.down * Physics.gravity.y * 10 * -1f);
            //rgd.
            //rgd.drag = 0;
        }
        else
        {
            rgd.AddForce(Vector3.up * bFactorTotal);
            //rgd.drag = 2;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        UpdateBuoyancy();
        rgd.velocity = Vector3.ClampMagnitude(rgd.velocity, clampValue);

    }
}
