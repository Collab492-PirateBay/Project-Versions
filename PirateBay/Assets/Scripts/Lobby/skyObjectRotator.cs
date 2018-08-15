using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyObjectRotator : MonoBehaviour
{
 
    public GameObject lookAtObject;
    private Vector3 lookAtPoint;
    public float rotSpeed = 3.0f;


    void Start()
    {
        lookAtPoint = lookAtObject.transform.position;

    }
   

    void Update()
    {
        this.transform.LookAt(lookAtPoint);
        this.transform.Translate(Vector3.right * rotSpeed * Time.deltaTime);
    }
  




}