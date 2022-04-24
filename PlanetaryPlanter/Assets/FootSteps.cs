using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] AudioClip[] stepSounds;
    [SerializeField] GameObject stepParticles;
    AudioSource audioSource;
    int lastPlayedSound;
    [SerializeField] private Vector3 spawnOffset;// = new Vector3(0.0f, -0.1f, 0.0f);

    void Step()
    {
        audioSource.PlayOneShot(GetRandomClip());

        Instantiate(stepParticles, transform.position + spawnOffset, transform.rotation);
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
