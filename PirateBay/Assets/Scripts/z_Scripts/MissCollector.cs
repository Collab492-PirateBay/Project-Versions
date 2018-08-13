/* MISS COLLECTOR */
/* BLAKE CURIA */

// when treasure chests (or other future items?) are not hit in time and go off screen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissCollector : MonoBehaviour
{
    //........................................................
    // RELATIVE SCRIPTS

    public Spawner m_SpawnerRef;

    public CannonMovement m_CannonMovementRef;

    public int m_MissesCount = 0;

    //............................................................
    //.................................................. * START *

    protected void Start()
    {
        GameObject spawnerObject = GameObject.FindGameObjectWithTag("Spawner");
        m_SpawnerRef = spawnerObject.GetComponent<Spawner>();

        GameObject cannonObject = GameObject.FindGameObjectWithTag("Cannon");
        m_CannonMovementRef = cannonObject.GetComponent<CannonMovement>();
    }

    //............................................................
    //............................................... * TRIGGERS *

    private void OnTriggerEnter(Collider colliderBox)
    {
        GameObject colliderObject;
        colliderObject = colliderBox.gameObject;

        BalloonedChest chestObject;
        chestObject = colliderObject.GetComponent<BalloonedChest>();

        if (chestObject != null)
        {
            if (m_SpawnerRef.m_SpawnDelayTimer <= 0)
            {
                m_SpawnerRef.m_SpawnDelayTimer = m_SpawnerRef.m_SpawnDelayDuration;
            }

            if (m_SpawnerRef.m_IsReSpawning == false)
            {
                m_CannonMovementRef.m_PlayerControlsAreActive = true;

                m_SpawnerRef.m_IsReSpawning = true;
            }

            m_MissesCount += 1;

            Destroy(chestObject.gameObject);
        }
    }
}