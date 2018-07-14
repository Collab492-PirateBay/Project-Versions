using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class cameraSwitch : MonoBehaviour
{
    //public new Camera camera;
    //public new Camera camera2;

    public GameObject rightButton;
    public GameObject midButton;
    public GameObject leftButton;
    public GameObject backButton;

    public float camSpeed = 3.0f;
    
    private Vector3 lookatPoint;
    private Vector3 originalPoint;
    private Vector3 midPoint;
    private Vector3 rightPoint;
    private Vector3 leftPoint;

    private bool mainMenuFinished;
    private bool canMoveBack;
    private bool midSelected;
    private bool leftSelected;
    private bool rightSelected;
    private bool CamCanLookAt;
    private bool MenuisActive;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.lockState = CursorLockMode.None;
        //Not working, Asking in class for help
        CamCanLookAt = true;

        canMoveBack = false;
        midSelected = false;
        leftSelected = false;
        rightSelected = false;
        mainMenuFinished = false;
        MenuisActive = true;

        lookatPoint = GameObject.Find("LookAt_Point").transform.position;
        originalPoint = GameObject.Find("CamOriginalPOS").transform.position;
        rightPoint = GameObject.Find("RightCam_POS").transform.position;
        midPoint = GameObject.Find("MidCam_POS").transform.position;
        leftPoint = GameObject.Find("LeftCam_POS").transform.position;
              


    }

    void Update()
    {

        if (mainMenuFinished == true)
        {
            //Set Up Coroutine for moving camera to originalPOS
            

        }

        if (leftSelected == true)
        {

            StartCoroutine(MovetoLeftCam());

        }
        if(midSelected == true)
        {
            StartCoroutine(MoveToMid());
        }
        if(rightSelected == true)
        {
            StartCoroutine(MoveToRight());
        }
        if (canMoveBack == true)
        {
            StartCoroutine(moveCamBack());
        }
        if (MenuisActive == true)
        {
            this.transform.LookAt(lookatPoint);
            this.transform.Translate(Vector3.right * camSpeed * Time.deltaTime);
        }

    }

    IEnumerator MoveCamToStart()
    {
        transform.position = Vector3.Lerp(this.transform.position, originalPoint, 0.02f);
        yield return new WaitForSeconds(.8f);
        backButton.SetActive(true);
        leftSelected = false;
        yield return null;
    }



    IEnumerator MovetoLeftCam()
    {
        while (leftSelected == true)
        {
            transform.position = Vector3.Lerp(this.transform.position, leftPoint, 0.02f);
            yield return new WaitForSeconds(.8f);
            backButton.SetActive(true);
            leftSelected = false;
            yield return null;
        }
    }

    IEnumerator MoveToMid()
    {
        while (midSelected == true)
        {
            transform.position = Vector3.Lerp(this.transform.position, midPoint, 0.02f);
            //transform.LookAt(lookatPoint);
            yield return new WaitForSeconds(.8f);
            backButton.SetActive(true);
            midSelected = false;
            yield return null;
        }
    }

    IEnumerator MoveToRight()
    {
        while (rightSelected == true)
        {
            transform.position = Vector3.Lerp(this.transform.position, rightPoint, 0.02f);
            midButton.SetActive(false);
            //transform.LookAt(lookatPoint);
            yield return new WaitForSeconds(.8f);
            backButton.SetActive(true);
            rightSelected = false;
            yield return null;
        }
    }

    IEnumerator moveCamBack()
    {
        while (canMoveBack == true)
        {
            transform.position = Vector3.Lerp(this.transform.position, originalPoint, 0.02f);
            //transform.LookAt(lookatPoint);
            yield return new WaitForSeconds(.8f);
            canMoveBack = false;
            yield return null;
        }
    }



//Move Camera Position
public void OnRightClicked()
    {
        rightSelected = true;
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        midButton.SetActive(false);
        backButton.SetActive(true);

    }

    public void OnBackClicked()
    {
        canMoveBack = true;
        leftButton.SetActive(true);
        midButton.SetActive(true);
        rightButton.SetActive(true);
        backButton.SetActive(false);


    }

    public void OnMiddleClicked()
    {
        midSelected = true;
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        midButton.SetActive(false);
        backButton.SetActive(true);

    }
    public void OnLeftClicked()
    {
        leftSelected = true;
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        midButton.SetActive(false);
        backButton.SetActive(true);

    }

}


