using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WwiseVolumeScript : MonoBehaviour
{
    public Slider volumeSlider;
    public float musicVolume;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume()
    {
        float sliderValue = volumeSlider.value;
        musicVolume = volumeSlider.value;
        AkSoundEngine.SetRTPCValue("MusicVolume", musicVolume);
    }
}
