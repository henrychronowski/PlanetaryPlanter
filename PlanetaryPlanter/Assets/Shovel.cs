using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    [SerializeField] bool isSwinging;
    [SerializeField] float totalSwingTime;
    [SerializeField] float timeSinceSwingStart;
    [SerializeField] float startupTime;
    [SerializeField] float activeTime;
    [SerializeField] AttackHitbox hitbox;
    [SerializeField] CharacterMovement character;
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isSwinging && character.canMove)
        {
            isSwinging = true;
        }
    }

    void Swing()
    {
        //set animator bool
        if (isSwinging)
        {
            timeSinceSwingStart += Time.deltaTime;
            if(timeSinceSwingStart > totalSwingTime) //returns true when total duration is complete
            {
                isSwinging = false;
                timeSinceSwingStart = 0;
                return;
            }
            else if(timeSinceSwingStart > activeTime + startupTime) //returns true when the hitbox has exhausted its active time
            {
                hitbox.isActive = false;
                return;
            }
            else if(timeSinceSwingStart > startupTime) //returns true when the hitbox should become active
            {
                hitbox.isActive = true;
                return;
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
        Swing();
    }
}
