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
    [SerializeField] private Transform spawnOffsetLeft;// = new Vector3(0.0f, -0.1f, 0.0f);
    [SerializeField] private Transform spawnOffsetRight;// = new Vector3(0.0f, -0.1f, 0.0f);


    CharacterMovement player;
    [SerializeField] float stepVolume = 1.0f;
    [SerializeField] bool useLocation = true;
    [SerializeField] bool groundedRequired = true;


    void Step()
    {
        if (!player.grounded && groundedRequired)
            return;
        AudioClip clip = GetRandomClip();

        if(useLocation)
            AudioSource.PlayClipAtPoint(GetRandomClip(), transform.position, stepVolume);
        else
            audioSource.PlayOneShot(clip, stepVolume);


    }

    void StepRight()
    {
        Step();

        GameObject particle = Instantiate(stepParticles, spawnOffsetRight.position, transform.rotation);
        particle.transform.Rotate(Vector3.up, player.gameObject.transform.rotation.y);
    }

    void StepLeft()
    {
        Step();

        GameObject particle = Instantiate(stepParticles, spawnOffsetLeft.position, transform.rotation);
        particle.transform.Rotate(Vector3.up, player.gameObject.transform.rotation.y);
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
        player = FindObjectOfType<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
