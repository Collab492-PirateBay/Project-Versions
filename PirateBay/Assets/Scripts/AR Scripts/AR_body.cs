using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AR_body : MonoBehaviour, ITrackableEventHandler {

	public GameObject fairyspawn;
    private ParticleSystem imageTargetParticle;
    private GameObject playerCharacter;
    public float  distanceActive;

    private TrackableBehaviour mTrackableBehaviour;
    // Use this for initialization
    void Start () {
        imageTargetParticle = GetComponent<ParticleSystem>();
        playerCharacter = GameObject.FindGameObjectWithTag("Player");

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(
                                   TrackableBehaviour.Status previousStatus,
                                   TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // Play audio when target is found
            Instantiate(fairyspawn, transform.position, Quaternion.identity);
        }
        else
        {
            // Stop audio when target is lost
            
        }
    }

    // Update is called once per frame
    void Update () {
        /*
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
        */
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
