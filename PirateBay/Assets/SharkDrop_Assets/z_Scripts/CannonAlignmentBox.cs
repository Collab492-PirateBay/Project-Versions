/* CANNON ALIGNMENT BOX */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAlignmentBox : MonoBehaviour 
{
    //........................................................
    // VARIABLES

    public bool m_IsReadyingFire = false;
    public bool m_IsFiring = false;

    [SerializeField] private float m_FiringCooldownTimer;
    [SerializeField] private float m_FiringCooldownDur = 0.0f;


    //........................................................
    // RELATIVE SCRIPTS
    public CannonMovement m_Cannon;

    //............................................................
    //.................................................. * START *
    void Start()
    {
        GameObject cannonObject = GameObject.FindGameObjectWithTag("Cannon");
        m_Cannon = cannonObject.GetComponent<CannonMovement>();
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {
        m_FiringCooldownTimer -= Time.deltaTime;

        if (m_FiringCooldownTimer <= 0)
        {
            m_FiringCooldownTimer = 0;
        }

        if (m_FiringCooldownTimer > 0)
        {
            m_IsFiring = false;
        }
 

    }

    //............................................................
    //............................................... * TRIGGERS *

    private void OnTriggerEnter(Collider colliderBox)
    {
        GameObject colliderObject;
        colliderObject = colliderBox.gameObject;

        BalloonedObject balloonObject;
        balloonObject = colliderObject.GetComponent<BalloonedObject>();

        if (balloonObject != null)
        {
            if (m_FiringCooldownTimer <= 0)
            {
                m_IsFiring = true;

                m_Cannon.m_PlayerControlsAreActive = false;
                m_Cannon.FireCannon();

                //make prefab particle fx smoke
                //pop balloons

                balloonObject.m_IsGoingUp = false;
                balloonObject.m_HangTimer = balloonObject.m_HangDur;

                m_FiringCooldownTimer = m_FiringCooldownDur;
            }
        }
    }
}
