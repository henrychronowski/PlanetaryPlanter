using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [System.Serializable]
    public class InteractableEvent : UnityEvent { } 

    public InteractableEvent interactableEvent = new InteractableEvent();

    public InteractableEvent onInteractableEvent { get { return interactableEvent; } set { interactableEvent = value; } }

    public void InteractableEventTriggered()
    {
        ((MonoBehaviour)interactableEvent.GetPersistentTarget(0)).SendMessage(interactableEvent.GetPersistentMethodName(0));
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
