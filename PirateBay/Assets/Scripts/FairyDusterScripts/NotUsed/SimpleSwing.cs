/* JAR : SIMPLE SWING */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSwing : MonoBehaviour 
{
    //..............................................................
    //..................................................... SETTINGS
    //[SerializeField] private float m_TiltSpeed = 0.0f;
    private Rigidbody m_RigidBody = null;

    //..............................................................
    //SWINGING
    [SerializeField] private float m_SwingSpeed = 0.0f;
    [SerializeField] private float m_MaxSwingSpeed = 2.0f;
    [SerializeField] private float m_ReturnSpeed = 0.0f;

    private Vector3 m_SwingDir;

    private Vector3 m_SwingStartPosition;
    public Transform m_FPScamera;
    public Transform m_JarObject;

    [SerializeField] private float m_CameraOffsetJar = 0.0f;


    [SerializeField] private bool m_IsSwinging = false;
    [SerializeField] private bool m_IsRetracting = false;
    [SerializeField] private bool m_IsStopped = true;



    protected void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();

        m_SwingStartPosition = transform.localPosition;
    }

    protected void Update()
    {
        //CAMERA RELATION
        //m_SwingStartPosition = (m_FPScamera.transform.position.x + m_CameraOffsetJar.x, ;

        Vector3 m_JarVelocity;
        m_JarVelocity = m_SwingDir * m_SwingSpeed;
        m_SwingDir = Vector3.zero;

        //SWING JAR INPUT
        if (Input.GetKey(KeyCode.Space))
        {
            m_IsSwinging = true;
            m_IsStopped = false;
        }
        else
        {
            m_IsSwinging = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_IsRetracting = true;
        }


        if (m_IsSwinging == true)
        {
            m_SwingDir += Vector3.left;
        }

        if (m_IsSwinging == false && m_IsRetracting == true)
        {
            m_SwingDir += Vector3.right;
            if (transform.localPosition.x >= m_SwingStartPosition.x)
            {
                m_IsStopped = true;
                m_SwingDir += Vector3.zero;
                transform.localPosition = m_SwingStartPosition;
                m_IsRetracting = false;
            }
        }

        m_RigidBody.velocity = m_JarVelocity;
    }
}
