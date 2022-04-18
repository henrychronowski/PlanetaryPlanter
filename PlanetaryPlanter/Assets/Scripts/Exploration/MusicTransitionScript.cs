using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransitionScript : MonoBehaviour
{
    public AudioSource tempBiome;
    public AudioSource newBiome;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            tempBiome.Stop();
            newBiome.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            newBiome.Stop();
            tempBiome.Play();
        }
    }
}
