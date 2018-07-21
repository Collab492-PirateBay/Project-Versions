using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestAccelerometer : MonoBehaviour {

	public NetworkServerUI input;
    public float speed = 0.25f;

	// Use this for initialization
	void Start () {
		input = GameObject.Find("NetworkManager").GetComponent<NetworkServerUI>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(input.accelX * speed, 0, -input.accelZ * speed);
	}
}
