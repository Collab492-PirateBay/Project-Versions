/* RADIATE */
/* OUTER BALL OF FAIRY EXPANDS AND SHRINKS */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiate : MonoBehaviour 
{
    [SerializeField] private float m_RadiateSpeed = 0.0f;

    [SerializeField] private float m_RadiateDur = 0.0f;
    [SerializeField] private float m_RadiateLifeTimer;

    [SerializeField] private bool m_IsRadiating = false;

    //COLOR CHANGE
    private Renderer m_RadiateRenderer = null;
    public Material[] m_RadiateMaterial = null;

    [SerializeField] private GameObject m_JarObject;
    [SerializeField] private JarOrbit m_Jar;

    protected void Start()
    {
        m_RadiateLifeTimer = m_RadiateDur;

        //Getting the color of the fairy
        m_JarObject = GameObject.FindWithTag("Jar");
        m_Jar = m_JarObject.GetComponent<JarOrbit>();

        m_RadiateRenderer = GetComponent<Renderer>();
    }

    protected void Update()
    {
        //TIMER & SETTING NEW DIRECTION
        m_RadiateLifeTimer -= Time.deltaTime;

        if (m_RadiateLifeTimer <= 0)
        {
            m_RadiateLifeTimer = 0;

            if (m_IsRadiating == true)
            {
                m_IsRadiating = false;
            }
            else if (m_IsRadiating == false)
            {
                m_IsRadiating = true;
            }

            m_RadiateLifeTimer = m_RadiateDur;
        }


        //EXPANSION / SHRINKING
        if (m_IsRadiating == true)
        {
            transform.localScale = ((1.0f + m_RadiateDur - m_RadiateLifeTimer) * Vector3.one) * m_RadiateSpeed;
        }
        else if (m_IsRadiating == false)
        {
            transform.localScale = ((1.3f - m_RadiateDur - m_RadiateLifeTimer) * Vector3.one) * m_RadiateSpeed;
        }

        //COLOR CHANGE
        if (m_Jar.fairyType == "Red")
        {
            m_RadiateRenderer.sharedMaterial = m_RadiateMaterial[0];
        }
        if (m_Jar.fairyType == "Green")
        {
            m_RadiateRenderer.sharedMaterial = m_RadiateMaterial[1];
        }
        if (m_Jar.fairyType == "Blue")
        {
            m_RadiateRenderer.sharedMaterial = m_RadiateMaterial[2];
        }
        if (m_Jar.fairyType == "Yellow")
        {
            m_RadiateRenderer.sharedMaterial = m_RadiateMaterial[3];
        }

    }
}
