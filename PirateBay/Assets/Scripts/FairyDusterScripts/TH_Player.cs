using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TH_Player : MonoBehaviour {

	private UIManager UI_manager;
	[SerializeField]
	private GameObject[] fairyTargets;
	private float[] distances;
	public float  distanceActive;
	public Animation mopAnim;
	private bool collectingStarted;
	[SerializeField]
    private int targetNumb = 0;
    public float waitTime;

    private NetworkServerUI netControl;
    [SerializeField]
    private float speed = 5.0f;
    private Vector3 basePos;

	public bool Test;

	// Use this for initialization
	void Start () 
	{
		UI_manager = GameObject.FindGameObjectWithTag("uiManager").GetComponent<UIManager>();

        netControl = GameManager.GameManagerInstance.gameObject.GetComponent<NetworkServerUI>();

		//fairyTargets = GameObject.FindGameObjectsWithTag("Target");
		distances = new float[fairyTargets.Length];
        targetNumb = 0;
		var vuforia = VuforiaARController.Instance;
    	vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    	vuforia.RegisterOnPauseCallback(OnPaused);
		
	}

	private void OnVuforiaStarted()
	{
		CameraDevice.Instance.SetFocusMode(
		CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
	}
 
	private void OnPaused(bool paused)
	{
		if (!paused) // resumed
		{
			// Set again autofocus mode when app is resumed
			CameraDevice.Instance.SetFocusMode(
				CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
        transform.Translate(new Vector3(netControl.accelX, netControl.accelY, 0) * Time.deltaTime);
        /*
        basePos = new Vector3(netControl.accelX, netControl.accelY, netControl.accelZ);

        if (netControl.accelX > basePos.x)
            transform.Translate(Vector3.right * speed, Space.World);
        if (netControl.accelX < basePos.x)
            transform.Translate(Vector3.left * speed, Space.World);
        if (netControl.accelY > basePos.y)
            transform.Translate(Vector3.up * speed, Space.World);
        if (netControl.accelY < basePos.y)
            transform.Translate(Vector3.down * speed, Space.World);
        */
        /*
		//if(!UI_manager.m_GameHasEnded)
		if(targetNumb < 3)
		{
			distances[targetNumb] = Vector3.Distance(gameObject.transform.position, fairyTargets[targetNumb].transform.position);

			if(distances[targetNumb] < distanceActive || Test)
			{
				StartCoroutine("fairyCount");
				mopAnim.Play();
				collectingStarted = true;
				/*
				if(m_timeElapsed > 5 && !UI_manager.m_GameHasEnded)
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
					StopCoroutine("fairyCount");
				}
			}
		 }
		 else
		 {
		 	mopAnim.Stop();
			StopCoroutine("fairyCount");
		}
        //Debug.Log(targetNumb);
        */
    }

    void CollectFairy()
	{
		UI_manager.m_FairiesObtained += 1;
        fairyTargets[targetNumb].GetComponent<FairyPoof>().StartCoroutine("explodeBody");
        //Debug.Log("ran once");
        Test = false;
        //if(UI_manager.m_GameHasEnded == false)
		if(targetNumb < 3)
            targetNumb += 1;
	}

    private IEnumerator fairyCount()
    {
        yield return new WaitForSeconds(waitTime);
		if(UI_manager.m_GameHasEnded == false)
		{
        	CollectFairy();
        	UI_manager.StartCoroutine("goldObtained");
		}
		
    }
}
