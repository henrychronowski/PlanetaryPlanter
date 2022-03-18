using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAIThief : MonoBehaviour
{
    public NavMeshAgent thief; //could be unnecessary

    public GameObject player; //to know the player to target
    public Transform detectionRadius; //how close the player must be to be noticed
    public Transform movementRadius; //the range on the map the ai can move

    Vector3 destination;
    bool newDestinationNeeded = false;

    // Start is called before the first frame update
    void Start()
    {
        newDestinationNeeded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (newDestinationNeeded == true)
        {
            float randX = 0.0f;
            float randZ = 0.0f;

            randX = Random.Range(movementRadius.transform.position.x -
                movementRadius.GetComponent<SphereCollider>().radius,
                movementRadius.transform.position.x +
                movementRadius.GetComponent<SphereCollider>().radius);

            randZ = Random.Range(movementRadius.transform.position.z -
                movementRadius.GetComponent<SphereCollider>().radius,
                movementRadius.transform.position.z +
                movementRadius.GetComponent<SphereCollider>().radius);

            //gameObject.GetComponent<NavMeshAgent>().SetDestination();
        }

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
