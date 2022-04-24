using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class InteractRadius : MonoBehaviour
{
    [SerializeField]
    LayerMask interactable;

    [SerializeField]
    float radius;

    [SerializeField]
    InteractableObject closestInteractable;

    [SerializeField]
    bool inRange;


    public GameObject highlight;
    public GameObject interactTip;
    public SpriteRenderer interactSprite;
    public Transform interactBubble;

    void SetClosestInteractable()
    {
        Collider[] interactables = Physics.OverlapSphere(interactBubble.position, radius, interactable);
        if (interactables.Length <= 0)
        {
            inRange = false;
            return;
        }
        inRange = true;

        float closestDistance = Vector3.Distance(interactables[0].transform.position, interactBubble.position);
        int closestIndex = 0;
        for(int i = 1; i < interactables.Length; i++)
        {
            if (Vector3.Distance(interactables[i].transform.position, interactBubble.position) < closestDistance)
            {
                closestDistance = Vector3.Distance(interactables[i].transform.position, interactBubble.position);
                closestIndex = i;
            }
        }
        closestInteractable = interactables[closestIndex].gameObject.GetComponent<InteractableObject>();
        interactSprite.sprite = closestInteractable.interactSprite;
        
    }

    void CheckInput()
    {
        if(inRange)
        {
            highlight.SetActive(true);
            interactSprite.gameObject.SetActive(true);
            
            
            highlight.transform.position = closestInteractable.transform.position;
            highlight.transform.rotation = closestInteractable.transform.rotation;
            highlight.transform.Rotate(90, 0, 0);
            highlight.GetComponentInChildren<Light>().spotAngle = closestInteractable.interactLightAngle;
            if(Input.GetKeyDown(KeyCode.E))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().animator.SetBool("Interacting", true);
                //closestInteractable.gameObject.GetComponent<PlantSpot>().Interact();
                closestInteractable.gameObject.GetComponent<InteractableObject>().InteractableEventTriggered();
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().animator.SetBool("Interacting", false);
            }
        }
        else
        {
            highlight.SetActive(false);
            interactSprite.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().animator.SetBool("Interacting", false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        SetClosestInteractable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255f, 255f, 0f, .4f);
        Gizmos.DrawSphere(interactBubble.position, radius);

    }
}
