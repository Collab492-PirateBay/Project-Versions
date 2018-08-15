/* LIGHT FLICKER */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour 
{
    //[SerializeField] private float m_FlickerSpeed = 0.0f;

    private float m_Timer = 4.0f;
    [SerializeField] private float m_Duration = 0.0f;

	void Start () 
    {
        m_Timer = m_Duration;
	}
	
	void Update () 
    {
        m_Timer -= Time.deltaTime;

        if (m_Timer >= (m_Timer / 2))
        {
            gameObject.SetActive(true);
        }
        else if (m_Timer < (m_Timer / 2))
        {
            gameObject.SetActive(false);
        } 

        if (m_Timer <= 0)
        {
            m_Timer = m_Duration;
        }
	}
}
