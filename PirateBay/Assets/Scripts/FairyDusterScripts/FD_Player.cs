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

    private int targetNumb = 0;
    public float waitTime;

	public bool Test;

	// Use this for initialization
	void Start () 
	{
		UI_manager = GameObject.FindGameObjectWithTag("uiManager").GetComponent<UIManager>();
		//fairyTargets = GameObject.FindGameObjectsWithTag("Target");
		distances = new float[fairyTargets.Length];
        targetNumb = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
         distances[targetNumb] = Vector3.Distance(gameObject.transform.position, fairyTargets[targetNumb].transform.position);

		 //if(distances[0] < distanceActive)
		 if(Test)
		 {
            StartCoroutine("fairyCount");
		 	mopAnim.Play();
			collectingStarted = true;
            /*
			if(m_timeElapsed > 5)
			{
				CollectFairy();
			}
            */
		 }
		 else
		 {
			if(collectingStarted)
			{
				//mopAnim.Rewind();
		 		mopAnim.Stop();
		 		
				collectingStarted = false;
                StopCoroutine("fairyCount");
			}

		 }
        Debug.Log(targetNumb);
	}

	void CollectFairy()
	{
		UI_manager.m_FairiesObtained += 1;
        fairyTargets[targetNumb].GetComponent<FairyPoof>().StartCoroutine("explodeBody");
        Debug.Log("ran once");
        Test = false;
        if(targetNumb < fairyTargets.Length)
            targetNumb += 1;
	}

    private IEnumerator fairyCount()
    {
        yield return new WaitForSeconds(waitTime);
        CollectFairy();
        UI_manager.StartCoroutine("goldObtained");
    }
}
