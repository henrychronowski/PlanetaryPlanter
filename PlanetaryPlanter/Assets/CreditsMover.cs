using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMover : MonoBehaviour
{
    public float speed;
    public float heightToExit;
    public AkEvent mainMusic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);
        Debug.Log(transform.position.y);
        if (transform.position.y > heightToExit)
        {
            SceneManager.LoadScene(0);
            mainMusic.Stop(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
            mainMusic.Stop(0);
        }
    }
}
