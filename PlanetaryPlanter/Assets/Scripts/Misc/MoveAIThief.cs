using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MoveAIThief : MonoBehaviour
{
    public NavMeshAgent thief; //could be unnecessary

    public GameObject player; //to know the player to target
    public GameObject inventory;
    public Transform detectionRadius; //how close the player must be to be noticed
    public Transform movementRadius; //the range on the map the ai can move
    public float dropItemTime;
    public float stealCooldownTime;

    Vector3 destination;
    [SerializeField] GameObject stolenObject; //gameobject or what?
    bool newDestinationNeeded = false;
    bool playerSpotted = false;
    bool itemSpotFull = false;
    float currentDropItemTime;
    bool stealCooldownActive = false;
    float currentStealCooldownTime;

    [SerializeField] float knockback;
    [SerializeField] float knockbackAngle;
    [SerializeField] Image itemIcon;

    // Start is called before the first frame update
    void Start()
    {
        newDestinationNeeded = true;
        currentDropItemTime = dropItemTime;
        currentStealCooldownTime = stealCooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (stealCooldownActive == true)
        {
            currentStealCooldownTime -= Time.deltaTime;

            if (currentStealCooldownTime <= 0.0f)
            {
                currentStealCooldownTime = stealCooldownTime;
                stealCooldownActive = false;
            }
        }

        if (playerSpotted == true && stealCooldownActive == false)
        {
            Debug.Log("updating player position");

            destination = player.transform.position;
            gameObject.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }

        if (newDestinationNeeded == true)
        {
            Debug.Log("picking new destination");

            PickNewDestination();

            newDestinationNeeded = false;
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
        else
        {
            HideItem();
        }

        if (Vector3.Distance(thief.transform.position, movementRadius.transform.position)
            > movementRadius.GetComponent<SphereCollider>().radius)
        {
            playerSpotted = false;
            newDestinationNeeded = true;
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
        if (Vector3.Distance(gameObject.transform.position, destination) < 1.0f && stealCooldownActive == false)
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
                    ShowItem();
                    
                }

                Vector3 direction = (player.transform.position - transform.position).normalized;
                player.GetComponent<CharacterMovement>().AddForce((new Vector3(direction.x, knockbackAngle, direction.z)).normalized * knockback);

                playerSpotted = false;
                newDestinationNeeded = true;
            }
        }
    }

    public void DropItem()
    {
        itemSpotFull = false;
        stolenObject = null; //maybe should just destroy it or something
    }

    public void ShowItem()
    {
        itemIcon.enabled = true;
        itemIcon.sprite = stolenObject.GetComponent<IconHolder>().icon;
    }

    public void HideItem()
    {
        itemIcon.enabled = false;
    }

    public GameObject GetStolenObject()
    {
        return stolenObject;
    }

    public void ChangeDestinationNeeded(bool value)
    {
        newDestinationNeeded = value;
    }

    public void ChangePlayerSpotted(bool val)
    {
        playerSpotted = val;
    }

    public void StealItemsCooldown()
    {
        stealCooldownActive = true;
        newDestinationNeeded = true;
    }
}