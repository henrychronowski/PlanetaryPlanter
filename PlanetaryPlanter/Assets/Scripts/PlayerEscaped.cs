using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEscaped : MonoBehaviour
{
    public GameObject player;
    public GameObject thief;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            thief.GetComponent<MoveAIThief>().PlayerEscaped();
        }

        if (other.gameObject == thief)
        {
            //maybe a different function but this should tell it to move back into the zone
            thief.GetComponent<MoveAIThief>().PlayerEscaped();
        }
    }
}
