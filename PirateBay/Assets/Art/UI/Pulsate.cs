/* PULSATE */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsate : MonoBehaviour 
{
    [SerializeField] private float m_PulsateSpeed = 0.0f;

    [SerializeField] private float m_PulsateDur = 0.0f;
    [SerializeField] private float m_PulsateLifeTimer;

    [SerializeField] private bool m_IsPulsating = false;



    protected void Start()
    {
        m_PulsateLifeTimer = m_PulsateDur;
    }

    protected void Update()
    {
        //TIMER & SETTING NEW DIRECTION
        m_PulsateLifeTimer -= Time.deltaTime;

        if (m_PulsateLifeTimer <= 0)
        {
            m_PulsateLifeTimer = 0;

            if (m_IsPulsating == true)
            {
                m_IsPulsating = false;
            }
            else if (m_IsPulsating == false)
            {
                m_IsPulsating = true;
            }

            m_PulsateLifeTimer = m_PulsateDur;
        }


        //EXPANSION / SHRINKING
        if (m_IsPulsating == true)
        {
            transform.localScale = ((1.0f + m_PulsateDur - m_PulsateLifeTimer) * Vector3.one) * m_PulsateSpeed;
        }
        else if (m_IsPulsating == false)
        {
            //THIS FLIPS IT UPSIDE DOWN AND SHRINKS IT...try to just shrink it
            transform.localScale = ((1.3f - m_PulsateDur - m_PulsateLifeTimer) * Vector3.one) * m_PulsateSpeed;
        }

    }
}
