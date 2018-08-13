/* OCEAN */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour 
{
    [SerializeField] private float m_WaveSpeed = 0.0f;

    private Vector3 m_WaveDir;


	void Start () 
    {
		
	}
	
	void Update () 
    {
        //Vector3 waveDirection = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        ////direction waves are moving
        //m_WaveDir = waveDirection - this.transform.position;

        ////if the distance between the enemy and it's target is greater than the cushion, then move towards it.
        //if (Vector3.Distance(transform.position, enemy_TargetHero.position) >= enemy_TargetHeroCushion)
        //{
            //move towards target on local z-axis (it moves forward)
            this.transform.Translate(0, 0, m_WaveSpeed * Time.deltaTime);
        //}

	}
}
