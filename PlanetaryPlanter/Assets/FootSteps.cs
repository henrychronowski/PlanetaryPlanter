using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] AudioClip[] stepSounds;

    AudioSource audioSource;
    int lastPlayedSound;
    void Step()
    {
        audioSource.PlayOneShot(GetRandomClip());
    }

    AudioClip GetRandomClip()
    {
        int index = Random.Range(0, stepSounds.Length);
        while(index == lastPlayedSound && stepSounds.Length != 1)
        {
            index = Random.Range(0, stepSounds.Length);
        }
        lastPlayedSound = index;
        return stepSounds[index];
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
