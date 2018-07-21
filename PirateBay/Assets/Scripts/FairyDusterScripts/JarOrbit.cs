/* JAR MOVEMENT */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JarOrbit : MonoBehaviour
{
    [SerializeField] public bool m_CanSwing = false;
    public PlayerMovement m_Player;

    //.....................................................
    //SWINGING JAR
    [SerializeField] private float m_SwingRate = 0.0f;
    [SerializeField] private float m_RetractionRate = 0.0f;

    [SerializeField] private bool m_IsSwinging = false;
    [SerializeField] private bool m_IsRetracting = false;
    [SerializeField] private bool m_IsStopped = true;

    [SerializeField] private float m_ScreenBoundaryLeft = 0.0f;
    [SerializeField] private float m_ScreenBoundaryRight = 0.0f;

    //.....................................................
    //CAPTURING FAIRIES
    private int m_CatchRatioMax = 6;
    private int m_CatchRatioMin;
    private int m_CatchRatioPicker;

    //REPLACE DUST FALL MIN WITH THE POINT MULTIPLIER
    private int m_DustFallMin;

    private float m_SwingCooldownTimer;
    [SerializeField] private float m_SwingCooldownDur = 0.0f;

    //Just indicates to player if they caught the fairy, how rare it is, and how many points they get
    [SerializeField] private Text m_NotifierText;
    [SerializeField] private float m_NotifierTimer;
    [SerializeField] float m_NotifierDur = 0.0f;

    //.....................................................
    //SHAKING FAIRIES
    [SerializeField] public bool m_HasCaughtAFairy = false;
    [SerializeField] private bool m_CanShake = false;


    [SerializeField] private float m_ShakeSpeed = 0.0f;

    [SerializeField] private bool m_IsMovingUp = false;
    [SerializeField] private bool m_IsMovingDown = false;

    [SerializeField] private bool m_CanMoveUp = false;
    [SerializeField] private bool m_CanMoveDown = false;

    public string fairyType;

    //SHAKE SETTINGS

    [SerializeField] private bool m_ShakingHasStarted = false;

    [SerializeField] private float m_ShakeTimer;
    [SerializeField] private float m_ShakeDur = 0.0f;

    [SerializeField] private float m_ShakeForceUp;
    [SerializeField] private float m_ShakeForceDown;

    //SCORING SYSTEM
    [SerializeField] private float m_PointsMultiplier = 0.0f;
    [SerializeField] private float m_PlusPoints = 0.0f;

    [SerializeField] private float m_PointsEarnedFromThisFairy = 0.0f;

    public float m_TotalScore = 0.0f;
    public Text m_TotalScoreText;
    public Text m_TotalScoreTextShadow;

    public Animator m_ShakeAnimator;
    [SerializeField] private float m_RhythmTimer = 0.0f;
    [SerializeField] private float m_RhythmDur = 0.0f;

    private NetworkServerUI netInput;
    private Vector3 accelerometerInput;

    //............................................................
    //.................................................. * START *

    void Start()
    {
        GameObject m_PlayerObject = GameObject.FindGameObjectWithTag("Player");
        m_Player = m_PlayerObject.GetComponent<PlayerMovement>();

        m_TotalScoreText = GameObject.Find("TotalScore").GetComponent<Text>();
        m_TotalScoreTextShadow = GameObject.Find("TotalScoreShadow").GetComponent<Text>();

        m_NotifierText = GameObject.Find("NotifierText").GetComponent<Text>();

        netInput = GameManager.GameManagerInstance.gameObject.GetComponent<NetworkServerUI>();
    }

    //............................................................
    //................................................. * UPDATE *

    void Update()
    {
        
        m_TotalScoreText.text = "" + m_TotalScore;
        m_TotalScoreTextShadow.text = m_TotalScoreText.text;

        accelerometerInput = new Vector3(netInput.accelX, netInput.accelY, netInput.accelZ);

        if (m_Player.m_CanMove == true)
        {
            
            m_NotifierTimer -= Time.deltaTime;

            if (m_NotifierTimer <= 0)
            {
                m_NotifierTimer = 0;
                m_NotifierText.text = "";
            }

            if (m_HasCaughtAFairy == false)
            {
                m_ShakeAnimator.enabled = false;

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


                //SWING JAR INPUT
                if (m_SwingCooldownTimer <= 0)
                {
                    if (runInEditMode)
                    {
                        if (Input.GetKey(KeyCode.Space))
                            if (accelerometerInput.sqrMagnitude > 5)
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
                    else
                    {
                        if (accelerometerInput.sqrMagnitude > 5)
                        {
                            m_IsSwinging = true;
                            m_IsStopped = false;
                        }
                        else
                        {
                            m_IsSwinging = false;
                        }

                        if (accelerometerInput.sqrMagnitude < 1)
                        {
                            m_IsRetracting = true;
                            m_IsSwinging = false;
                            m_SwingCooldownTimer = m_SwingCooldownDur;
                        }
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

                }


                //Resetting initial positioning when back to idle position for reinsurance
                if (m_IsStopped == true)
                {
                    transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }


            }

            //...............................................................
            //................................................... * SHAKING *

            if (m_HasCaughtAFairy == true)
            {
                m_ShakeAnimator.enabled = true;

                //SWING VARIABLES
                float aShakeAmount;
                aShakeAmount = m_ShakeSpeed * Time.deltaTime;

                Vector3 aShakeVector;
                aShakeVector = aShakeAmount * Vector3.right;




                //...............................................................
                //NEW SHAKE

                //SHAKE TIMER
                m_ShakeTimer -= Time.deltaTime;
                if (m_ShakeTimer <= 0)
                {
                    m_ShakeTimer = 0;
                }

                //DEATH OF FAIRY --> RESET SETTINGS TO CATCH AGAIN
                if (m_ShakingHasStarted == true)
                {
                    
                    if (m_ShakeTimer <= 0)
                    {
                        m_NotifierTimer = m_NotifierDur;
                        m_NotifierText.text = "" + m_PointsEarnedFromThisFairy;

                        if (m_HasCaughtAFairy == true)
                        {
                            m_CanSwing = true;
                            fairyType = "";
                            m_CanShake = false;
                            m_ShakingHasStarted = false;
                            m_PlusPoints = 0;
                            m_PointsEarnedFromThisFairy = 0;
                            m_IsStopped = true;
                            m_IsMovingUp = false;
                            m_IsMovingDown = false;

                            //IF WIN CONDITION IS BASED ON A CATCH REQUIREMENT
                            m_Player.m_FairiesObtained += 1;

                            m_HasCaughtAFairy = false;
                        }
                    }
                }

                //gives the player a very short grace period before reverting the jar back to default position
                m_RhythmTimer -= Time.deltaTime;
                if (m_RhythmTimer <= 0)
                {
                    m_RhythmTimer = 0;
                }

                //SHAKE UP
                if (m_IsMovingDown == false)
                {
                    if (runInEditMode)
                    {
                        if (accelerometerInput.sqrMagnitude > 5 && accelerometerInput.y > 0)
                        {
                            m_ShakeAnimator.SetBool("IsGoingUp", true);
                            m_ShakeAnimator.SetFloat("ShakeSpeed", m_ShakeSpeed);
                            m_IsMovingUp = true;
                            //m_ShakeForceUp = accelometer force y
                            m_PlusPoints = m_PointsMultiplier * m_ShakeForceUp;
                            m_PointsEarnedFromThisFairy += m_PlusPoints;
                            m_TotalScore += m_PlusPoints;

                            if (m_ShakingHasStarted == false)
                            {
                                m_ShakingHasStarted = true;
                                m_ShakeTimer = m_ShakeDur;
                            }
                            //might need some code to recognize a stop for the accelometer because of the first if statement about moving down?
                        }
                        else if (Input.GetKeyUp(KeyCode.UpArrow))
                        {
                            m_IsMovingUp = false;
                            m_ShakeAnimator.SetBool("IsGoingUp", false);
                            m_RhythmTimer = m_RhythmDur;
                        }
                    }
                    else
                    {
                        //recognize accelerometer input & direction 
                        if (accelerometerInput.sqrMagnitude > 5 && accelerometerInput.y > 0)
                        {
                            m_ShakeAnimator.SetBool("IsGoingUp", true);
                            m_ShakeAnimator.SetFloat("ShakeSpeed", m_ShakeSpeed);
                            m_IsMovingUp = true;
                            //m_ShakeForceUp = accelometer force y
                            m_PlusPoints = m_PointsMultiplier * m_ShakeForceUp;
                            m_PointsEarnedFromThisFairy += m_PlusPoints;
                            m_TotalScore += m_PlusPoints;

                            if (m_ShakingHasStarted == false)
                            {
                                m_ShakingHasStarted = true;
                                m_ShakeTimer = m_ShakeDur;
                            }
                            //might need some code to recognize a stop for the accelometer because of the first if statement about moving down?
                        }
                        
                        else if (accelerometerInput.sqrMagnitude < 1)
                        {
                            m_IsMovingUp = false;
                            m_ShakeAnimator.SetBool("IsGoingUp", false);
                            m_RhythmTimer = m_RhythmDur;
                        }
                    }
                }

                //SHAKE DOWN
                if (m_IsMovingUp == false)
                {
                    if (runInEditMode)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow)) //if y axis <= m_DownShakeForceRequirement
                        {
                            m_ShakeAnimator.SetBool("IsGoingDown", true);
                            m_ShakeAnimator.SetFloat("ShakeSpeed", m_ShakeSpeed);
                            m_IsMovingDown = true;
                            //m_ShakeForceDown = accelometer force -y
                            m_PlusPoints = m_PointsMultiplier * m_ShakeForceDown;
                            m_PointsEarnedFromThisFairy += m_PlusPoints;
                            m_TotalScore += m_PlusPoints;

                            if (m_ShakingHasStarted == false)
                            {
                                m_ShakingHasStarted = true;
                                m_ShakeTimer = m_ShakeDur;
                            }
                        }
                        else if (Input.GetKeyUp(KeyCode.DownArrow))
                        {
                            m_IsMovingDown = false;
                            m_ShakeAnimator.SetBool("IsGoingDown", false);
                            m_RhythmTimer = m_RhythmDur;
                        }
                    }
                    else
                    {
                        //recognize accelerometer input & direction 
                        if (accelerometerInput.sqrMagnitude > 5 && accelerometerInput.y < 0)
                        {
                            m_ShakeAnimator.SetBool("IsGoingDown", true);
                            m_ShakeAnimator.SetFloat("ShakeSpeed", m_ShakeSpeed);
                            m_IsMovingDown = true;
                            //m_ShakeForceDown = accelometer force -y
                            m_PlusPoints = m_PointsMultiplier * m_ShakeForceDown;
                            m_PointsEarnedFromThisFairy += m_PlusPoints;
                            m_TotalScore += m_PlusPoints;

                            if (m_ShakingHasStarted == false)
                            {
                                m_ShakingHasStarted = true;
                                m_ShakeTimer = m_ShakeDur;
                            }
                        }
                        else if (accelerometerInput.sqrMagnitude < 1)
                        {
                            m_IsMovingDown = false;
                            m_ShakeAnimator.SetBool("IsGoingDown", false);
                            m_RhythmTimer = m_RhythmDur;
                        }
                    }
                }


                if ((m_IsMovingUp == false) && (m_IsMovingDown == false))
                {
                    if (m_RhythmTimer <= 0)
                    {
                        m_ShakeAnimator.SetBool("IsRevertingBackToDefault", true);
                    }
                }
                else
                {
                    m_ShakeAnimator.SetBool("IsRevertingBackToDefault", false);
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
                    m_NotifierTimer = m_NotifierDur;
                    m_NotifierText.text = "MISS!";
                    aFairy.m_IsScared = true;
                }
                else if (m_CatchRatioPicker == 5)
                {
                    m_HasCaughtAFairy = true;
                    m_Player.m_FairiesObtained += 1;
                    m_CanMoveUp = true;
                    m_CanMoveDown = true;
                    m_PointsEarnedFromThisFairy = 0;

                    //DETERMINING FAIRY'S WORTH/VALUE IN DUST PER SHAKE
                    if (aFairy.m_FairyIsRed)
                    {
                        fairyType = "Red";
                        m_DustFallMin = 250;
                        m_NotifierTimer = m_NotifierDur;
                        m_NotifierText.text = "SUPER RARE!";
                        transform.localRotation = Quaternion.Euler(0.0f, -25, 0.0f);
                    }
                    else if (aFairy.m_FairyIsGreen)
                    {
                        fairyType = "Green";
                        m_DustFallMin = 200;
                        m_NotifierTimer = m_NotifierDur;
                        m_NotifierText.text = "RARE!";
                        transform.localRotation = Quaternion.Euler(0.0f, -25, 0.0f);
                    }
                    else if (aFairy.m_FairyIsBlue)
                    {
                        fairyType = "Blue";
                        m_DustFallMin = 150;
                        m_NotifierTimer = m_NotifierDur;
                        m_NotifierText.text = "SPECIAL!";
                        transform.localRotation = Quaternion.Euler(0.0f, -25, 0.0f);
                    }
                    else if (aFairy.m_FairyIsYellow)
                    {
                        fairyType = "Yellow";
                        m_DustFallMin = 100;
                        m_NotifierTimer = m_NotifierDur;
                        m_NotifierText.text = "OKAY!";
                        transform.localRotation = Quaternion.Euler(0.0f, -25, 0.0f);
                    }

                    aFairy.gameObject.SetActive(false);
                    m_SwingCooldownTimer = m_SwingCooldownDur;
                } 
            }

        }
    }
}
