using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_body : MonoBehaviour {

	public ARmarker fairyspawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
			fairyspawn.enabled = true;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
			fairyspawn.enabled = false;
	}
}
