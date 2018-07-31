/* RETICLE */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour 
{
    //.......................................................
    // RAYCAST VARIABLES
    [SerializeField] private float m_CannonFireRange = 15.0f;
    private LineRenderer m_LineRenderer = null;

    public GameObject rayOriginCannonBarrel;

    public Material rayMaterial = null;

    //........................................................
    // RETICLE VARIABLES
    [SerializeField] private GameObject m_Reticle;
    [SerializeField] private GameObject m_ReticleFrame;

    [SerializeField] private float m_ReticleRange = 0.0f;

    public bool m_IsReadyingFire = false;
    public bool m_IsFiring = false;



    //........................................................
    // RELATIVE SCRIPTS
    public CannonMovement m_Cannon;

    //............................................................
    //.................................................. * START *
    void Start()
    {
        // RAYCAST
        m_LineRenderer = GetComponent<LineRenderer>();
        m_LineRenderer.sharedMaterial = rayMaterial;

        GameObject cannonObject = GameObject.FindGameObjectWithTag("Cannon");
        m_Cannon = cannonObject.GetComponent<CannonMovement>();

        m_Reticle = Instantiate(m_Reticle) as GameObject;
        m_ReticleFrame = Instantiate(m_ReticleFrame) as GameObject;
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {
        // RAYCAST
        m_LineRenderer.SetPosition(0, rayOriginCannonBarrel.transform.position);
        m_LineRenderer.SetPosition(1, rayOriginCannonBarrel.transform.position + m_CannonFireRange * transform.forward);

        GameObject a_HitObject;
        a_HitObject = null;

        //so the load bar is barely infront of the reticle frame
        float m_ReticleLoadBarRange = m_ReticleRange + 0.001f;

        Ray a_Ray = new Ray();
        a_Ray.origin = rayOriginCannonBarrel.transform.position;
        a_Ray.direction = transform.forward;

        RaycastHit a_RayCastaHit;

        if (Physics.Raycast(a_Ray, out a_RayCastaHit, m_CannonFireRange))
        {
            m_LineRenderer.SetPosition(1, a_RayCastaHit.point);
            a_HitObject = a_RayCastaHit.transform.gameObject;
            m_ReticleFrame.SetActive(true);
            m_ReticleFrame.transform.position = (a_Ray.origin + a_Ray.direction + m_ReticleRange * transform.forward);
        }
        else
        {
            m_ReticleFrame.SetActive(true);
            m_ReticleFrame.transform.position = (a_Ray.origin + a_Ray.direction + m_ReticleRange * transform.forward);
            m_IsReadyingFire = false;
            m_Reticle.SetActive(false);
        }


        if (a_HitObject != null)
        {
            BalloonedObject balloonedTargets;
            balloonedTargets = a_HitObject.GetComponent<BalloonedObject>();

            if (balloonedTargets != null)
            {
                m_Reticle.SetActive(true);
                m_Reticle.transform.position = (a_Ray.origin + a_Ray.direction + m_ReticleLoadBarRange * transform.forward);

                m_IsReadyingFire = true;
            }
            else
            {
                m_IsReadyingFire = false;
                m_Reticle.SetActive(false);
            }
        }

        if (m_IsFiring == true)
        {
            m_Cannon.FireCannon();
            m_IsReadyingFire = false;
            m_IsFiring = false;
        }
    }
}