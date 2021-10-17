using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveInScene : MonoBehaviour
{
    public int sceneIndex;

    void CheckScene()
    {
        if(SceneManager.GetActiveScene().buildIndex != sceneIndex)
        {
            MonoBehaviour[] components = GetComponents<MonoBehaviour>();
            foreach(MonoBehaviour m in components)
            {
                if(m == this)
                {
                    Debug.Log(m.GetType());
                }
                else if(m == GetComponent<Plant>())
                {
                    //keep this active no matter what so stuff can grow
                }
                else
                {
                    m.enabled = false;
                    //c.gameObject.GetComponent(c.GetType()).enabled = false;
                }
            }
        }
        else
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CheckScene();
    }
}
