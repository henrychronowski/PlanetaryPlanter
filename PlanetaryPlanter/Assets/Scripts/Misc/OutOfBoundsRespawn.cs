using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsRespawn : MonoBehaviour
{
    CharacterMovement player;
    [SerializeField] Transform spawn;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < transform.position.y)
        {
            player.Teleport(spawn.position);
            Debug.Log("Out of bounds!");
        }
    }
}
