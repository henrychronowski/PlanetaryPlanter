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
    public float stuckCountdown;

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
    [SerializeField] Animator squimbusAnimator;

    GameObject[] itemsToSteal;
    InventorySpace[] spaces;

    Vector3 currentLocation;
    Vector3 pastLocation;
    float currentStuckCountdown;
    bool isStuck = false;

    bool stunned = false;
    float currentStunCountdown;
    public float stunCountdown;

    // Audio Manager Script is set up here
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        newDestinationNeeded = true;
        currentDropItemTime = dropItemTime;
        currentStealCooldownTime = stealCooldownTime;
        currentStuckCountdown = stuckCountdown;
        currentStunCountdown = stunCountdown;

        //Audio Manager Is Opened Up here
        soundManager = SoundManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //currentLocation = gameObject.transform.position;

        if (stunned == false)
        {
            if (Vector3.Distance(currentLocation, pastLocation) < 1.0f) //maybe make this a distance thing?
            {
                isStuck = true;
            }

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
            UpdateAnimValues();

            if (playerSpotted == true && itemSpotFull == true)
            {
                PickNewDestination();
            }

            //if (isStuck == true)
            //{
            //    currentStuckCountdown -= Time.deltaTime;

            //    if (currentStuckCountdown <= 0.0f)
            //    {
            //        PickNewDestination();
            //        isStuck = false;
            //    }
            //}
        }

        if (stunned == true)
        {
            currentStunCountdown -= Time.deltaTime;

            if (currentStunCountdown <= 0.0f)
            {
                currentStunCountdown = stunCountdown;
                stunned = false;
            }
        }

        //pastLocation = currentLocation;
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
        // I think this will fix it sometimes struggling to stop, sometimes its destination
        // is lower than what the terrain allows so just ignoring the destination Y val might fix it
        // -Daniel
        
        if (Vector3.Distance(gameObject.transform.position, new Vector3(destination.x, transform.position.y, destination.z)) < 2.0f)
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

        switch (Random.Range(0, 5))
        {
            case 0: soundManager.PlaySoundAtLocation("SquimAl1", transform.position); break;
            case 1: soundManager.PlaySoundAtLocation("SquimAl2", transform.position); break;
            case 2: soundManager.PlaySoundAtLocation("SquimAl3", transform.position); break;
            case 3: soundManager.PlaySoundAtLocation("SquimAl4", transform.position); break;
            case 4: soundManager.PlaySoundAtLocation("SquimAl5", transform.position); break;
        }

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
        int randItem = 0;
        bool itemFound = false;
        InventorySpace[] filledSpaces;

        int attemptsToSteal = 0;

        filledSpaces = inventory.GetComponent<NewInventory>().GetFilledSpaces();

        //could be this or a collider contact
        if (itemSpotFull == false)
        {
            randItem = Random.Range(0, filledSpaces.Length);

            stolenObject = filledSpaces[randItem].item.itemObject;
            inventory.GetComponent<NewInventory>().PopItem(filledSpaces[randItem]);
            
            TutorialManagerScript.instance.Unlock("Squimbus!");
            Debug.Log("got your nose >:)");

            switch (Random.Range(0, 2))
            {
                case 0: soundManager.PlaySoundAtLocation("SquimSt1", transform.position); break;
                case 1: soundManager.PlaySoundAtLocation("SquimSt2", transform.position); break;
            }

            itemSpotFull = true;
            ShowItem();

            Vector3 direction = (player.transform.position - transform.position).normalized;
            player.GetComponent<CharacterMovement>().AddForce((new Vector3(direction.x, knockbackAngle, direction.z)).normalized * knockback);
        
            playerSpotted = false;
            newDestinationNeeded = true;
        }
    }

    public void DropItem()
    {
        itemSpotFull = false;
        stolenObject = null; //maybe should just destroy it or something
        stunned = true;
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

    void UpdateAnimValues()
    {        
        if (thief.velocity.magnitude > 0)
            squimbusAnimator.SetBool("moving", true);
        else
            squimbusAnimator.SetBool("moving", false);

        squimbusAnimator.SetFloat("moveSpeed", thief.velocity.magnitude / thief.speed);

        if(thief.velocity.magnitude / thief.speed < 0.1f)
        {
            squimbusAnimator.SetBool("moving", false);
        }
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

    public void ChangeStunState()
    {
        if (stunned == false)
        {
            stunned = true;
        }
        else if (stunned == true)
        {
            stunned = false;
        }
    }
}