/* CANNON MOVEMENT */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    //........................................................
    // COMMON VARIABLES

    // LOCK CONTROLS (GAMEPLAY)
    [SerializeField] public bool m_PlayerControlsAreActive = false;

    [SerializeField] private float m_TurningLimit = 0.15f;

    [SerializeField] private float m_TurningCapabilitySpeed = 0.0f;

    private float m_TurningSpeed = 0.0f;

    public Transform m_CannonBallSpawnPoint;
    public CannonBall m_CannonBallPrefab;

    [SerializeField] private Light fx_CannonLight;
    private float fx_Timer;
    [SerializeField] private float fx_TimerDur = 0.0f;

    public Transform m_CannonSmokeSpawnPoint;
    public GameObject m_CannonSmokePrefab;

    public bool m_CannonIsFiring = false;
    private float m_FiringCooldown;
    [SerializeField] private float m_FiringDuration = 0.0f;

    public bool m_NeedNewFuse = false;
    public bool m_ShowFuseBurning = false;
    public bool m_TargetIsOnTheLeft = false;

    // AUDIO SFX
    [SerializeField] private bool m_PlayCannonSounds = false;
    [SerializeField] private AudioSource cannonAudioSource = null;
    [SerializeField] private AudioClip sfx_CannonRotation = null;
    [SerializeField] private AudioClip sfx_CannonFire = null;
    [SerializeField] private AudioClip sfx_CannonReload = null;
    public float cannon_Volume;
     
    //........................................................
    // KEYBOARD VARIABLES (only use for playtesting)

    [SerializeField] private bool X_keyboardControl;

    //........................................................
    // ACCELEROMETER VARIABLES
    [SerializeField] private bool X_IsAccelerometerON = false;
    private NetworkServerUI netInput = null;
    
    //private Vector3 accelerometerInput;

    private float m_CannonRotationValue = 0.0f;

    private float accelSafetyPositiveAxis;
    private float accelSafetyNegativeAxis;

    [SerializeField] private float accelSafetyLeft = -0.6f;
    [SerializeField] private float accelSafetyRight = 0.2f;

    [SerializeField] private float accelSafetyUp = 0.0f;
    [SerializeField] private float accelSafetyDown = 0.0f;

    [SerializeField] private bool X_IsPhoneOrientationHorizontal = false;

    //........................................................
    // RELATIVE SCRIPTS

    public SceneManagement_SD m_SceneManager;
    public Spawner cannonRef_Spawner;

    private UIManager m_uiManager;

    //............................................................
    //.................................................. * START *
    void Start()
    {
        /*
        GameObject sceneManagerObject = GameObject.FindGameObjectWithTag("SceneManager");
        m_SceneManager = sceneManagerObject.GetComponent<SceneManagement_SD>();
        */

        GameObject spawnerObject = GameObject.FindGameObjectWithTag("Spawner");
        cannonRef_Spawner = spawnerObject.GetComponent<Spawner>();

        GameObject sfxCannonObject = GameObject.Find("sfx_Cannon");
        cannonAudioSource = sfxCannonObject.GetComponent<AudioSource>();

        GameObject lightObject = GameObject.Find("CannonFlash");
        fx_CannonLight = lightObject.GetComponent<Light>();

        m_uiManager = GameObject.FindGameObjectWithTag("uiManager").GetComponent<UIManager>();

        //Sets Network input
        if ( X_IsAccelerometerON == true )
        {
            netInput = GameManager.GameManagerInstance.gameObject.GetComponent<NetworkServerUI>();
        }
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {
        fx_Timer -= Time.deltaTime;
        if (fx_Timer <= 0)
        {
            fx_Timer = 0;
            m_NeedNewFuse = false;
            fx_CannonLight.gameObject.SetActive(false);
        }
        else if (fx_Timer > 0)
        {
            m_NeedNewFuse = true;
            fx_CannonLight.gameObject.SetActive(true);
        }
/*
        // CONTROLLING PLAYER INPUT
        if (m_SceneManager.m_GameplayActive == true)
        {
            m_PlayerControlsAreActive = true;
        }
        else
        {
            m_PlayerControlsAreActive = false;
        }

*/
        if (m_PlayerControlsAreActive == true)
        {
            //....................................................
            // ACCELEROMETER INPUTS

            if (!X_keyboardControl)
            {
                //Gets Accelerometer input from networked client, can use to get accelerometer readings for playtesting
                //accelerometerInput = new Vector3(netInput.accelX, netInput.accelY, netInput.accelZ);

                if (X_IsPhoneOrientationHorizontal)
                {
                    m_CannonRotationValue = netInput.accelY;
                    accelSafetyPositiveAxis = accelSafetyUp;
                    accelSafetyNegativeAxis = accelSafetyDown;

                    // *************************************************
                    // NEED TO FIND OUT THE SAFETY CUSHIONS for the horizontal mode ^^
                    // *************************************************
                }
                else
                {
                    m_CannonRotationValue = netInput.accelX;
                    accelSafetyPositiveAxis = accelSafetyRight;
                    accelSafetyNegativeAxis = accelSafetyLeft;
                }

                m_CannonRotationValue = netInput.accelX;
                //print(m_CannonRotationValue);
            }

            //....................................................
            // COMMON VARIABLES

            float aTurnAmount;
            aTurnAmount = m_TurningSpeed * Time.deltaTime;

            Vector3 aTurnVector;
            aTurnVector = aTurnAmount * Vector3.up;

            m_FiringCooldown -= Time.deltaTime;

            if (m_FiringCooldown <= 0)
            {
                m_FiringCooldown = 0;
                m_CannonIsFiring = false;
            }
            else if (m_FiringCooldown > 0)
            {
                m_CannonIsFiring = true;

                if (m_FiringCooldown <= (m_FiringDuration / 1.5f))
                {
                    m_PlayCannonSounds = false;
                    cannonAudioSource.PlayOneShot(sfx_CannonReload, cannon_Volume);
                    m_PlayCannonSounds = true;
                }

                if (m_FiringCooldown <= (m_FiringDuration / 10))
                {
                    m_PlayCannonSounds = false;
                }
            }

            //....................................................
            // TURNING LEFT

            if (m_CannonIsFiring == false)
            {
#if UNITY_EDITOR
                if (Input.GetKey(KeyCode.LeftArrow))
                {
#endif
                if (transform.localRotation.y >= (m_TurningLimit * -1))
                    {
                        if (!X_keyboardControl)
                        {
                        if ((m_CannonRotationValue > accelSafetyNegativeAxis) && (m_CannonRotationValue < 0.0f))
                            {
                                //print("greater than ciushion");
                                m_TurningSpeed = 0;
                                m_PlayCannonSounds = false;
                            }
                        else if (m_CannonRotationValue <= accelSafetyNegativeAxis)
                            {
                            m_TurningSpeed = m_TurningCapabilitySpeed * (m_CannonRotationValue * -1);
                                transform.Rotate(-aTurnVector);

                                cannonAudioSource.clip = sfx_CannonRotation;
                                m_PlayCannonSounds = true;

                                if (cannonRef_Spawner.m_IsSpawningOnLeftSide == true)
                                {
                                    m_ShowFuseBurning = true;
                                }
                                else
                                {
                                    m_ShowFuseBurning = false;
                                }
                            }
                        }
                        else if (X_keyboardControl)
                        {
                            m_TurningSpeed = m_TurningCapabilitySpeed;
                            transform.Rotate(-aTurnVector);

                            cannonAudioSource.clip = sfx_CannonRotation;
                            m_PlayCannonSounds = true;

                            if (cannonRef_Spawner.m_IsSpawningOnLeftSide == true)
                            {
                                m_ShowFuseBurning = true;
                            }
                            else
                            {
                                m_ShowFuseBurning = false;
                            }
                        }
                    }
#if UNITY_EDITOR
                    }
                    else if (Input.GetKeyUp(KeyCode.LeftArrow))
                    {
                        m_ShowFuseBurning = false;

                        m_PlayCannonSounds = false;
                    }

#endif
                //..................................................
                // TURNING RIGHT
#if UNITY_EDITOR
                if (Input.GetKey(KeyCode.RightArrow))
                    {
#endif
                    if (transform.localRotation.y <= m_TurningLimit)
                    {
                        if (!X_keyboardControl)
                        {
                        if ((m_CannonRotationValue < accelSafetyPositiveAxis) && (m_CannonRotationValue > 0.0f))
                            {
                                m_TurningSpeed = 0;
                                m_PlayCannonSounds = false;
                            }
                            else if (m_CannonRotationValue >= accelSafetyPositiveAxis)
                            {
                            m_TurningSpeed = m_TurningCapabilitySpeed * m_CannonRotationValue;
                                transform.Rotate(aTurnVector);

                                cannonAudioSource.clip = sfx_CannonRotation;
                                m_PlayCannonSounds = true;

                                if (cannonRef_Spawner.m_IsSpawningOnLeftSide == false)
                                {
                                    m_ShowFuseBurning = true;
                                }
                                else
                                {
                                    m_ShowFuseBurning = false;
                                }
                            }
                        }
                        else if (X_keyboardControl)
                        {
                            m_TurningSpeed = m_TurningCapabilitySpeed;
                            transform.Rotate(aTurnVector);

                            cannonAudioSource.clip = sfx_CannonRotation;
                            m_PlayCannonSounds = true;

                            if (cannonRef_Spawner.m_IsSpawningOnLeftSide == false)
                            {
                                m_ShowFuseBurning = true;
                            }
                            else
                            {
                                m_ShowFuseBurning = false;
                            }
                        }
                    }
#if UNITY_EDITOR
                    }
                    else if (Input.GetKeyUp(KeyCode.RightArrow))
                    {
                        m_ShowFuseBurning = false;
                        m_PlayCannonSounds = false;
                    }
#endif
            }
            }

        //..................................................
        // SFX

        if (m_PlayCannonSounds == true)
        {
            if (cannonAudioSource.isPlaying == false)
            {
                cannonAudioSource.Play();
            }
        }
        else if (m_PlayCannonSounds == false)
        {
            cannonAudioSource.Stop();
        }

        //....................................................
        // LOCK CONTROLS
        /*
        if (m_SceneManager.m_OrderInScene == 9)
        {
            m_PlayerControlsAreActive = false;
        }
        */

        if (m_uiManager.m_GameHasEnded)
            cannonAudioSource.Stop();
    }

    //....................................................................
    //.................................................... * FIRE CANNON *

    public void FireCannon()
    {
        if (m_FiringCooldown <= 0)
        {
            cannonAudioSource.clip = sfx_CannonFire;
            m_PlayCannonSounds = true;

            GameObject cannonSmokeObject;
            cannonSmokeObject = Instantiate(m_CannonSmokePrefab);
            cannonSmokeObject.transform.position = m_CannonBallSpawnPoint.position + 1.0f * Vector3.zero;

            fx_Timer = fx_TimerDur;

            /*
            CannonBall cannonBallObject;
            cannonBallObject = Instantiate(m_CannonBallPrefab) as CannonBall;

            cannonBallObject.transform.position = m_CannonBallSpawnPoint.position + 1.0f * Vector3.zero;
            cannonBallObject.m_CannonBallDir = transform.forward * 1;
            */

            m_FiringCooldown = m_FiringDuration;
        }
        fx_CannonLight.gameObject.SetActive(false);
    }
}