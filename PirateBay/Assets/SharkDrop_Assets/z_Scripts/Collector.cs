/* COLLECTOR */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour 
{
    //........................................................
    // VARIABLES

    [SerializeField] private int m_CollectionsCounter = 0;
    [SerializeField] private int m_GameOverRequirement = 4;

    //........................................................
    // RELATIVE SCRIPTS
    public Spawner m_Spawner;



    //............................................................
    //.................................................. * START *
    void Start()
    {
        GameObject spawnerObject = GameObject.FindGameObjectWithTag("Spawner");
        m_Spawner = spawnerObject.GetComponent<Spawner>();
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {
        if (m_CollectionsCounter >= m_GameOverRequirement)
        {
            // END GAME!!!!
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
            if (m_Spawner.m_SpawnDelayTimer <= 0)
            {
                m_Spawner.m_SpawnDelayTimer = m_Spawner.m_SpawnDelayDuration;
            }

            if (m_Spawner.m_IsReSpawning == false)
            {
                m_CollectionsCounter += 1;
                Destroy(balloonObject.gameObject);
                m_Spawner.m_IsReSpawning = true;
            }
        }
    }
}