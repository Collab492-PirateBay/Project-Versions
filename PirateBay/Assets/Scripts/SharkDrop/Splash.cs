using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour 
{

    private float m_Timer;
    [SerializeField] private float m_Dur = 0.0f;

	void Start () 
    {
        m_Timer = m_Dur;
	}
	
	void Update () 
    {
        m_Timer -= Time.deltaTime;

        if (m_Timer <= 0)
        {
            m_Timer = 0;
            Destroy(gameObject);
        }
	}
}
