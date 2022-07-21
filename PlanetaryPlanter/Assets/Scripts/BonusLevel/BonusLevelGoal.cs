using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelGoal : MonoBehaviour
{
    public void FinishLevel()
    {
        FindObjectOfType<BonusLevelMaster>().Deactivate(true);
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
