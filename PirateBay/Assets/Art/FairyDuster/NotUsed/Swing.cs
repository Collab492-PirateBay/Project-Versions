/* JAR : SWING */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour 
{
    //..............................................................
    //..................................................... SETTINGS
    //[SerializeField] private float m_TiltSpeed = 0.0f;
    private Rigidbody m_RigidBody = null;

    //..............................................................
    //SWINGING
    [SerializeField] private float m_SwingSpeed = 0.0f;
    [SerializeField] private float m_MaxSwingSpeed = 0.0f;
    [SerializeField] private float m_ReturnSpeed = 0.0f;

    private Vector3 m_SwingDir;

    public Transform m_SwingStartPosition;
    public Transform m_SwingEndPosition;

    [SerializeField] private GameObject m_StartSwingWaypoint = null;
    [SerializeField] private GameObject m_EndSwingWaypoint = null;

    [SerializeField] private float m_SwingCushion = 0.0f;

    [SerializeField] private bool m_IsSwinging = false;
    [SerializeField] private bool m_IsRetracting = false;
    [SerializeField] private bool m_IsStopped = true;



    [SerializeField] private bool m_HasFairyInJar = false;



    protected void Start()
    {
        //m_RigidBody = GetComponent<Rigidbody>();
    }

    protected void Update()
    {
        //Vector3 m_JarVelocity;
        //m_JarVelocity = Vector3.zero;
        //m_SwingDir = Vector3.zero;

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

        //SETTING THE GUIDELINES FOR THE BOOLEANS
        //if (m_IsSwinging == false)
        //{
            //if (m_IsStopped == false)
            //{
                //m_IsRetracting = true;
            //}
        //}


        if (m_IsSwinging == true)
        {
            Vector3 a_SwingingsEnd = new Vector3(m_EndSwingWaypoint.transform.position.x, m_EndSwingWaypoint.transform.position.y, this.transform.position.z);

            m_SwingDir = a_SwingingsEnd - this.transform.position;

            //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(m_SwingDir), Time.deltaTime * 1);

            if (Vector3.Distance(transform.position, m_EndSwingWaypoint.transform.position) >= m_SwingCushion)
            {
                this.transform.Translate(m_SwingSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                this.transform.position = transform.position;
            }
        }

        //if (m_IsRetracting == true)
        //{
            //Vector3 a_SwingingsStart = new Vector3(m_SwingStartPosition.position.x, m_SwingStartPosition.position.y, m_SwingStartPosition.position.z);

            //m_SwingDir = a_SwingingsStart - this.transform.position;

            //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(m_SwingDir), Time.deltaTime * 1);

            //if (Vector3.Distance(transform.position, m_SwingStartPosition.position) >= m_SwingCushion)
            //{
                //this.transform.Translate(0, 0, m_SwingSpeed * Time.deltaTime);
            //}
        //}






        if (m_IsSwinging == false)
        {
            
        }
            
        //m_RigidBody.velocity = m_JarVelocity;
    }
}
