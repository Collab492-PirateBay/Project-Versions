/* JAR MOVEMENT */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JarOrbit : MonoBehaviour
{
    //make the shake speed equal to the force of the accilometer and have a collision trigger on the top and bottom of jar

    [SerializeField] public bool m_CanSwing = false;
    public PlayerMovement m_Player;

    //.....................................................
    //SWINGING JAR
    [SerializeField] private float m_SwingRate = 0.0f;
    [SerializeField] private float m_RetractionRate = 0.0f;

    //[SerializeField] private bool m_CanSwing = true;
    [SerializeField] private bool m_IsSwinging = false;
    [SerializeField] private bool m_IsRetracting = false;
    [SerializeField] private bool m_IsStopped = true;

    [SerializeField] private float m_ScreenBoundaryLeft = 0.0f;
    [SerializeField] private float m_ScreenBoundaryRight = 0.0f;

    //don't think we need this... safetydist
    [SerializeField] private float m_CanSwingSafetyDist = 0.0f;

    //don't think we need this... swingpos
    [SerializeField] private float m_swingpos = 0.0f;

    //.....................................................
    //CAPTURING FAIRIES
    private int m_CatchRatioMax = 6;
    private int m_CatchRatioMin;
    private int m_CatchRatioPicker;

    //how much dust that you can collect per shake (determined by the fairy type/color)
    private int m_DustFallMin;

    //add this modifier to the Dust Fall Minimum to create the max dust that can be awarded
    [SerializeField] private int m_DustFallRangeModifier = 0;

    private float m_SwingCooldownTimer;
    private float m_SwingCooldownDur = 0.0f;

    public Text m_SwingResultsText;
    private float m_ResultsTextTimer;
    [SerializeField] float m_ResultsTextDur = 0.0f;

    private NetworkServerUI networkInput;
    private Vector3 accelorometerInput;

    //.....................................................
    //SHAKING FAIRIES
    [SerializeField] public bool m_HasCaughtAFairy = false;
    [SerializeField] private bool m_CanShake = false;

    public Text m_HintToShakeText;
    private float m_HintToShakeTimer;
    [SerializeField] private float m_HintToShakeDur;

    [SerializeField] private float m_ShakeSpeed = 0.0f;
    [SerializeField] private float m_ShakeMaxSpeed = 0.0f;


    [SerializeField] private bool m_IsMovingUp = false;
    [SerializeField] private bool m_IsMovingDown = false;

    [SerializeField] private bool m_CanMoveUp = false;
    [SerializeField] private bool m_CanMoveDown = false;

    [SerializeField] private bool m_IsInTopZone;
    [SerializeField] private bool m_IsInBottom;
    [SerializeField] private bool m_IsInMiddleZone;

    [SerializeField] private float m_ScreenBoundaryTop = 0.0f;
    [SerializeField] private float m_ScreenBoundaryBottom = 0.0f;



    void Start()
    {
        GameObject m_PlayerObject = GameObject.FindGameObjectWithTag("Player");
        m_Player = m_PlayerObject.GetComponent<PlayerMovement>();
        networkInput =  GameManager.GameManagerInstance.gameObject.GetComponent<NetworkServerUI>();
    }

    void Update()
    {
        accelorometerInput = new Vector3(networkInput.accelX, networkInput.accelY, networkInput.accelZ);
        if (m_Player.m_CanMove == true)
        {
            if (m_HasCaughtAFairy == false)
            {
                m_SwingCooldownTimer -= Time.deltaTime;
                if (m_SwingCooldownTimer <= 0)
                {
                    m_SwingCooldownTimer = 0;
                }

                //SWING VARIABLES
                float aSwingAmount;
                aSwingAmount = m_SwingRate * Time.deltaTime;

                Vector3 aSwingVector;
                aSwingVector = aSwingAmount * Vector3.up;


                //RETRACTION / SWING BACK VARIABLES
                float aRetractionAmount;
                aRetractionAmount = m_RetractionRate * Time.deltaTime;

                Vector3 aRetractionVector;
                aRetractionVector = aRetractionAmount * Vector3.up;

                m_swingpos = transform.localRotation.y;


                //SWING JAR INPUT
                if (m_SwingCooldownTimer <= 0)
                {
                    //if (Input.GetKey(KeyCode.Space))
                    if(accelorometerInput.sqrMagnitude > 5)
                    {
                        m_IsSwinging = true;
                        m_IsStopped = false;
                    }
                    else
                    {
                        m_IsSwinging = false;
                    }

                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        m_IsRetracting = true;
                        m_IsSwinging = false;
                        m_SwingCooldownTimer = m_SwingCooldownDur;
                    }
                }


                //ATTEMPT TO STOP AT LEFT SCREEN
                if (transform.localRotation.y <= m_ScreenBoundaryLeft)
                {
                    //m_CanSwing = false;
                    m_IsRetracting = true;
                    m_IsSwinging = false;
                }


                //SWING JAR OUTPUT
                if (m_IsSwinging == true)
                {
                    //jar orbits / swings to the left
                    transform.Rotate(-aSwingVector);
                }

                //RETRACTING JAR OUTPUT
                if (m_IsSwinging == false && m_IsRetracting == true)
                {
                    //jar orbits back / retracts to the right
                    transform.Rotate(aRetractionVector);

                    //condition for stopping at original position
                    if (transform.localRotation.y >= m_ScreenBoundaryRight)
                    {
                        m_IsStopped = true;
                        m_IsRetracting = false;
                    }

                    //ABLE TO SWING SAFETY DISTANCE
                    //want the player to be able to swing again without having to wait for the jar to go all the way back.
                    if (transform.localRotation.y <= m_CanSwingSafetyDist)
                    {
                        //m_CanSwing = true;
                    }
                }


                //Resetting initial positioning when back to idle position for reinsurance
                if (m_IsStopped == true)
                {
                    transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }
            }

            //...............................................................
            //............................................... * TEXT TIMERS *

            //COME BACK TO THE HINTS, may need to create a boolean for each for when the text have been activated
            //HINTS TEXT
            //m_HintToShakeTimer -= Time.deltaTime;

            //if (m_HintToShakeTimer <= 0)
            //{
            //    m_HintToShakeTimer = 0;
            //    m_HintToShakeText.text = "";
            //}

            //if (m_HintToShakeTimer > 0)
            //{
            //    if (m_ResultsTextTimer <= 0)
            //    {
            //        if (m_HasCaughtAFairy == true)
            //        {
            //            m_HintToShakeText.text = "Shake'M Up Good!";
            //        }
            //        else if (m_HasCaughtAFairy == false)
            //        {
            //            m_HintToShakeText.text = "Swing 'N Catch Me A Fairy!";
            //        }
            //    }
            //}

            m_ResultsTextTimer -= Time.deltaTime;

            if (m_ResultsTextTimer <= 0)
            {
                m_ResultsTextTimer = 0;
                m_SwingResultsText.text = "";

                //if (m_HintToShakeTimer <= 0)
                //{
                //    m_HintToShakeTimer = m_HintToShakeDur;
                //}
            }

            //...............................................................
            //................................................... * SHAKING *

            if (m_HasCaughtAFairy == true)
            {
                //SWING VARIABLES
                float aShakeAmount;
                aShakeAmount = m_ShakeSpeed * Time.deltaTime;

                Vector3 aShakeVector;
                aShakeVector = aShakeAmount * Vector3.right;

                //m_shakepos = transform.localRotation.y;


                //SHAKE INPUT
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    if (m_CanMoveUp == true)
                    {
                        m_IsMovingUp = true;
                        m_IsMovingDown = false;
                    }
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    if (m_CanMoveDown == true)
                    {
                        m_IsMovingUp = false;
                        m_IsMovingDown = true;
                    }
                }
                else
                {
                    m_IsMovingUp = false;
                    m_IsMovingDown = false;
                }

                //SHAKE RESTRAINTS

                ////ATTEMPT TO STOP AT TOP OF SCREEN
                //if (transform.localRotation.x <= m_ScreenBoundaryTop)
                //{
                //    m_CanMoveUp = false;
                //    transform.localRotation = transform.localRotation;
                //}
                //else if (transform.localRotation.x > m_ScreenBoundaryTop)
                //{
                //    m_CanMoveUp = true;
                //}

                ////ATTEMPT TO STOP AT BOTTOM OF SCREEN
                //if (transform.localRotation.x >= m_ScreenBoundaryBottom)
                //{
                //    m_CanMoveDown = false;
                //    transform.localRotation = transform.localRotation;
                //}
                //else if (transform.localRotation.x < m_ScreenBoundaryBottom)
                //{
                //    m_CanMoveDown = true;
                //}


                //SWING JAR OUTPUT
                if (m_IsMovingUp == true)
                {
                    print(aShakeVector + "up");
                    //jar orbits / shakes up
                    transform.Rotate(-aShakeVector);
                }

                if (m_IsMovingDown == true)
                {
                    print(aShakeVector + "down");
                    //jar orbits / shakes down
                    transform.Rotate(aShakeVector);
                }
            }
        }
    }

    //...............................................................
    //.................................................. * TRIGGERS *

    protected void OnTriggerEnter(Collider aTrigger)
    {
        //CATCH RATIO
        //when fairy has been hit by the jar while it's swinging, there's a random chance of catching it...

        GameObject aTriggerObject;
        aTriggerObject = aTrigger.gameObject;

        FairyMovement aFairy;
        aFairy = aTriggerObject.GetComponent<FairyMovement>();

        if (aFairy != null)
        {
            if (m_IsSwinging == true)
            {
                //start the randomizer, the minimun is determined by the fairy type and it's bonus (granted by failed attempts)
                m_CatchRatioPicker = Random.Range(aFairy.m_CatchRateMin, m_CatchRatioMax);

                if (m_CatchRatioPicker <= 4)
                {
                    aFairy.m_CatchRateAttemptBonus += 1;
                    m_IsRetracting = true;
                    m_SwingCooldownTimer = m_SwingCooldownDur;
                    m_ResultsTextTimer = m_ResultsTextDur;
                    m_SwingResultsText.text = "MISS!";
                    aFairy.m_IsScared = true;
                }
                else if (m_CatchRatioPicker == 5)
                {
                    m_HasCaughtAFairy = true;
                    m_Player.m_FairiesObtained += 1;
                    m_CanMoveUp = true;
                    m_CanMoveDown = true;

                    //DETERMINING FAIRY'S WORTH/VALUE IN DUST PER SHAKE
                    if (aFairy.m_FairyIsRed)
                    {
                        m_DustFallMin = 250;
                        m_ResultsTextTimer = m_ResultsTextDur;
                        m_SwingResultsText.text = "DON'T BE LETT'N DIS ONE GO TO WASTE!";
                    }
                    else if (aFairy.m_FairyIsGreen)
                    {
                        m_DustFallMin = 200;
                        m_ResultsTextTimer = m_ResultsTextDur;
                        m_SwingResultsText.text = "THOSE ONES'R RARE!";
                    }
                    else if (aFairy.m_FairyIsBlue)
                    {
                        m_DustFallMin = 150;
                        m_ResultsTextTimer = m_ResultsTextDur;
                        m_SwingResultsText.text = "THAT BE AN INTEREST'N FAIRY!";
                    }
                    else if (aFairy.m_FairyIsYellow)
                    {
                        m_DustFallMin = 100;
                        m_ResultsTextTimer = m_ResultsTextDur;
                        m_SwingResultsText.text = "YOU GOT A PLAIN'OL FAIRY!";
                    }

                    aFairy.gameObject.SetActive(false);
                    m_SwingCooldownTimer = m_SwingCooldownDur;
                } 
            }

        }
    }
}
