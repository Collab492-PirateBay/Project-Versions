using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_body : MonoBehaviour {

	public ARmarker fairyspawn;
    private ParticleSystem imageTargetParticle;

	// Use this for initialization
	void Start () {
        imageTargetParticle = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Player")
        {
            fairyspawn.enabled = true;
            imageTargetParticle.GetComponent<Renderer>().enabled = false;
        }
	}

	void OnTriggerExit(Collider other)
	{
        if (other.gameObject.tag == "Player")
        {
            fairyspawn.enabled = false;
            imageTargetParticle.GetComponent<Renderer>().enabled = true;
        }
	}
}
