/* BALLOONED TREASURE CHEST */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonedChest : MonoBehaviour
{
    //........................................................
    // MOVEMENT VARIABLES
    private Rigidbody m_RigidBody = null;

    private float m_Speed;
    [SerializeField] private float m_RisingSpeed = 0.0f;
    [SerializeField] private float m_FallingSpeed = 0.0f;

    private Vector3 m_Direction = Vector3.zero;
    public bool m_IsGoingUp = true;

    //........................................................
    // LIFE VARIABLES

    public float m_HangTimer;
    public float m_HangDur = 0.0f;

    public Animator m_TreasureChestAnimator = null;

    public GameObject m_Balloons;


    //........................................................
    // RELATIVE SCRIPTS

    public MissCollector m_MissRef;

    [SerializeField] private GameObject m_BundleOfCoins = null;

    //........................................................
    // AUDIO SFX

    [SerializeField] private AudioSource balloonObjectAudioSource = null;
    public bool m_PlayBallonObjectSounds = false;

    [SerializeField] private AudioClip sfx_Splash = null;
    [SerializeField] private AudioClip sfx_BalloonPop = null;

    [SerializeField] private AudioClip sfx_JackpotCoins = null;
    [SerializeField] private AudioClip sfx_ChestOpen = null;


    //............................................................
    //.................................................. * START *
    void Start()
    {
        m_RigidBody = gameObject.GetComponent<Rigidbody>();

        GameObject sfxBalloonedObject = GameObject.FindWithTag("SharkSounds");
        balloonObjectAudioSource = sfxBalloonedObject.GetComponent<AudioSource>();

        GameObject missCollectorObject = GameObject.FindGameObjectWithTag("MissCollector");
        m_MissRef = missCollectorObject.GetComponent<MissCollector>();

        m_TreasureChestAnimator.enabled = true;
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
            m_TreasureChestAnimator.SetBool("OpenLid", true);
        }
        else if (m_IsGoingUp == true)
        {
            m_TreasureChestAnimator.SetBool("OpenLid", false);
        }

        //........................................................
        // MOVEMENT

        Vector3 m_Velocity;
        m_Velocity = m_Direction * m_Speed;

        if (m_IsGoingUp == true)
        {
            if (m_MissRef.m_MissesCount >= 1)
            {
                m_Speed = (m_RisingSpeed / 2.0f);
            }
            else if (m_MissRef.m_MissesCount <= 0)
            {
                m_Speed = m_RisingSpeed;
            }

            m_Velocity += Vector3.up * m_Speed;
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


        //..................................................
        // SFX

        if (m_PlayBallonObjectSounds == true)
        {
            if (balloonObjectAudioSource.isPlaying == false)
            {
                balloonObjectAudioSource.Play();
            }
        }
        else if (m_PlayBallonObjectSounds == false)
        {
            balloonObjectAudioSource.Stop();
        }


        m_RigidBody.velocity = m_Velocity;
    }
}