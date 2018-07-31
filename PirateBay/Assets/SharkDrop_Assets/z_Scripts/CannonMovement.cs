/* CANNON MOVEMENT */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    //........................................................
    // COMMON VARIABLES

    [SerializeField] private float m_TurningLimit = 0.15f;

    [SerializeField] private float m_TurningCapabilitySpeed = 0.0f;

    private float m_TurningSpeed = 0.0f;

    public bool m_IsTargetAligned = false;

    public Transform m_CannonBallSpawnPoint;
    public CannonBall m_CannonBallPrefab;

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

    //............................................................
    //.................................................. * START *
    void Start()
    {
        //transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
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

        // FIRING
        if (m_IsTargetAligned == true)
        {
            FireCannon();
            m_IsTargetAligned = false;
        }
    }

    //....................................................................
    //.................................................... * FIRE CANNON *

    public void FireCannon()
    {
        if (m_FiringCooldown <= 0)
        {
            CannonBall cannonBallObject;
            cannonBallObject = Instantiate(m_CannonBallPrefab) as CannonBall;

            cannonBallObject.transform.position = m_CannonBallSpawnPoint.position + 1.0f * Vector3.zero;
            cannonBallObject.m_CannonBallDir = transform.forward * 1;

            m_FiringCooldown = m_FiringDuration;
        }
    }
}