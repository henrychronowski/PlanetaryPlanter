using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardIndicator : MonoBehaviour
{
    public Vector3 offset;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 60f, player.transform.position.z);
        transform.rotation = player.transform.rotation;
    }
}
