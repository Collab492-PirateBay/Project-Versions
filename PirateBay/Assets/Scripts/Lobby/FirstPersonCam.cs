using UnityEngine;
using System.Collections;

public class FirstPersonCam : MonoBehaviour
{
    public float turnSpeed = 20.0f;

    private Vector3 mouseOrigin;

    private float turnSpeedDefault = 20.0f;

    private bool isRotating;

    private Vector3 newRotation;

    
    void Update()
    {
       // mouseOrigin = Input.mousePosition;
       if (cameraSwitch.atStartPosition == true)
      {
            
            if (Input.GetMouseButton(0))
            {
                
                if (!isRotating)
                {
                    mouseOrigin = Input.mousePosition;
                    isRotating = true;
                }
                
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

                //transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
                transform.RotateAround(transform.position, Vector3.up, -pos.x * turnSpeed);
          }
        
            if(Input.GetMouseButtonUp(0))
            {
                
                isRotating = false;
               // newRotation = transform.eulerAngles.y;
            }
            
   
        }

    }

}
