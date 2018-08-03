using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FD_Player : MonoBehaviour {

	private UIManager UI_manager;
	[SerializeField]
	private GameObject[] fairyTargets;
	private float[] distances;
	public float  distanceActive;
	public Animation mopAnim;
	private bool collectingStarted;

	public bool Test;

	// Use this for initialization
	void Start () 
	{
		UI_manager = GameObject.FindGameObjectWithTag("uiManager").GetComponent<UIManager>();
		fairyTargets = GameObject.FindGameObjectsWithTag("Target");
		distances = new float[fairyTargets.Length];
	}
	
	// Update is called once per frame
	void Update () 
	{
         distances[0] = Vector3.Distance(gameObject.transform.position, fairyTargets[0].transform.position);
		 //distances[1] = Vector3.Distance(gameObject.transform.position, fairyTargets[1].transform.position);
		 //distances[2] = Vector3.Distance(gameObject.transform.position, fairyTargets[2].transform.position);

		 //if(distances[0] < distanceActive)
		 if(Test)
		 {
		 	float m_timeElapsed = Time.deltaTime;
		 	mopAnim.Play();
			collectingStarted = true;
			if(m_timeElapsed > 5)
			{
				CollectFairy();
			}
		 }
		 else
		 {
			if(collectingStarted)
			{
				//mopAnim.Rewind();
		 		mopAnim.Stop();
		 		
				collectingStarted = false;
			}

		 }	
	}

	void CollectFairy()
	{
		UI_manager.m_FairiesObtained += 1;
	}
}
