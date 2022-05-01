using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransitionScript : MonoBehaviour
{
    public AudioSource tempBiome;
    public AudioSource newBiome;
    [SerializeField] float transitionTime = 1f;
    [SerializeField] float tempVolume;
    [SerializeField] float newVolume;

    public static bool isTransitioning = false;
    public static bool transitionPending = false;
    public static IEnumerator activeCoroutine;
    public static AudioSource activeSource;

    private BoxCollider box;

    [SerializeField] bool insideTrigger;

    public enum MusicArea
    {
        FARM,
        COLD,
        HOT
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            insideTrigger = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        tempVolume = tempBiome.volume;
        newVolume = newBiome.volume;
        box = GetComponent<BoxCollider>();
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
            //Debug.Log(other.GetType());
            //if (other.GetType() == typeof(CapsuleCollider))
            //    return;
            //if(activeCoroutine == MusicFadeOut(tempBiome, tempVolume, newBiome, newVolume))
            //{
            //    Debug.Log("Duplicate transition detected");
            //    return;
            //}
            //else if(!isTransitioning)
            //    StartCoroutine(MusicFadeOut(tempBiome, tempVolume, newBiome, newVolume));
        }
    }

    IEnumerator MusicFadeOut(AudioSource stop, float stopVol, AudioSource start, float startVol)
    {
        activeCoroutine = MusicFadeOut(tempBiome, tempVolume, newBiome, newVolume);
        isTransitioning = true;
        float startTime = 0;

        while(stop.volume > 0)
        {
            startTime += Time.deltaTime;
            stop.volume = stopVol - (stopVol * (startTime / transitionTime));
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(MusicFadeIn(stop, stopVol, start, startVol));
            yield return null;
    }

    IEnumerator MusicFadeIn(AudioSource stop, float stopVol, AudioSource start, float startVol)
    {
        activeSource = start;
        float startTime = 0;
        start.volume = 0;
        if (!start.isPlaying)
            start.Play();

        while (start.volume < startVol)
        {
            startTime += Time.fixedDeltaTime;
            start.volume = startVol * (startTime / transitionTime);
            yield return new WaitForSeconds(0.1f);
        }
        start.volume = startVol;
        isTransitioning = false;
        activeCoroutine = null;
        
        List<Collider> colliders = new List<Collider>();
        colliders.AddRange(Physics.OverlapBox(box.center, box.bounds.extents, Quaternion.identity));

        if(insideTrigger)
        {
            Debug.Log("Player in " + gameObject.name);
            if (newBiome == activeSource)
                Debug.Log("Correct music playing, " + gameObject.name);
            else
            {
                Debug.Log("Incorrect music playing, " + gameObject.name);
                StartCoroutine(MusicFadeOut(newBiome, newVolume, tempBiome, tempVolume));
                yield break;
            }
        }

        yield return null;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            newBiome.Stop();
            tempBiome.Play();
            //insideTrigger = false;
            //if (other.GetType() == typeof(CapsuleCollider))
            //    return;
            //if(!isTransitioning)
            //    StartCoroutine(MusicFadeOut(newBiome, newVolume, tempBiome, tempVolume));

        }
    }
}
