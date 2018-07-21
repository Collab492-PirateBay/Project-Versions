/* JAR : SWING & SHAKE */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour 
{
    //..............................................................
    //..................................................... SETTINGS
    [SerializeField] private float m_RotationSpeed = 0.0f;

    //..............................................................
    //SWINGING
    [SerializeField] private float m_SwingSpeed = 0.0f;
    [SerializeField] private float m_MaxSwingSpeed = 0.0f;

    private Vector3 m_SwingDir;

    public Transform m_SwingStartPosition;
    public Transform m_SwingEndPosition;

    [SerializeField] private float m_SwingCushionStart = 0.0f;
    [SerializeField] private float m_SwingCushionEnd = 0.0f;

    [SerializeField] private bool m_IsSwinging = false;



    //..............................................................
    //SHAKING
    [SerializeField] private bool m_HasFairyInJar = false;

    [SerializeField] private bool m_StartedShaking = false;
    [SerializeField] private float m_ShakeSpeed = 0.0f;
    [SerializeField] private float m_MaxShakeSpeed = 0.0f;

    private Vector3 m_ShakeDir;

    public Transform m_ShakeStartPosition;
    public Transform m_ShakeEndPosition;

    [SerializeField] private bool m_IsShaking = false;



	protected void Start () 
    {
        
	}
	
	protected void Update () 
    {

        //SWING JAR INPUT

        if (Input.GetKey(KeyCode.Space))
        {
            m_IsSwinging = true;
        }
        else
            m_IsSwinging = false;


        if (m_IsSwinging == true)
        {
            Vector3 m_SwingEndPoint = new Vector3(m_SwingEndPosition.transform.position.x, m_SwingEndPosition.transform.position.y, m_SwingEndPosition.transform.position.z);

            m_SwingDir = m_SwingEndPoint - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(m_SwingDir), Time.deltaTime * m_RotationSpeed);

            this.transform.Translate(0, 0, m_SwingSpeed * Time.deltaTime);


            if (m_SwingDir.magnitude < m_SwingCushionEnd)
            {
                //iWaypointA++;
                //if (iWaypointA == s_PathA.Length)
                //{
                    
                //}
            }
        }
        else
        {
            if (m_IsSwinging == false)
            {
                Vector3 m_SwingStartPoint = new Vector3(m_SwingStartPosition.transform.position.x, m_SwingStartPosition.transform.position.y, m_SwingStartPosition.transform.position.z);

                m_SwingDir = m_SwingStartPoint - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(m_SwingDir), Time.deltaTime * m_RotationSpeed);

                this.transform.Translate(0, 0, m_SwingSpeed * Time.deltaTime);


                if (m_SwingDir.magnitude < m_SwingCushionStart)
                {
                    //iWaypointB++;
                    //if (iWaypointB == s_PathB.Length)
                    //{
                        //s_YouWinText.text = "MISSION ACCOMPLISHED!";
                        //gameObject.SetActive(false);
                    //}
                }
            }

        }


	}
}
