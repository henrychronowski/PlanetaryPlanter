using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerScript : MonoBehaviour
{
    public bool canInteract = false;
    public string NPCName;
    public string PlayerName;
    public DialogueScript dialogue;
    //public TextMesh prompt;

    void Start()
    {
        dialogue = GetComponent<DialogueScript>();
    }

    void Update()
    {
        ActivateDialogue();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            canInteract = true;
            UnityEngine.Debug.Log("Entered Trigger");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            canInteract = false;
            UnityEngine.Debug.Log("Exited Trigger");
        }
    }

    void ActivateDialogue()
    {
        if (canInteract == true && Input.GetKeyDown(KeyCode.E))
        {
            canInteract = false;
            StartCoroutine(dialogue.Type());
        }
    }

    public void CutsceneDialogue()
    {
        StartCoroutine(dialogue.Type());
    }
}
