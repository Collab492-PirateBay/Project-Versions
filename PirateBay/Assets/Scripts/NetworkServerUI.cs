using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
//using UnityStandardAssets.CrossPlatformInput;

public class NetworkServerUI : MonoBehaviour {

	//CrossPlatformInputManager.VirtualAxis m_AccelAxis;
	//string accelerometerAxisName = "acceleration";
	public float accelX;
	public float accelY;
	public float accelZ;
	
	void OnGUI()
	{
		string ipaddress = Network.player.ipAddress;
		GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
		GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status: " + NetworkServer.active);
		GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connected: " + NetworkServer.connections.Count);

		//GUI.Box(new Rect(Screen.width/2 - 100, Screen.height/2 - 25, 250, 50), "Acceleration");
		//GUI.Label(new Rect(Screen.width/2 -85, Screen.height/2 -5, 250, 20), accelX + ", " + accelY + ", " + accelZ);
	}

	// Use this for initialization
	void Start () {

		//m_AccelAxis = new CrossPlatformInputManager.VirtualAxis(accelerometerAxisName);
		//CrossPlatformInputManager.RegisterVirtualAxis(m_AccelAxis);
		
		//NetworkDiscovery.StartAsServer();
		NetworkServer.Listen(25000);
		NetworkServer.RegisterHandler(888, ServerReceiveMessage);
	}

	private void ServerReceiveMessage(NetworkMessage message)
	{
		//Debug.Log("Message Received");
		StringMessage msg = new StringMessage ();
		msg.value = message.ReadMessage<StringMessage>().value;

		string[] deltas = msg.value.Split('|');
		//m_AccelAxis.Update(new Vector3(Convert.ToSingle(deltas[0]), Convert.ToSingle(deltas[1]), Convert.ToSingle(deltas[2])));

		accelX = Convert.ToSingle(deltas[0]);
		accelY = Convert.ToSingle(deltas[1]);
		accelZ = Convert.ToSingle(deltas[2]);		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		

	}
}
