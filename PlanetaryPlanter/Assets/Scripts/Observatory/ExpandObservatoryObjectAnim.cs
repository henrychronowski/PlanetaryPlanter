using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandObservatoryObjectAnim : MonoBehaviour
{
    Vector3 originalScale;
    public float timeSinceStart = 0;
    public float timeToFullyExpand = 1f;
    public GameObject particles;
    public bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        Instantiate(particles, transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceStart += Time.deltaTime;
        if(timeSinceStart < timeToFullyExpand)
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, timeSinceStart / timeToFullyExpand);
        else if(!finished)
        {
            transform.localScale = originalScale;
            finished = true;
        }
    }
}
