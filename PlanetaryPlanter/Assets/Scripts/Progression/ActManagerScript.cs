using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManagerScript : MonoBehaviour
{
    public static ActManagerScript instance;
    public GameObject actCanvas;
    public List<ActScript> acts;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        acts.AddRange(GetComponents<ActScript>());
    }

    // Update is called once per frame
    void Update()
    {
        CheckActive();
    }

    void CheckActive()
    {
        if(actCanvas.activeInHierarchy)
        {
            NewInventory.instance.SetSpacesActive(true);
            Time.timeScale = 0;
        }
    }
}
