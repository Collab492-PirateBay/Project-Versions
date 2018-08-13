

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkDropIntroCamera : MonoBehaviour 
{
    // CAMERA POSITIONS FOR START SCREEN
    private bool m_StartScreenIsActive = true;
    [SerializeField] private float m_CameraSpeed = 0.0f;
    private Vector3 m_CameraFocus;

    // CAMERA POSITIONS FOR WHEN GAMEPLAY BEGINS
    public bool m_GameplayHasStarted = false;
    private Vector3 m_CannonViewLocation;


    void Start () 
    {
        m_CannonViewLocation = GameObject.Find("Cannon").transform.position;
	}
	
	void Update () 
    {
        m_CameraFocus = GameObject.Find("Cannon").transform.position;

        if (m_StartScreenIsActive == true)
        {

            //this has the camera focused and revoling around an object
            this.transform.LookAt(m_CameraFocus);
            this.transform.Translate(Vector3.right * m_CameraSpeed * Time.deltaTime);

            //this makes it smoother but keeping one direction while rotating around object
            Camera.main.transform.rotation = Quaternion.Euler(17.0f, 86.0f, 0.0f);

        }

        if (m_GameplayHasStarted == true)
        {
            MoveCameraToCannon();
        }

	}

    IEnumerator MoveCameraToCannon()
    {
        transform.position = Vector3.Lerp(this.transform.position, m_CannonViewLocation, 0.0f);

        yield return new WaitForSeconds(1.5f);

        yield return null;
    }
}
