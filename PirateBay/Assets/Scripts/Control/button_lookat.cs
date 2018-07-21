using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class button_lookat : MonoBehaviour {

    public GameObject aButton_1;
    public GameObject aButton_2;
    public GameObject aButton_3;
    public GameObject aCamera;

    private Vector3 lookAtPoint;


    
    void Start ()
    {
        //Find the camera's position//
        
    }
	
	
	void Update ()
    {
        lookAtPoint = aCamera.transform.position;
        aButton_1.transform.LookAt(-lookAtPoint);
        aButton_2.transform.LookAt(-lookAtPoint);
        aButton_3.transform.LookAt(-lookAtPoint);
    }


}
