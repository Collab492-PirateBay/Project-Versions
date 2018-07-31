/* SPAWNER */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //........................................................
    // VARIABLES

    [SerializeField] private int m_SpawnLocationID_Current;
    [SerializeField] private int m_SpawnLocationID_Previous;

    public BalloonedObject m_SharkPrefab;

    private float m_SpawnPosition;

    [SerializeField] private float m_DistanceFromCenter = 0.0f;
    [SerializeField] private float m_FurtherDistanceFomCenter = 0.0f;

    //........................................................
    // SHARED VARIABLES

    public bool m_IsReSpawning;
    public float m_SpawnDelayTimer;
    public float m_SpawnDelayDuration = 0.0f;

    //public bool m_IsSpawnedObjectOnLeft = false;

    //............................................................
    //.................................................. * START *
    void Start()
    {
        m_IsReSpawning = true;
        m_SpawnLocationID_Previous = Random.Range(1, 5);
        m_SpawnLocationID_Current = Random.Range(1, 5);
        m_SpawnDelayTimer = m_SpawnDelayDuration;
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {
        // CLOSER LEFT POSITION
        if (m_SpawnLocationID_Current == 1)
        {
            m_SpawnPosition = m_DistanceFromCenter * -1;
            m_SpawnLocationID_Previous = 1;
        }
        else // FURTHER LEFT POSITION
        if (m_SpawnLocationID_Current == 2)
        {
            m_SpawnPosition = m_FurtherDistanceFomCenter * -1;
            m_SpawnLocationID_Previous = 2;
        }
        else // CLOSER RIGHT POSITION
        if (m_SpawnLocationID_Current == 3)
        {
            m_SpawnPosition = m_DistanceFromCenter * 1;
            m_SpawnLocationID_Previous = 3;
        }
        else // FURTHER RIGHT POSITION
        if (m_SpawnLocationID_Current == 4)
        {
            m_SpawnPosition = m_FurtherDistanceFomCenter * 1;
            m_SpawnLocationID_Previous = 4;
        }


        if (m_IsReSpawning == true)
        {
            if (m_SpawnDelayTimer <= 0)
            {
                if (m_SpawnLocationID_Previous <= 2)
                {
                    m_SpawnLocationID_Current = Random.Range(3, 5);
                    //m_IsSpawnedObjectOnLeft = false;
                }
                else if (m_SpawnLocationID_Previous >= 3)
                {
                    m_SpawnLocationID_Current = Random.Range(1, 3);
                    //m_IsSpawnedObjectOnLeft = true;
                }

                BalloonedObject sharkObject;
                sharkObject = Instantiate(m_SharkPrefab) as BalloonedObject;

                sharkObject.transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z + m_SpawnPosition));

                m_IsReSpawning = false;
            }
        }

        m_SpawnDelayTimer -= Time.deltaTime;

        if (m_SpawnDelayTimer <= 0)
        {
            m_SpawnDelayTimer = 0;
        }
    }
}