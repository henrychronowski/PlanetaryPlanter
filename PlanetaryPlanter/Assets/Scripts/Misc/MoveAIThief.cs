using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAIThief : MonoBehaviour
{
    public NavMeshAgent thief; //could be unnecessary

    public GameObject player; //to know the player to target
    public GameObject inventory;
    public Transform detectionRadius; //how close the player must be to be noticed
    public Transform movementRadius; //the range on the map the ai can move
    public float dropItemTime;

    Vector3 destination;
    [SerializeField] GameObject stolenObject; //gameobject or what?
    bool newDestinationNeeded = false;
    bool playerSpotted = false;
    bool itemSpotFull = false;
    float currentDropItemTime;

    // Start is called before the first frame update
    void Start()
    {
        newDestinationNeeded = true;
        currentDropItemTime = dropItemTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (newDestinationNeeded == true)
        {
            Debug.Log("picking new destination");

            PickNewDestination();

            newDestinationNeeded = false;
        }

        if (playerSpotted == true)
        {
            Debug.Log("updating player position");

            destination = player.transform.position;
            gameObject.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }

        if (itemSpotFull == true)
        {
            currentDropItemTime -= Time.deltaTime;

            if (currentDropItemTime <= 0.0f)
            {
                DropItem();
                currentDropItemTime = dropItemTime;
            }
        }

        CheckThiefLocation();
    }

    void PickNewDestination()
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
    }

    void CheckThiefLocation()
    {
        //maybe change this to a distance close to the destination (with a slight pause?)
        if (Vector3.Distance(gameObject.transform.position, destination) < 2.0f)
        {
            //pick new location to move to
            newDestinationNeeded = true;
        }

        if (playerSpotted == true && Vector3.Distance(gameObject.transform.position, destination) < 2.0f)
        {
            StealFromPlayer();
        }
    }

    public void PlayerNoticed()
    {
        //make the ai move after the player now
        Debug.Log("player spotted");

        destination = player.transform.position;
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

    void StealFromPlayer() //take an item from the player inventory
    {
        int randItem;

        //could be this or a collider contact
        if (Vector3.Distance(gameObject.transform.position, destination) < 1.0f)
        {
            if (inventory.GetComponent<NewInventory>().spaces.Count > 0 && itemSpotFull == false)
            {
                randItem = (int)Random.Range(0, inventory.GetComponent<NewInventory>().spaces.Count);
                bool itemFound = false;

                if (inventory.GetComponent<NewInventory>().spaces[randItem].filled == false)
                {
                    for (randItem = 0; randItem < inventory.GetComponent<NewInventory>().spaces.Count; randItem++)
                    {
                        if (inventory.GetComponent<NewInventory>().spaces[randItem].filled == true)
                        {
                            itemFound = true;
                            break;
                        }
                    }
                }

                if (itemFound == true)
                {
                    TutorialManagerScript.instance.Unlock("Squimbus!");
                    Debug.Log("got your nose :)");
                    stolenObject = inventory.GetComponent<NewInventory>().GetItem
                        (inventory.GetComponent<NewInventory>().spaces[randItem]);
                    inventory.GetComponent<NewInventory>().PopItem(
                       inventory.GetComponent<NewInventory>().spaces[randItem]);
                    itemSpotFull = true;
                }

                playerSpotted = false;
                newDestinationNeeded = true;
            }
        }
    }

    public void DropItem()
    {
        itemSpotFull = false;
        stolenObject = null;
    }

    public GameObject GetStolenObject()
    {
        return stolenObject;
    }

    public void ChangeDestinationNeeded(bool value)
    {
        newDestinationNeeded = value;
    }
}