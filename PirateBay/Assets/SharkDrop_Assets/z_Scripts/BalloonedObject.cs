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
    public bool m_IsGoingUp = true;

    //........................................................
    // LIFE VARIABLES

    public float m_HangTimer;
    public float m_HangDur = 0.0f;

    public Animator m_SharkAnimator;

    public GameObject m_Balloons;

    //............................................................
    //.................................................. * START *
    void Start()
    {
        m_RigidBody = gameObject.GetComponent<Rigidbody>();

        m_SharkAnimator.enabled = true;
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {
        m_HangTimer -= Time.deltaTime;


        if (m_HangTimer <= 0)
        {
            m_HangTimer = 0;
        }

        //........................................................
        // LIFE

        if (m_IsGoingUp == false)
        {
                m_SharkAnimator.SetBool("IsHappy", true);
                m_SharkAnimator.SetBool("IsSad", false);
        }
        else if (m_IsGoingUp == true)
        {
            m_SharkAnimator.SetBool("IsHappy", false);
            m_SharkAnimator.SetBool("IsSad", true);
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


        if (m_IsGoingUp == false)
        {
            if (m_HangTimer <= 0)
            {
                m_Balloons.gameObject.SetActive(false);
                m_Speed = m_FallingSpeed;
                m_Velocity += Vector3.down * m_Speed;
            }
        }

        m_RigidBody.velocity = m_Velocity;
    }
}