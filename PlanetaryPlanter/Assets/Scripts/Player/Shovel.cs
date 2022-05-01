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
    public bool canBreakObjects;

    // Audio Manager Script is set up here
    private SoundManager soundManager;
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isSwinging && character.canMove && character.grounded)
        {
            isSwinging = true;
            character.animator.SetBool("usedShovel", true);
            soundManager.PlaySound("SwingShovel");
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
                character.animator.SetBool("usedShovel", false);

                return;
            }
            else if(timeSinceSwingStart > activeTime + startupTime) //returns true when the hitbox has exhausted its active time
            {
                character.animator.SetBool("usedShovel", false);

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
        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Swing();
    }
}
