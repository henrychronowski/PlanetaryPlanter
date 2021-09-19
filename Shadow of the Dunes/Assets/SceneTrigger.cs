using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public PlayableDirector dir;
    public bool played = false;
    public string SceneNameToLoad;
    public enum TriggerType
    {
        Cutscene,
        SceneLoad
    }

    public TriggerType triggerType;

    private void OnTriggerEnter(Collider other)
    {
        if(triggerType == TriggerType.Cutscene)
        {
            if(other.tag == "Player")
            {
                if(!played)
                {
                    played = true;
                    dir.Play();
                }
            }
        }

        if(triggerType == TriggerType.SceneLoad)
        {
            if (other.tag == "Player")
            {
                SceneManager.LoadScene(SceneNameToLoad);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //dir.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
