/* SPAWNER */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // SPAWNER CONTROLLER
    [SerializeField] public bool m_SpawnerIsActive = false;
    private bool m_FirstSpawnHasPassed = false;

    //........................................................
    // VARIABLES

    [SerializeField] private int m_SpawnLocationID_Current;
    [SerializeField] private int m_SpawnLocationID_Previous;

    public BalloonedShark m_SharkPrefab;
    public BalloonedChest m_TreasureChestPrefab;

    private float m_SpawnPosition;

    [SerializeField] private float m_DistanceFromCenter = 0.0f;
    [SerializeField] private float m_FurtherDistanceFomCenter = 0.0f;

    private int m_BalloonedItemID;
    private int m_ChestCount = 0;
    private int m_ChestMax = 2;
    private int m_SpawnCount = 0;

    //........................................................
    // SHARED VARIABLES

    public bool m_IsReSpawning;
    public bool m_IsSpawningOnLeftSide = false;
    public float m_SpawnDelayTimer;
    public float m_SpawnDelayDuration = 0.0f;

    //........................................................
    // RELATIVE SCRIPTS

    public SceneManagement_SD m_SceneManagerRef;


    //............................................................
    //.................................................. * START *
    void Start()
    {
        m_IsReSpawning = true;
        m_SpawnLocationID_Previous = Random.Range(1, 5);
        m_SpawnLocationID_Current = Random.Range(1, 5);
        m_SpawnDelayTimer = m_SpawnDelayDuration;

        GameObject sceneManagerRefObject = GameObject.FindGameObjectWithTag("SceneManager");
        m_SceneManagerRef = sceneManagerRefObject.GetComponent<SceneManagement_SD>();
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {

        // CONTROLLING PLAYER INPUT
        if (m_SceneManagerRef.m_GameplayActive == true)
        {
            m_SpawnerIsActive = true;
        }
        else
        {
            m_SpawnerIsActive = false;
        }

        // SPAWN LOCATION
        if (m_SpawnerIsActive == true)
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

            // SPAWN ITEM
            if (m_IsReSpawning == true)
            {
                if (m_SpawnDelayTimer <= 0)
                {
                    // CHOOSE LOCATION OPPOSITE OF LAST
                    if (m_SpawnLocationID_Previous <= 2)
                    {
                        m_SpawnLocationID_Current = Random.Range(3, 5);
                        m_IsSpawningOnLeftSide = false;
                    }
                    else if (m_SpawnLocationID_Previous >= 3)
                    {
                        m_SpawnLocationID_Current = Random.Range(1, 3);
                        m_IsSpawningOnLeftSide = true;
                    }

                    // CHOOSE ITEM TYPE TO SPAWN (Shark ID = 1, Treasure Chest ID = 2)
                    if (m_FirstSpawnHasPassed == false)
                    {
                        m_BalloonedItemID = 1;
                        m_FirstSpawnHasPassed = true;
                    }
                    else if (m_FirstSpawnHasPassed == true)
                    {
                        if (m_ChestCount < m_ChestMax)
                        {
                            if ((m_SpawnCount >= 3) && (m_SpawnCount <= 4))
                            {
                                m_BalloonedItemID = 2;
                            }
                            else
                            {
                                m_BalloonedItemID = Random.Range(1, 3);
                            }
                        }
                        else if (m_ChestCount >= m_ChestMax)
                        {
                            m_BalloonedItemID = 1;
                        }
                    }

                    // SPAWN SHARK

                    if (m_BalloonedItemID == 1)
                    {
                        BalloonedShark sharkObject;
                        sharkObject = Instantiate(m_SharkPrefab) as BalloonedShark;

                        sharkObject.transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z + m_SpawnPosition));
                    }
                    else if (m_BalloonedItemID == 2)
                    {
                        BalloonedChest treasureChestObject;
                        treasureChestObject = Instantiate(m_TreasureChestPrefab) as BalloonedChest;

                        treasureChestObject.transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z + m_SpawnPosition));

                        m_ChestCount += 1;
                    }

                    m_SpawnCount += 1;
                    m_IsReSpawning = false;
                }
            }

            m_SpawnDelayTimer -= Time.deltaTime;

            if (m_SpawnDelayTimer <= 0)
            {
                m_SpawnDelayTimer = 0;
            }


            if (m_SceneManagerRef.m_OrderInScene == 9)
            {
                m_SpawnerIsActive = false;
            }
        }
    }
}