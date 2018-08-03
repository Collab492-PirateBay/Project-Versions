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

    public bool m_IsTargetAligned = false;

    public Transform m_CannonBallSpawnPoint;
    public CannonBall m_CannonBallPrefab;

    public Transform m_CannonSmokeSpawnPoint;
    public GameObject m_CannonSmokePrefab;

    private float m_FiringCooldown;
    [SerializeField] private float m_FiringDuration = 0.0f;

    //........................................................
    // KEYBOARD VARIABLES

    [SerializeField] private bool keyboardControl;

    //........................................................
    // ACCELEROMETER VARIABLES

    private NetworkServerUI netInput = null;
    private Vector3 accelerometerInput;

    private float m_CannonRotationValue = 0.0f;

    [SerializeField] private float accelerometerSafetyCushion = 0.0f;

    //........................................................
    // RELATIVE SCRIPTS

    public SceneManagement_SD m_SceneManager;

    //............................................................
    //.................................................. * START *
    void Start()
    {
        GameObject sceneManagerObject = GameObject.FindGameObjectWithTag("SceneManager");
        m_SceneManager = sceneManagerObject.GetComponent<SceneManagement_SD>();
        //Sets Network input
        netInput = GameManager.GameManagerInstance.gameObject.GetComponent<NetworkServerUI>();
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {
        // CONTROLLING PLAYER INPUT
        if (m_SceneManager.m_GameplayActive == true)
        {
            m_PlayerControlsAreActive = true;
        }
        else
        {
            m_PlayerControlsAreActive = false;
        }


        if (m_PlayerControlsAreActive == true)
        {
            //....................................................
            // ACCELEROMETER INPUTS

            if (!keyboardControl)
            {
                //Gets Accelerometer input from networked client
                accelerometerInput = new Vector3(netInput.accelX, netInput.accelY, netInput.accelZ);

                m_CannonRotationValue = accelerometerInput.sqrMagnitude;
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
            }

            //....................................................
            // TURNING LEFT

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (transform.localRotation.y >= (m_TurningLimit * -1))
                {
                    if (!keyboardControl)
                    {
                        if (m_CannonRotationValue < accelerometerSafetyCushion)
                        {
                            m_TurningSpeed = 0;
                        }
                        else if (m_CannonRotationValue <= accelerometerSafetyCushion)
                        {
                            m_TurningSpeed = m_TurningCapabilitySpeed;
                            transform.Rotate(-aTurnVector);
                        }
                    }
                    else if (keyboardControl)
                    {
                        m_TurningSpeed = m_TurningCapabilitySpeed;
                        transform.Rotate(-aTurnVector);
                    }
                }
            }

            //..................................................
            // TURNING RIGHT

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (transform.localRotation.y <= m_TurningLimit)
                {
                    if (!keyboardControl)
                    {
                        if (m_CannonRotationValue < accelerometerSafetyCushion)
                        {
                            m_TurningSpeed = 0;
                        }
                        else if (m_CannonRotationValue >= accelerometerSafetyCushion)
                        {
                            m_TurningSpeed = m_TurningCapabilitySpeed;
                            transform.Rotate(aTurnVector);
                        }
                    }
                    else if (keyboardControl)
                    {
                        m_TurningSpeed = m_TurningCapabilitySpeed;
                        transform.Rotate(aTurnVector);
                    }
                }
            }
        }

        if (m_SceneManager.m_OrderInScene == 9)
        {
            m_PlayerControlsAreActive = false;
        }
    }

    //....................................................................
    //.................................................... * FIRE CANNON *

    public void FireCannon()
    {
        if (m_FiringCooldown <= 0)
        {
            GameObject cannonSmokeObject;
            cannonSmokeObject = Instantiate(m_CannonSmokePrefab);

            cannonSmokeObject.transform.position = m_CannonBallSpawnPoint.position + 1.0f * Vector3.zero;


            CannonBall cannonBallObject;
            cannonBallObject = Instantiate(m_CannonBallPrefab) as CannonBall;

            cannonBallObject.transform.position = m_CannonBallSpawnPoint.position + 1.0f * Vector3.zero;
            cannonBallObject.m_CannonBallDir = transform.forward * 1;

            m_FiringCooldown = m_FiringDuration;
        }
    }
}