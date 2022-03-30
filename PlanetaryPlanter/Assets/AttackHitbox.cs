using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public GameObject particles;
    public AudioSource breakRock;
    public bool isActive;
    private MeshRenderer render; //temporary
    private BoxCollider hitboxCollider;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BreakableWall")
        {
            Instantiate(particles, other.transform.position, Quaternion.identity);
            breakRock.Play();
            Destroy(other.gameObject);
        }
        if(other.tag == "Squimbus")
        {
            Debug.Log("Hit Squimbus");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        hitboxCollider = GetComponent<BoxCollider>();
        render = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        hitboxCollider.enabled = isActive;
        render.enabled = isActive;
    }
}
