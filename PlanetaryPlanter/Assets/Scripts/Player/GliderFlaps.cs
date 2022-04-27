using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderFlaps : MonoBehaviour
{
	public float stepRate = 0.5f;
	public float stepCoolDown;
	public AudioClip MelWing;
	public AudioSource flap;


	// Update is called once per frame
	void Update()
	{
		stepCoolDown -= Time.deltaTime;
		if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && stepCoolDown < 0f)
		{
			flap.pitch = 1f + Random.Range(-0.2f, 0.2f);
			flap.PlayOneShot(MelWing, 0.9f);
			stepCoolDown = stepRate;
		}
	}
}
