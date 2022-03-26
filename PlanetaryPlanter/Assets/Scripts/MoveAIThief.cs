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
    GameObject stolenObject; //gameobject or what?
    bool newDestinationNeeded = false;
    bool playerSpotted = false;

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
            Debug.Log("picking new destination");
            
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

            destination = new Vector3(randX, 1, randZ);
            gameObject.GetComponent<NavMeshAgent>().SetDestination(destination);

            newDestinationNeeded = false;
        }

        if (playerSpotted == true)
        {
            Debug.Log("updating player position");
            //destination = player.transform.position;
            gameObject.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }

        CheckThiefLocation();
    }

    void CheckThiefLocation()
    {
        //maybe change this to a distance close to the destination (with a slight pause?)
        if (Vector3.Distance(gameObject.transform.position, destination) < 2.0f)
        {
            //pick new location to move to
            newDestinationNeeded = true;
        }
    }

    public void PlayerNoticed()
    {
        //make the ai move after the player now
        Debug.Log("player spotted");
        gameObject.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        playerSpotted = true;
    }

    public void PlayerEscaped()
    {
        //have the ai go back to random wandering
        Debug.Log("player escaped");
        newDestinationNeeded = true;
        playerSpotted = false;
    }

    void StealFromPlayer()
    {
        //take an item from the player inventory or something?
    }
}
