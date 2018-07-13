using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkClientUI : MonoBehaviour {

	static NetworkClient client;
	bool isConnecting;
	public string stringToEdit = "Enter IP Address";

	void OnGUI()
	{
		string ipaddress = Network.player.ipAddress;
		GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
		GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Status: " + client.isConnected);

		if(!client.isConnected)
		{
			stringToEdit = GUI.TextField(new Rect(Screen.width/2 -60,Screen.height/2,120,20), stringToEdit, 25);
			if(GUI.Button(new Rect(Screen.width/2-50,10,100,90), "Connect"))
			{
				
				Connect(stringToEdit);
			}
		}
	}

	// Use this for initialization
	void Start () {
		//NetworkDiscovery.StartAsClient();
		client = new NetworkClient();
	}
	
	void Connect(string connectAddress)
	{
		isConnecting = true;
		client.Connect(connectAddress, 25000);
	}

	static public void SendAccelerometerInfo(Vector3 deltaAccel)
	{
		float xdelta;
		float ydelta;
		float zdelta;

		if(client.isConnected)
		{
			xdelta = deltaAccel.x;
			ydelta = deltaAccel.y;
			zdelta = deltaAccel.z; 

			StringMessage msg = new StringMessage();
			msg.value = xdelta + "|" + ydelta + "|" + zdelta;
			client.Send(888, msg);
		}
	}

	// Update is called once per frame
	void Update () {

	if(!client.isConnected)
	{
		if(isConnecting)
		{
			GUI.Box(new Rect(Screen.width/2, Screen.height/2, Screen.width/2 + 100, Screen.height/2 + 50), "Connecting...");
		}

		
	}
	
	if(client.isConnected)
	{
		SendAccelerometerInfo(Input.acceleration);
	}
	}
}
