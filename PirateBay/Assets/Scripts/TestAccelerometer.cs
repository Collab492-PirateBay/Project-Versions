using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestAccelerometer : MonoBehaviour {

	public NetworkServerUI input;

	// Use this for initialization
	void Start () {
		input = GameObject.Find("NetworkManager").GetComponent<NetworkServerUI>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(input.accelX, 0, -input.accelZ);
	}
}
