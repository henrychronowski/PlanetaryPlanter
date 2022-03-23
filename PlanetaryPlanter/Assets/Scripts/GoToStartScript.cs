using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToStartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoToStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GoToStart()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(0);
    }
}
