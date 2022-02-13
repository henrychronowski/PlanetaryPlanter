using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerBiome : MonoBehaviour
{
    public Biomes currentBiome;
    public Text biomeText;

    // Start is called before the first frame update
    void Start()
    {
        DisplayCurrentBiome();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayCurrentBiome()
    {
        biomeText.text = "Current biome is: " + currentBiome.ToString();
    }

    public void SetCurrentBiome(Biomes biome)
    {
        currentBiome = biome;
    }
}
