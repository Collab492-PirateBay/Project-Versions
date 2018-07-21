/* JAR ORIENTATION */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateJarOrientation : MonoBehaviour 
{
    public JarOrbit m_JarOrbit;

    [SerializeField] private float m_RotationSpeed = 0.0f;

    [SerializeField] public GameObject m_Lid;
    [SerializeField] public GameObject m_CaughtFairy;
    //make an object or material change for each fairy type


    //...........................................................
    //SWING DEINITIONS
    private Vector3 m_SwingPosition;
    private Quaternion m_SwingRotation;

    [SerializeField] private float m_SwingPosX = 0.0f;
    [SerializeField] private float m_SwingPosY = 0.0f;
    [SerializeField] private float m_SwingPosZ = 0.0f;

    [SerializeField] private float m_SwingRotX = 0.0f;
    [SerializeField] private float m_SwingRotY = 0.0f;
    [SerializeField] private float m_SwingRotZ = 0.0f;

    //...........................................................
    //SHAKE DEFINITIONS
    private Vector3 m_ShakePosition;
    private Quaternion m_ShakeRotation;

    [SerializeField] private float m_ShakePosX = 0.2f;
    [SerializeField] private float m_ShakePosY = -0.2f;
    [SerializeField] private float m_ShakePosZ = 0.8f;

    [SerializeField] private float m_ShakeRotX = 0.0f;
    [SerializeField] private float m_ShakeRotY = 0.0f;
    [SerializeField] private float m_ShakeRotZ = 0.0f;



	void Start () 
    {
        GameObject m_Jar = GameObject.FindGameObjectWithTag("Jar");
        m_JarOrbit = m_Jar.GetComponent<JarOrbit>();

        m_SwingPosition = transform.localPosition;
        m_SwingRotation = transform.localRotation;

        //m_ShakePosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        //m_ShakeRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z);
        //^^replaced with below two lines...
        m_ShakePosition = new Vector3(m_ShakePosX, m_ShakePosY, m_ShakePosZ);
        m_ShakeRotation = Quaternion.Euler(m_ShakeRotX, m_ShakeRotY, m_ShakeRotZ);
	}
	
	void Update () 
    {
        if (m_JarOrbit.m_HasCaughtAFairy == true)
        {
            transform.localPosition = m_ShakePosition;
            transform.localRotation = Quaternion.Slerp(m_ShakeRotation, m_ShakeRotation, Time.deltaTime * m_RotationSpeed);
            m_Lid.SetActive(true);
            m_CaughtFairy.SetActive(true);
        }
        else if (m_JarOrbit.m_HasCaughtAFairy == false)
        {
            transform.localPosition = m_SwingPosition;
            transform.localRotation = m_SwingRotation;
            m_Lid.SetActive(false);
            m_CaughtFairy.SetActive(false);
        }

	}
}
