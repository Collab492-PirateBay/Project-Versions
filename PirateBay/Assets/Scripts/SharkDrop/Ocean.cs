/* OCEAN */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour 
{
    [SerializeField] private float m_WaveSpeed = 0.0f;

    private Vector3 m_WaveDir;

    [SerializeField] private float m_DistanceLimit = -1280.0f;
    private Vector3 m_SpawnLocation;


	void Start () 
    {
        m_SpawnLocation = new Vector3(-600.0f, 3.4f, 880.0f);
	}
	
	void Update () 
    {

            this.transform.Translate(0, 0, m_WaveSpeed * Time.deltaTime);

        if (transform.position.z <= m_DistanceLimit)
        {
            transform.position = m_SpawnLocation;
        }

	}
}
