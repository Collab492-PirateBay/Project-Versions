/* BALLOONED OBJECTS */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonedObject : MonoBehaviour
{

    //........................................................
    // MOVEMENT VARIABLES
    private Rigidbody m_RigidBody = null;

    private float m_Speed;
    [SerializeField] private float m_RisingSpeed = 0.0f;
    [SerializeField] private float m_FallingSpeed = 0.0f;

    [SerializeField] private float m_HeightCap = 0.0f;

    private Vector3 m_Direction = Vector3.zero;
    [SerializeField] private bool m_IsGoingUp = true;

    //........................................................
    // LIFE VARIABLES
    public bool m_IsVulnerable = false;
    [SerializeField] private float m_VulnerabilityHeight = 0.0f;

    [SerializeField] private int m_HP = 1;

    private float m_CooldownTimer;
    private float m_CooldownDur = 0.8f;

    //public string m_BalloonItemType;

    //............................................................
    //.................................................. * START *
    void Start()
    {
        m_RigidBody = gameObject.GetComponent<Rigidbody>();
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {
        //........................................................
        // LIFE

        if (m_HP <= 0)
        {
            m_HP = 0;
            m_IsVulnerable = false;
            m_IsGoingUp = false;
        }

        m_CooldownTimer -= Time.deltaTime;
        if (m_CooldownTimer <= 0)
        {
            m_CooldownTimer = 0;
        }


        //........................................................
        // MOVEMENT

        Vector3 m_Velocity;
        m_Velocity = m_Direction * m_Speed;

        if (m_IsGoingUp == true)
        {
            if (transform.position.y <= m_HeightCap)
            {
                m_Speed = m_RisingSpeed;
                m_Velocity += Vector3.up * m_Speed;
            }
        }
        else if (m_IsGoingUp == false)
        {
            m_Speed = m_FallingSpeed;
            m_Velocity += Vector3.down * m_Speed;
        }

        // VULNERABILITY SENSOR

        if (transform.position.y <= m_VulnerabilityHeight)
        {
            m_IsVulnerable = false;
        }
        else if (transform.position.y > m_VulnerabilityHeight)
        {
            m_IsVulnerable = true;
        }


        m_RigidBody.velocity = m_Velocity;
    }

    //............................................................
    //............................................. * COLLISIONS *

    private void OnTriggerEnter(Collider collider)
    {
        GameObject colliderObject;
        colliderObject = collider.gameObject;

        CannonBall cannonBallObject;
        cannonBallObject = colliderObject.GetComponent<CannonBall>();

        if ((cannonBallObject != null) && (m_IsVulnerable == true))
        {
            if (m_CooldownTimer <= 0)
            {
                Destroy(cannonBallObject.gameObject);
                m_HP -= 1;
                m_CooldownTimer = m_CooldownDur;
            }
        }
    }
}