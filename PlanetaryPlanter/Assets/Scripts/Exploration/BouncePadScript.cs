using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadScript : MonoBehaviour
{
    public float bounceSpeed = 25.0f;
    public GameObject player;

    // Audio Manager Script is set up here
    private SoundManager soundManager;
    public Animation stretchAnim;

    // Start is called before the first frame update
    void Start()
    {
        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterMovement>().Bounce(bounceSpeed);
            soundManager.PlaySound("Bounce");
            stretchAnim.Play();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharacterMovement>().Bounce(bounceSpeed);
            soundManager.PlaySound("Bounce");
            stretchAnim.Play();
        }
    }
}
