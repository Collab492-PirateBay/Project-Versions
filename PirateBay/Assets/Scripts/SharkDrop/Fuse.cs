/* FUSE */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour 
{
    //........................................................
    // VARIABLES

    [SerializeField] private Vector3 m_DefaultPosition;

    private float m_ShrinkRate = 0.0f;
    [SerializeField] private float m_Speed = 0.0f;

    public ParticleSystem m_Flame = null;

    public bool m_FlameIsActive = false;


    //........................................................
    // RELATIVE SCRIPTS

    public CannonMovement fuseRef_CannonMovement;

    //............................................................
    //.................................................. * START *
    void Start()
    {
        GameObject cannonObject = GameObject.FindGameObjectWithTag("Cannon");
        fuseRef_CannonMovement = cannonObject.GetComponent<CannonMovement>();

        GameObject flameObject = GameObject.Find("Flame");
        m_Flame = flameObject.GetComponent<ParticleSystem>();

        m_DefaultPosition = transform.position;
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {
        if (fuseRef_CannonMovement.m_NeedNewFuse == true)
        {
            m_Flame.gameObject.SetActive(true);
            transform.localPosition= m_DefaultPosition;
        }


        if (fuseRef_CannonMovement.m_ShowFuseBurning == true)
        {
            m_Flame.gameObject.SetActive(true);
            m_ShrinkRate = m_Speed;
            transform.Translate(0, m_ShrinkRate * Time.deltaTime, 0);
        }
        else if (fuseRef_CannonMovement.m_ShowFuseBurning == false)
        {
            m_ShrinkRate = 0.0f;
            m_Flame.gameObject.SetActive(false);
        }
    }
}