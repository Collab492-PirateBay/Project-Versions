using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullRotator : MonoBehaviour {

    public GameObject lookAtPoint;

    public Vector3 axis = Vector3.up;
    public Vector3 bufferPosition;
    public float radius = 12;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 80.0f;
    private Transform centerPoint;
    void Start()
    {
        centerPoint = lookAtPoint.transform;
        transform.position = (transform.position - centerPoint.position).normalized * radius + centerPoint.position;
        
    }

    void Update()
    {
        transform.RotateAround(centerPoint.position, axis, -rotationSpeed * Time.deltaTime);
        bufferPosition = (transform.position - centerPoint.position).normalized * radius + centerPoint.position;
        transform.position = Vector3.MoveTowards(transform.position, bufferPosition, Time.deltaTime * radiusSpeed);
    }

}
