using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelAttack : MonoBehaviour
{
    [SerializeField] bool isSwinging;
    [SerializeField] float totalSwingTime;
    [SerializeField] float timeSinceSwingStart;
    [SerializeField, Tooltip("Yes")] float startupTime;
    [SerializeField, Tooltip("Yes")] float activeTime;
    [SerializeField, Tooltip("Yes")] float endTime;


    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && !isSwinging)
        {
            isSwinging = true;
        }
    }

    void Swing()
    {
        //set animator bool
        if(isSwinging)
        {
            timeSinceSwingStart += Time.deltaTime;
            
        }
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
