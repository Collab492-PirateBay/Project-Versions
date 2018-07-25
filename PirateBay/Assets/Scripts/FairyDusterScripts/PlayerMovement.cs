/* PLAYER MOVEMENT */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //_______________________________________________________________________
    //__________________________________________________________ * SETTINGS *

    [SerializeField] public bool m_CanMove = false;

    //...............................................................
    //GAME TYPES

    //TRIAL GAME
    //there is a time limit
    [SerializeField] private bool m_IsTrialGame;
    private float m_GameTimer;
    private float m_GameTimerDur = 60.0f;
    public Text m_GameTimerText;

    //REQUIREMENT GAME
    //if the game requires number of fairies needed to be caught
    [SerializeField] private bool m_IsRequirementGame;
    [SerializeField] private int m_FairiesNeededToWin = 3;
    [SerializeField] public int m_FairiesObtained = 0;

    //FREE ROAM GAME
    //make an exit button
    [SerializeField] private bool m_IsFreeRoamGame;

    //...............................................................
    //SPEED & DIRECTION
    private Rigidbody m_RigidBody = null;
    public Vector3 m_Dir = Vector3.zero;
    [SerializeField] private float m_Speed = 6.5f;

    //...............................................................
    //LOOKING AROUND W/MOUSE
    [SerializeField] private float m_TurnSpeed = 27.0f;
    [SerializeField] private float m_TiltSpeed = -27.0f;


    //...............................................................
    //TIMERS

    //COUNTDOWN FOR GAME START
    private float m_CountdownTimer;
    private float m_CountdownDur = 3.0f;
    public Text m_CountdownText;
    public bool m_GameHasStarted;


    public bool m_GameHasEnded;

    public UIManager uiManager;
    private GameManager Game_manager;
    //............................................................
    //.................................................. * START *

    protected void Start()
    {
        //...............................................................
        //ESTABLISHING RIGIDBODY
        m_RigidBody = GetComponent<Rigidbody>();

        //...............................................................
        //MOUSE STAYS WITHIN GAME SCREEN
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        //...............................................................
        //LASERSIGHT / RAYCASTING
        //m_LineRenderer = GetComponent<LineRenderer>();

        //TIMERS
        m_GameTimer = m_GameTimerDur;
        m_CountdownTimer = m_CountdownDur;

        m_GameHasStarted = false;
        m_GameHasEnded = false;

        GameObject uiManagerObject = GameObject.FindGameObjectWithTag("uiManager");
        uiManager = uiManagerObject.GetComponent<UIManager>();

        Game_manager = GameManager.GameManagerInstance;
    }

    //............................................................
    //................................................. * UPDATE *

    protected void Update()
    {
        
        //...............................................................
        //TIMERS & GAME START / END
        //GAME TIMER
        if (m_GameHasStarted == true)
        {
            //m_GameTimerText = GameObject.Find("TimerText").GetComponent<Text>();

            if (m_IsTrialGame == true)
            {
                m_GameTimer -= Time.deltaTime;
            }
        }

        //END GAME CONDITION FOR TRIAL GAME
        if (m_GameTimer <= 0)
        {
            m_GameTimer = 0;
            if (m_IsTrialGame == true)
            {
                m_GameHasEnded = true;

                m_CountdownTimer = m_CountdownDur;
                m_CountdownText.text = "END!";

                StartCoroutine(EndTimer());

            }
        }

        //WIN CONDITION FOR REQUIREMENT GAME
        if (m_IsRequirementGame == true)
        {
            if (m_FairiesObtained == m_FairiesNeededToWin)
            {
                m_GameHasEnded = true;

                m_CountdownTimer = m_CountdownDur;
                m_CountdownText.text = "END!";

                StartCoroutine(EndTimer());
            }
        }

        //END GAME CONDITION FOR FREE ROAM
        //enter script here

        //SCORE / TIMER DISPLAY, affected by the type of game
        if (m_IsTrialGame == true)
        {
            m_GameTimerText.text = (m_GameTimer / 60).ToString("00") + ":" + (m_GameTimer % 60).ToString("00");
        }
        else if (m_IsRequirementGame == true)
        {
            m_GameTimerText.text = (m_FairiesObtained + " / " + m_FairiesNeededToWin);
        }
        else if (m_IsFreeRoamGame == true)
        {
            m_GameTimerText.text = "" + m_FairiesObtained;
        }

        //................................................................................
        //.............................................. * COUNTDOWN TIMER to begin game *
        if (uiManager.hasGameStarted == true)
        {
            
            m_CountdownTimer -= Time.deltaTime;

            if (m_CountdownTimer <= 0)
            {
                m_GameHasStarted = true;
                m_CanMove = true;
            }
        }

        if (m_GameHasEnded == false)
        {
            if ((m_CountdownTimer >= 1))
            {
                m_CountdownText.text = (m_CountdownTimer % 60).ToString("00");
            }
            else
                if ((m_CountdownTimer < 1) && (m_CountdownTimer >= 0))
            {
                m_CountdownText.text = "GO!";
            }
            else
                if (m_CountdownTimer < 0)
            {
                m_CountdownText.text = "";
            }
        }

        //DON'T LET THE PLAYER DO ANYTHING UNTIL THE COUNTDOWN TIMER IS UP
        if (m_CanMove == true)
        {
            m_GameTimerText = GameObject.Find("GameTimerText").GetComponent<Text>();

#if UNITY_EDITOR
            /*
            //...............................................................
            //MOVEMENT CONTROLS
            Vector3 m_Velocity;
            m_Velocity = Vector3.zero;
            m_Dir = Vector3.zero;
            //SAFETY LOCKS (rotation)
            m_Velocity.x = 0.0f;
            m_Velocity.y = m_RigidBody.velocity.y;

            //...............................................................
            //STRAFE LEFT
            if (Input.GetKey(KeyCode.A))
            {
                m_Velocity += m_Speed * transform.right * -1;
            }
            //STRAFE RIGHT
            if (Input.GetKey(KeyCode.D))
            {
                m_Velocity += m_Speed * transform.right;
            }
            //MOVE FORWARD
            if (Input.GetKey(KeyCode.W))
            {
                m_Velocity += m_Speed * transform.forward;
            }
            //MOVE BACKWARD
            if (Input.GetKey(KeyCode.S))
            {
                m_Velocity += m_Speed * transform.forward * -1;
            }

            //KEEPING VELOCITY IN CHECK
            if (m_Velocity != m_RigidBody.velocity)
            {
                m_RigidBody.velocity = m_Velocity;
            }


            //...............................................................
            //ROTATE/TURN PLAYER

            //ROTATE LEFT
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0.0f, -3.0f, 0.0f);
            }
            //ROTATE RIGHT
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0.0f, 3.0f, 0.0f);
            }

            //...............................................................
            //LOOKING AROUND with MOUSE
            float m_MouseTurn;
            m_MouseTurn = Input.GetAxis("Mouse X");

            float m_TurnAngle;
            m_TurnAngle = m_TurnSpeed * m_MouseTurn * Time.deltaTime;

            transform.Rotate(0.0f, m_TurnAngle, 0.0f);

            //TILTING camera with MOUSE
            float m_MouseTilt;
            m_MouseTilt = Input.GetAxis("Mouse Y");

            float m_TiltAngle;
            m_TiltAngle = m_TiltSpeed * m_MouseTilt * Time.deltaTime;
            if (m_TiltAngle != 0.0f)
            {
                Camera.main.transform.Rotate(m_TiltAngle, 0.0f, 0.0f);
            }


            m_RigidBody.velocity = m_Velocity;
            */
#endif

        }

     

    }

    IEnumerator EndTimer()
    {
        yield return new WaitForSeconds(5.0f);
        Game_manager.setCurrentScene("AB_Lobby");
    }

}