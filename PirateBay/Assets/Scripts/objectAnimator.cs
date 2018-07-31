using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectAnimator : MonoBehaviour {

    public Animator m_ObjectAnimator;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_ObjectAnimator.enabled = true;
    }
}
