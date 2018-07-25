using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_body : MonoBehaviour {

	public ARmarker fairyspawn;
    private ParticleSystem imageTargetParticle;
    private GameObject playerCharacter;
    public float  distanceActive;
	// Use this for initialization
	void Start () {
        imageTargetParticle = GetComponent<ParticleSystem>();
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        float dist = Vector3.Distance(gameObject.transform.position, playerCharacter.transform.position);

        if (dist < distanceActive)
        {
            fairyspawn.enabled = true;
            imageTargetParticle.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            //fairyspawn.enabled = false;
            //imageTargetParticle.GetComponent<Renderer>().enabled = true;
        }
    }
    /*
	void OnTriggerStay(Collider other)
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
    */
}
