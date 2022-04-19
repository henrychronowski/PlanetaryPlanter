using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] AudioClip[] stepSounds;

    AudioSource audioSource;

    void Step()
    {
        audioSource.PlayOneShot(GetRandomClip());
    }

    AudioClip GetRandomClip()
    {
        return stepSounds[Random.Range(0, stepSounds.Length)];
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
