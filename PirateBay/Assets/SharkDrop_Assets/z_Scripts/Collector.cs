/* COLLECTOR */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour 
{
    //........................................................
    // VARIABLES
    [SerializeField] private int m_CollectionsCounter = 0;
    [SerializeField] private int m_GameOverRequirement = 4;

    public Text m_ScoreText;
    public Text m_ScoreTextShadow;

    public bool m_GameIsOver = false;

    public int m_GoldEarned = 0;
    public Text m_GoldEarnedText;
    public Text m_GoldEarnedTextShadow;

    //........................................................
    // RELATIVE SCRIPTS
    public Spawner m_Spawner;

    public CannonMovement m_CannonMovement;

    //............................................................
    //.................................................. * START *
    void Start()
    {
        GameObject spawnerObject = GameObject.FindGameObjectWithTag("Spawner");
        m_Spawner = spawnerObject.GetComponent<Spawner>();

        GameObject cannonObject = GameObject.FindGameObjectWithTag("Cannon");
        m_CannonMovement = cannonObject.GetComponent<CannonMovement>();
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {
        if (m_CollectionsCounter >= m_GameOverRequirement)
        {
            m_GameIsOver = true;
        }

        m_ScoreText.text = m_CollectionsCounter + " / " + m_GameOverRequirement;
        m_ScoreTextShadow.text = m_CollectionsCounter + " / " + m_GameOverRequirement;

        m_GoldEarnedText.text = "+ " + m_GoldEarned;
        m_GoldEarnedTextShadow.text = "+ " + m_GoldEarned;
    }

    //............................................................
    //............................................... * TRIGGERS *

    private void OnTriggerEnter(Collider colliderBox)
    {
        GameObject colliderObject;
        colliderObject = colliderBox.gameObject;

        BalloonedShark sharkObject;
        sharkObject = colliderObject.GetComponent<BalloonedShark>();

        BalloonedChest chestObject;
        chestObject = colliderObject.GetComponent<BalloonedChest>();

        if (sharkObject != null)
        {
            if (m_Spawner.m_SpawnDelayTimer <= 0)
            {
                m_Spawner.m_SpawnDelayTimer = m_Spawner.m_SpawnDelayDuration;
            }

            if (m_Spawner.m_IsReSpawning == false)
            {
                m_CollectionsCounter += 1;

                m_CannonMovement.m_PlayerControlsAreActive = true;

                m_Spawner.m_IsReSpawning = true;
            }

            Destroy(sharkObject.gameObject);
        }

        if (chestObject != null)
        {
            if (m_Spawner.m_SpawnDelayTimer <= 0)
            {
                m_Spawner.m_SpawnDelayTimer = m_Spawner.m_SpawnDelayDuration;
            }

            if (m_Spawner.m_IsReSpawning == false)
            {
                m_GoldEarned += Random.Range(50, 201);

                m_CannonMovement.m_PlayerControlsAreActive = true;

                m_Spawner.m_IsReSpawning = true;
            }

            Destroy(chestObject.gameObject);
        }
    }
}