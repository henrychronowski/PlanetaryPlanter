using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public PlanetGravityScript exitPlanetGravity;
    public Transform exitTransform;
    
    [SerializeField]
    bool loadNewScene;
    [SerializeField]
    int indexToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(loadNewScene)
            {
                GameObject.FindObjectOfType<SaveObjects>().LoadNewScene(indexToLoad);

            }
            else
            {
                other.gameObject.transform.position = exitTransform.position;
                other.gameObject.GetComponent<PlayerGravityScript>().planet = exitPlanetGravity;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
