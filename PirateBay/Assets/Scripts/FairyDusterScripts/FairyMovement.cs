/* FAIRY MOVEMENT */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyMovement : MonoBehaviour 
{
    //..............................................................
    //................................ MOVEMENT & DIRECTION SETTINGS

    //RIGIDBODY
    private Rigidbody m_RigidBody;

    //SPEED
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_MaxSpeed = 1.5f;
    [SerializeField] private float m_MinSpeed = 0.75f;
    [SerializeField] private float m_SpeedChangeTimer;
    [SerializeField] private float m_SpeedChangeDur;
    [SerializeField] private float m_SpeedChangeDurMin;
    [SerializeField] private float m_SpeedChangeDurMax;
    [SerializeField] public bool m_IsScared = false;


    //DIRECTION X & Z AXIS'
    private Vector3 m_Dir;
    private int m_CurrentDir;
    private bool m_MovingInLastDirection = true;
    //DIRECTION Y AXIS
    private int m_CurrentDirY;
    //TURNING PARAMETERS (DEGREES)
    [SerializeField] private float m_TurnDegreesMin = 0.0f;
    [SerializeField] private float m_TurnDegreesMax = 0.0f;
    [SerializeField] private float m_TurnDegrees;


    //BOUNDARY MOVE LIMITS
    [SerializeField] private float m_TopLimit = 6.95f;
    [SerializeField] private float m_BottomLimit = -6.95f;

    [SerializeField] private float m_RightLimit = 9.25f;
    [SerializeField] private float m_LeftLimit = -9.25f;

    [SerializeField] private float m_ForwardLimit = -6.95f;
    [SerializeField] private float m_BackLimit = -9.25f;


    //MOVE TIMER
    [SerializeField] private float m_MoveTimer = 0.0f;
    [SerializeField] private float m_MoveDur = 0.0f;
    //parameters for the max & min of the random time selected for each movement
    [SerializeField] private float m_MoveDurMax = 0.0f;
    [SerializeField] private float m_MoveDurMin = 0.0f;

    //..............................................................
    //................................. FAIRY TYPES & COLOR SETTINGS

    private int m_FairyTypePicker;
    public bool m_FairyIsRed;
    public bool m_FairyIsGreen;
    public bool m_FairyIsBlue;
    public bool m_FairyIsYellow;

    //MATERIAL/COLOR CHANGE
    private Renderer m_FairyRenderer = null;
    public Material[] m_FairyColorMaterial = null;


    //FAIRY TYPE
    //public int m_CatchRateMin;
    //should see the SF change below each time the timer is up and resets
    //[SerializeField] private int m_CatchRateBase;
    //should see below change each time you miss swinging at the fairy, should get +1 and stop at 3/stay 3
    //[SerializeField] public int m_CatchRateAttemptBonus = 0;
    //the bonus allows the player to have a better chance of catching the... 
    //fairy on future attempts by adding it to their base rate and producing...
    //the catchrate min that will be sent to the jar script.



    //private float m_FairyTypeChangeTimer;
    //[SerializeField] private float m_FairyTypeChangeDurationMin = 0.0f;
    //[SerializeField] private float m_FairyTypeChangeDurationMax = 0.0f;
    //can later change the duration to be affected by the color/rarity of the fairy maybe


    //make the fairy type determined more on rarity in the random picker
    //also make the duration of its type random and a better length of time than 5 sec


    //...............................................................
    //............................................ * METHOD : START *

    protected void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();

        m_FairyTypePicker = Random.Range(1, 5);

        m_FairyRenderer = GetComponent<Renderer>();
    }

    //...............................................................
    //........................................... * METHOD : UPDATE *

    protected void Update()
    {
        //..............................................................
        //FAIRY TYPE & CATCH RATE SETTIINGS

        //FAIRY TYPE TIMER
        //m_FairyTypeChangeTimer -= Time.deltaTime;
        //if (m_FairyTypeChangeTimer <= 0)
        //{
            //m_FairyTypeChangeTimer = 0;
            //m_FairyTypePicker = Random.Range(1, 5);
            //m_FairyTypeChangeTimer = Random.Range(m_FairyTypeChangeDurationMin, m_FairyTypeChangeDurationMax);
        //}

        //SETTING FAIRY'S TYPE
        if (m_FairyTypePicker == 1)
        {
            m_FairyIsRed = true;
            m_FairyIsGreen = false;
            m_FairyIsBlue = false;
            m_FairyIsYellow = false;
        }
        else if (m_FairyTypePicker == 2)
        {
            m_FairyIsRed = false;
            m_FairyIsGreen = true;
            m_FairyIsBlue = false;
            m_FairyIsYellow = false;
        }
        else if (m_FairyTypePicker == 3)
        {
            m_FairyIsRed = false;
            m_FairyIsGreen = false;
            m_FairyIsBlue = true;
            m_FairyIsYellow = false;
        }
        else if (m_FairyTypePicker == 4)
        {
            m_FairyIsRed = false;
            m_FairyIsGreen = false;
            m_FairyIsBlue = false;
            m_FairyIsYellow = true;
        }

        //FAIRY TYPE DETERMINES...
        if (m_FairyIsRed == true)
        {
            //m_CatchRateBase = 1;
            m_FairyRenderer.sharedMaterial = m_FairyColorMaterial[0];
        }
        if (m_FairyIsGreen == true)
        {
            //m_CatchRateBase = 2;
            m_FairyRenderer.sharedMaterial = m_FairyColorMaterial[1];
        }
        if (m_FairyIsBlue == true)
        {
            //m_CatchRateBase = 3;
            m_FairyRenderer.sharedMaterial = m_FairyColorMaterial[2];
        }
        if (m_FairyIsYellow == true)
        {
            //m_CatchRateBase = 4;
            m_FairyRenderer.sharedMaterial = m_FairyColorMaterial[3];
        }

        //CURRENT FAIRY'S TYPE & CATCH RATIO
        //the bonus is triggered in the jar's script, getting +1 each time it fails
        //m_CatchRateMin = m_CatchRateAttemptBonus + m_CatchRateBase;

        //CATCH RATE CAP
        //if (m_CatchRateMin >= 5)
        //{
            //m_CatchRateMin = 5;
        //}

            
        //..............................................................
        //MOVEMENT

        //ESTABLISHING VELOCITY
        Vector3 a_Velocity;
        a_Velocity = m_Dir * m_Speed;
        m_Dir = Vector3.zero;


        //SPEED UP TO MAX SPEED IF PLAYER HAS SWUNG AND MISSED
        //boolean controlled in Jar's script
        if (m_IsScared == true)
        {
            m_Speed = m_MaxSpeed;
            m_SpeedChangeTimer += Random.Range(m_SpeedChangeDurMin, m_SpeedChangeDurMax);
            m_IsScared = false;
        }

        //TIMER CHANGES SPEED
        m_SpeedChangeTimer -= Time.deltaTime;
        if (m_SpeedChangeTimer <= 0)
        {
            m_SpeedChangeTimer = 0;
            m_SpeedChangeDur = Random.Range(m_SpeedChangeDurMin, m_SpeedChangeDurMax);
            m_Speed = Random.Range(m_MinSpeed, m_MaxSpeed);
            m_SpeedChangeTimer = m_SpeedChangeDur;
        }

        //MOVEMENT COOLDOWN SPECS
        //makes the amount of time between movements vary each time...
        if (m_MovingInLastDirection == false)
        {
            m_MoveDur = Random.Range(m_MoveDurMin, m_MoveDurMax);
            m_TurnDegrees = Random.Range(m_TurnDegreesMin, m_TurnDegreesMax);
            m_MoveTimer = m_MoveDur;
            m_MovingInLastDirection = true;
        }

        //RANDOMLY SETTING THE NEW DIRECTION
        //establishing a real-time countdown...
        m_MoveTimer -= Time.deltaTime;

        //chooses a new direction to move when the move timer reaches 0, and then resets it...
        if (m_MoveTimer <= 0)
        {
            m_MovingInLastDirection = false;
            m_CurrentDir = Random.Range(1, 5);
            m_CurrentDirY = Random.Range(1, 3);
            m_MoveTimer = m_MoveDur;
        }


        //MOVING IN THE NEW DIRECTION
        //translates what the new direction is for the fairy...

        //MAY NEED TO MAKE IT ALWAYS GOING FORWARD AND JUST TURNING IT
        //MOVE FORWARD
        if (m_CurrentDir == 1)
        {
            m_Dir += Vector3.forward;
            transform.rotation = Quaternion.Euler(0, m_TurnDegrees, 0);
        }

        //MOVE RIGHT
        if (m_CurrentDir == 2)
        {
            m_Dir += Vector3.right;
            transform.rotation = Quaternion.Euler(0, m_TurnDegrees, 0);
        }

        //MOVE BACK
        if (m_CurrentDir == 3)
        {
            m_Dir += Vector3.back;
            transform.rotation = Quaternion.Euler(0, -m_TurnDegrees, 0);
        }

        //MOVE LEFT
        if (m_CurrentDir == 4)
        {
            m_Dir += Vector3.left;
            transform.rotation = Quaternion.Euler(0, -m_TurnDegrees, 0);
        }

        //MOVE UP
        if (m_CurrentDirY == 1)
        {
            m_Dir += Vector3.up;
        }

        //MOVE UP
        if (m_CurrentDirY == 2)
        {
            m_Dir += Vector3.down;
        }

        //..............................................................
        //MOVE RESTRICTIONS / BOUNDARIES
        //make sure that the fairies don't leave the arena, incase we end up...
        //using them in swarms and they individually wouldn't have box colliders...
/*
        //TOP LIMIT
        if (transform.position.y >= m_TopLimit)
        {
            m_MovingInLastDirection = false;
            m_CurrentDirY = 2;
            m_MoveTimer = 0;
        }
        //BOTTOM LIMIT
        if (transform.position.y <= m_BottomLimit)
        {
            m_MovingInLastDirection = false;
            m_CurrentDirY = 1;
            m_MoveTimer = 0;
        }
        //RIGHT LIMIT
        if (transform.position.x >= m_RightLimit)
        {
            m_MovingInLastDirection = false;
            m_CurrentDir = 4;
            m_MoveTimer = 0;
        }
        //LEFT LIMIT
        if (transform.position.x <= m_LeftLimit)
        {
            m_MovingInLastDirection = false;
            m_CurrentDir = 2;
            m_MoveTimer = 0;
        }
        //FRONT/FORWARD LIMIT
        if (transform.position.z >= m_ForwardLimit)
        {
            m_MovingInLastDirection = false;
            m_CurrentDir = 1;
            m_MoveTimer = 0;
        }
        //BACK/BEHIND LIMIT
        if (transform.position.z <= m_BackLimit)
        {
            m_MovingInLastDirection = false;
            m_CurrentDir = 1;
            m_MoveTimer = 0;
        }
*/
        //RIGIDBODY FOLLOWUP W/VELOCITY
        m_RigidBody.velocity = a_Velocity;

    }


    //...............................................................
    //................................................. * COLLSIONS *

    protected void OnCollisionEnter(Collision aFistBump)
    {
        //FIST BUMPING
        //when colliding with a fellow fairy, then change directions to avoid bug...
        if (aFistBump.gameObject.CompareTag("Fairy"))
        {
            m_MovingInLastDirection = false;
            m_CurrentDir = m_CurrentDir - 1;
            m_MoveTimer = 0;
            if (m_CurrentDir == 0)
            {
                m_CurrentDir = 4;
            }

            if (m_CurrentDirY == 1)
            {
                m_CurrentDirY = 2;
            }
            else
                if (m_CurrentDirY == 2)
            {
                m_CurrentDirY = 1;
            }
        }
    }

}
