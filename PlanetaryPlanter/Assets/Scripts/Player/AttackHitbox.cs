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
    public bool canBreakRocks = false;
    public BonkAIThief bonk;

    // Audio Manager Script is set up here
    private SoundManager soundManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BreakableWall" && canBreakRocks)
        {
            Instantiate(particles, other.transform.position, Quaternion.identity);
            breakRock.Play();
            Destroy(other.gameObject);
            soundManager.PlaySound("RockSmash");
        }
        if(other.tag == "Squimbus")
        {
            Debug.Log("Hit Squimbus");
            bonk.RecoverStolenItemWithBonk(other.gameObject);
            soundManager.PlaySound("SquimHit");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        hitboxCollider = GetComponent<BoxCollider>();
        render = GetComponent<MeshRenderer>();
        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        hitboxCollider.enabled = isActive;
        render.enabled = isActive;
    }
}
