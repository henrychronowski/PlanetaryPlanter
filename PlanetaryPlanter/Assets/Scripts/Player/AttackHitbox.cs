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
           // breakRock.Play();
            Destroy(other.gameObject);
            soundManager.PlaySound("BreakRocks");
        }
        if(other.tag == "Squimbus")
        {
            Debug.Log("Hit Squimbus");
            bonk.RecoverStolenItemWithBonk(other.gameObject);
             switch (Random.Range(0, 3))
        {
            case 0: soundManager.PlaySound("SquimWh1"); break;
            case 1: soundManager.PlaySound("SquimWh2"); break;
            case 2: soundManager.PlaySound("SquimWh3"); break;
            case 3: soundManager.PlaySound("SquimWh4"); break;
            
        }
            soundManager.PlaySound("HitSquim");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        hitboxCollider = GetComponent<BoxCollider>();
        //Audio Manager Is Opend Up here
        soundManager = SoundManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        hitboxCollider.enabled = isActive;
    }
}
