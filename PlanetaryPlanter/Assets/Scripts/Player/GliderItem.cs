using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// THIS SCRIPT IS NO LONGER BEING USED.
// When highlighted in the hotbar, clamps downward Y velocity to allow travel over great distances
public class GliderItem : MonoBehaviour
{
    public float durability = 1000f;
    public float durabilityDecrement = 1f;
    public bool DecrementDurability() //Returns false when durability is empty
    {
        durability -= durabilityDecrement;
        return durability > 0;
    }

    //Update Icon to show durability


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
