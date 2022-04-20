using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetSteps : MonoBehaviour
{
	public float stepRate = 0.5f;
	public float stepCoolDown;
	public AudioClip footStep;
	public AudioSource feets;


	// Update is called once per frame
	void Update()
	{
		stepCoolDown -= Time.deltaTime;
		if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && stepCoolDown < 0f)
		{
			feets.pitch = 1f + Random.Range(-0.2f, 0.2f);
			feets.PlayOneShot(footStep, 0.9f);
			stepCoolDown = stepRate;
		}
	}
}

