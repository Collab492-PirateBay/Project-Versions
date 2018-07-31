/* RETICLE LOADING BAR */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleLoadingBar : MonoBehaviour 
{
    //........................................................
    // VARIABLES

    [SerializeField] private float m_ReadyFireTimer;
    [SerializeField] private float m_ReadyFireDur = 0.0f;

    private Renderer m_ReticleRenderer = null;
    public Material m_ReticleMaterial = null;

    //[SerializeField] private bool m_IsResettingTimer = false;

    //........................................................
    // RELATIVE SCRIPTS
    public Reticle m_ReticleReference;


    //............................................................
    //.................................................. * START *
    void Start()
    {
        GameObject reticleObject = GameObject.FindGameObjectWithTag("Reticle");
        m_ReticleReference = reticleObject.GetComponent<Reticle>();

        m_ReticleRenderer = GetComponent<Renderer>();
        m_ReticleRenderer.sharedMaterial = m_ReticleMaterial;

        m_ReadyFireTimer = m_ReadyFireDur;
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {
        
        if (m_ReticleReference.m_IsReadyingFire == false)
        {
            //m_IsResettingTimer = true;
            m_ReadyFireTimer = m_ReadyFireDur;
        }
        else if (m_ReticleReference.m_IsReadyingFire == true)
        {
            m_ReadyFireTimer -= Time.deltaTime;
        }


        if (m_ReadyFireTimer <= 0.001f)
        {
            m_ReadyFireTimer = 0.001f;

            if (m_ReticleReference.m_IsReadyingFire == true)
            {
                m_ReticleReference.m_IsFiring = true;
                m_ReticleReference.m_IsReadyingFire = false;
            }
            else if (m_ReticleReference.m_IsReadyingFire == false)
            {
                m_ReticleReference.m_IsFiring = false;
            }
        }

        //// INCASE YOU DECIDE TO GO BACK TO STANDARD SHADER (didn't work really)
        //Color aColor;

        //aColor = m_ReticleMaterial.color;
        //aColor.a = m_ReadyFireTimer / m_ReadyFireDur;

        //m_ReticleMaterial.color = aColor;


        // ALPHA CUTOFF
        m_ReticleRenderer.material.SetFloat("_Cutoff", (m_ReadyFireTimer / m_ReadyFireDur));
    }
}