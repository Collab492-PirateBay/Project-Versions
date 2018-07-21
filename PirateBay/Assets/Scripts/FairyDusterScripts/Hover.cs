/* HOVER */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour 
{
    private Vector3 m_Dir;
    private bool isHoveringUp;

    private float hoverTimer = 0.0f;
    [SerializeField] private float m_HoverDur = 0.32f;

    [SerializeField] private float m_HoverSpeed = 0.18f;

    private Rigidbody m_RigidBody = null;

    //...............................................................
    //..................................................... * START *
    void Start()
    {
        hoverTimer = m_HoverDur;
        isHoveringUp = true;

        m_RigidBody = GetComponent<Rigidbody>();
    }

    //...............................................................
    //.................................................... * UPDATE *
    void Update()
    {
        Vector3 a_Velocity;
        a_Velocity = m_Dir * m_HoverSpeed;
        m_Dir = Vector3.zero;


        hoverTimer -= Time.deltaTime;

        if (hoverTimer <= 0)
        {
            hoverTimer = 0;

            if (isHoveringUp == true)
            {
                hoverTimer = m_HoverDur;
                isHoveringUp = false;

            }
            else if (isHoveringUp == false)
            {
                hoverTimer = m_HoverDur;
                isHoveringUp = true;

            }
        }



        if (isHoveringUp == true)
        {
            m_Dir += Vector3.up;
        }
        else if (isHoveringUp == false)
        {
            m_Dir += Vector3.down;
        }


        m_RigidBody.velocity = a_Velocity;
    }
}
