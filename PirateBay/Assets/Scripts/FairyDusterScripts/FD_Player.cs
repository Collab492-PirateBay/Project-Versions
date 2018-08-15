using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FD_Player : MonoBehaviour {

    
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
	private bool firstpass;
	
	public bool Test;

	// Use this for initialization
	void Start () 
	{
		UI_manager = GameObject.FindGameObjectWithTag("uiManager").GetComponent<UIManager>();
		//fairyTargets = GameObject.FindGameObjectsWithTag("Target");

        //Sets Fairy Targets
		distances = new float[fairyTargets.Length];
        targetNumb = 0;
        
        //Gets Vuforia Instance in Game
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
		//if(!UI_manager.m_GameHasEnded)
        // Passes when number of targets is less than required number
		if(targetNumb < UI_manager.m_NumberNeededToWin)
		{
            // gets distance between current target and camera and sets animation value
			distances[targetNumb] = Vector3.Distance(gameObject.transform.position, fairyTargets[targetNumb].transform.position);
            fairyTargets[targetNumb].GetComponent<FairyPoof>().fairy_queen.GetComponent<Animator>().SetFloat("Proximity", distances[targetNumb]);

            // Activates when within a certain proximity
            // can activate from gameobject for testing purposes
            if (distances[targetNumb] < distanceActive || Test)
			{
				
				StartCoroutine("fairyCount");
				if(!firstpass)
				{
                    //Starts fairy sfx
                	fairyTargets[targetNumb].GetComponent<FairyPoof>().StartCoroutine("fairyScreech");
					firstpass = true;
				}
                // Animation
                mopAnim.Play();
				collectingStarted = true;
				/*
				if(m_timeElapsed > 5 && !UI_manager.m_GameHasEnded)
				{
					CollectFairy();
				}
				*/
			}
			else
			{
                // Activates only when player was within proximity distance
				if(collectingStarted)
				{
                    //Stops all animations, sounds and coroutines
                    mopAnim.Rewind();
					mopAnim.Stop();
                    fairyTargets[targetNumb].GetComponent<FairyPoof>().StopCoroutine("fairyScreech");
					firstpass = false;
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
	}


    // Adds to fairies collected and explodes them
	void CollectFairy()
	{
		UI_manager.m_NumberObtained += 1;
        fairyTargets[targetNumb].GetComponent<FairyPoof>().StartCoroutine("explodeBody");
        
        Test = false;
        //if(UI_manager.m_GameHasEnded == false)
        if (targetNumb < 3)
        {
            fairyTargets[targetNumb].SetActive(false);
            targetNumb += 1;
            fairyTargets[targetNumb].SetActive(true);
        }
	}

    //Starts counter for fairy dusting
    private IEnumerator fairyCount()
    {
        yield return new WaitForSeconds(waitTime);
		if(UI_manager.m_GameHasEnded == false)
		{
        	CollectFairy();
            UI_manager.StartCoroutine("tutorialTextDisplay");
        	UI_manager.StartCoroutine("goldObtained");
		}
		
    }
}
