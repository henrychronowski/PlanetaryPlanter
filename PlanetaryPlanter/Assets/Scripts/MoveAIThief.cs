using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAIThief : MonoBehaviour
{
    public float moveSpeed; //how fast the ai will move
    public NavMeshAgent thief;

    public GameObject player; //to know the player to target
    public Transform detectionRadius; //how close the player must be to be noticed
    public Transform movementRadius; //the range on the map the ai can move

    Vector3 destination;
    bool newDestinationNeeded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<NavMeshAgent>().SetDestination();

        CheckThiefLocation();
    }

    void CheckThiefLocation()
    {
        if (gameObject.transform.position == destination)
        {
            //pick new location to move to
            newDestinationNeeded = true;
        }
    }
}
