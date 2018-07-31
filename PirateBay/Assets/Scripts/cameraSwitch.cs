using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class cameraSwitch : MonoBehaviour
{
    //Camera to use
    public GameObject mainCamera;
    //Main Menu Buttons
    public GameObject AdventureButton;
   // public GameObject options_Button;
    //public GameObject exitGame_Button;
    public GameObject TitleObject;
    //Lobby Buttons
    public GameObject rightButton;
    public GameObject midButton;
    public GameObject leftButton;
    public GameObject backButton;
    public GameObject canonGame_Button;
    public GameObject fairyDusterGame_Button;

    public float camSpeed = 0.01f;
    
    private Vector3 lookatPoint;
    private Vector3 originalPoint;
    private Vector3 midPoint;
    private Vector3 rightPoint;
    private Vector3 leftPoint;
    private Vector3 currentPoint;
    private Vector3 GameslookAtPoint;

    public static bool mainMenuFinished;
    public static bool atStartPosition;

    private bool canMoveBack;
    private bool midSelected;
    private bool leftSelected;
    private bool rightSelected;
    private bool CamCanLookAt;
    private bool MenuisActive;
    private bool SetLobbyButtonsActive;

    void Start()
    {
        canMoveBack = false;
        midSelected = false;
        leftSelected = false;
        rightSelected = false;
        mainMenuFinished = false;
        MenuisActive = true;
        CamCanLookAt = false;
    }

    void Update()
    {
        lookatPoint = GameObject.Find("SM_Prop_Fountain_01").transform.position;
        GameslookAtPoint = GameObject.Find("SM_Prop_Crate_03").transform.position;

        originalPoint = GameObject.Find("CamOriginalPOS").transform.position;
        rightPoint = GameObject.Find("CityCam").transform.position;
        midPoint = GameObject.Find("CathedralCam").transform.position;
        leftPoint = GameObject.Find("VillageCam").transform.position;

        //After main menu is closed this moves camera to start position.
        if (mainMenuFinished == true)
        {
            StartCoroutine(MoveCamToStart());
        }
        //Left Option Selected
        if (leftSelected == true)
        {
            StartCoroutine(MovetoLeftCam());
        }
        //Mid(Cathedral) Option Selected
        if(midSelected == true)
        {
            StartCoroutine(MoveToMid());
        }
        //Right (Liz City) Option Selected
        if(rightSelected == true)
        {
            StartCoroutine(MoveToRight());
        }
        //After pressing back, moves the camera to start position
        if (canMoveBack == true)
        {
            StartCoroutine(moveCamBack());
        }
        //When the Main menu is active this rotates the camera around the island.
        if (MenuisActive == true)
        {
            this.transform.LookAt(lookatPoint);
            this.transform.Translate(Vector3.right * camSpeed * Time.deltaTime);
        }
        if (CamCanLookAt == true)
        {
            Camera.main.transform.rotation = Quaternion.Euler(17.0f, 86.0f, 0.0f);
            StartCoroutine(camLookAtStart());
        }

    }

    //Move the camera to start position after Main Menu has finished
    IEnumerator MoveCamToStart()
    {
        backButton.SetActive(false);
        transform.position = Vector3.Lerp(this.transform.position, originalPoint, 0.02f);
        

        yield return new WaitForSeconds(2.8f);
        
        atStartPosition = true;
        yield return null;
    }


    IEnumerator MovetoLeftCam()
    {
        fairyDusterGame_Button.SetActive(true);
        canonGame_Button.SetActive(true);
        backButton.SetActive(true);
        //look at the Games area
        this.transform.LookAt(GameslookAtPoint);
        backButton.SetActive(true);
        transform.position = Vector3.Lerp(this.transform.position, leftPoint, 0.02f);
        yield return null;
    }

    IEnumerator MoveToMid()
    {
        backButton.SetActive(true);
        transform.position = Vector3.Lerp(this.transform.position, midPoint, 0.02f);

        yield return null;
    }

    IEnumerator MoveToRight()
    {
        Camera.main.transform.rotation = Quaternion.Euler(0.0f, 116.454f, 0.0f);
        backButton.SetActive(true);
        transform.position = Vector3.Lerp(this.transform.position, rightPoint, 0.02f);
        yield return null;
    }

    IEnumerator moveCamBack()
    {
        backButton.SetActive(false);
        fairyDusterGame_Button.SetActive(false);
        canonGame_Button.SetActive(false);
        CamCanLookAt = true;
        transform.position = Vector3.Lerp(this.transform.position, originalPoint, 0.02f);

        yield return new WaitForSeconds(1.5f);
        canMoveBack = false;
        yield return null;
    }

    IEnumerator camLookAtStart()
    {
        //Camera.main.transform.rotation = Quaternion.Euler(17.0f, 86.0f, 0.0f);

        yield return new WaitForSeconds(1.5f);
        CamCanLookAt = false;
        yield return null;
    }


    /*
    IEnumerator OptionsNA()
    {
        options_Button.GetComponentsInChildren<Text>()[0].text = "Not Available Yet";
        yield return new WaitForSeconds(1.0f);
        options_Button.GetComponentsInChildren<Text>()[0].text = "Options";
        yield return null;
    }
    */

    /************************************
    //Main Menu Stuff
    ************************************/
    public void OnAdventureClicked()
    {
        CamCanLookAt = true;
        mainMenuFinished = true;
        MenuisActive = false;
        AdventureButton.SetActive(false);
       // options_Button.SetActive(false);
        //exitGame_Button.SetActive(false);
        TitleObject.SetActive(false);

    }

    public void OnOptionsClicked()
    {
     //   StartCoroutine(OptionsNA());
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
    public void OnBackClicked()
    {
        rightSelected = false;
        midSelected = false;
        leftSelected = false;
        canMoveBack = true;
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        midButton.SetActive(true);
        backButton.SetActive(false);
    }

    /*****************************
    //Lobby Options
    *****************************/
    public void OnRightClicked()
    {
        rightSelected = true;

    }



    public void OnMiddleClicked()
    {
        rightSelected = false;
        midSelected = true;
        leftSelected = false;
        canMoveBack = false;
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        midButton.SetActive(false);
        backButton.SetActive(true);

    }
    public void OnLeftClicked()
    {
        rightSelected = false;
        midSelected = false;
        leftSelected = true;
        canMoveBack = false;
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        midButton.SetActive(false);
        backButton.SetActive(true);

    }

}


