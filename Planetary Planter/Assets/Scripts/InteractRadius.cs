using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void SetClosestInteractable()
    {
        Collider[] interactables = Physics.OverlapSphere(transform.position, radius, interactable);
        if (interactables.Length <= 0)
        {
            inRange = false;
            return;
        }
        inRange = true;

        float closestDistance = Vector3.Distance(interactables[0].transform.position, transform.position);
        int closestIndex = 0;
        for(int i = 1; i < interactables.Length; i++)
        {
            if (Vector3.Distance(interactables[0].transform.position, transform.position) < closestDistance)
            {
                closestDistance = Vector3.Distance(interactables[0].transform.position, transform.position);
                closestIndex = i;
            }
        }
        closestInteractable = interactables[closestIndex].gameObject.GetComponent<InteractableObject>();
        
    }

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(inRange)
            {
                closestInteractable.gameObject.GetComponent<PlantSpot>().Interact();
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
        CheckInput();
        SetClosestInteractable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255f, 255f, 0f, 5f);
        Gizmos.DrawSphere(transform.position, radius);

    }
}
