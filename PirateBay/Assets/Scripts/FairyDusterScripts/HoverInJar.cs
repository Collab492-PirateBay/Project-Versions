/* HOVERING IN THE JAR */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverInJar : MonoBehaviour 
{
    private bool isHoveringUp;

    private float hoverTimer = 0.0f;
    [SerializeField] private float m_HoverDur = 0.32f;

    [SerializeField] private float m_HoverSpeed = 0.18f;


    //...............................................................
    //..................................................... * START *
    void Start()
    {
        hoverTimer = m_HoverDur;
        isHoveringUp = true;

    }

    //...............................................................
    //.................................................... * UPDATE *
    void Update()
    {
        
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
            //dont want rigidbody, use transform translate instead?
            //m_Dir += Vector3.up;
        }
        else if (isHoveringUp == false)
        {
           // m_Dir += Vector3.down;
        }
    }
}
