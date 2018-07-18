/* AR MARKER */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARmarker : MonoBehaviour 
{
    //MARKER TYPE
    //you have to check-off the specific card in the Inspector for the 4 different AR markers below...
    [SerializeField] public bool m_AnchorMarker;
    [SerializeField] public bool m_SkullMarker;
    [SerializeField] public bool m_HelmMarker;
    [SerializeField] public bool m_ParrotMarker;
    [SerializeField] private int m_MarkerNumber;
    [SerializeField] public int m_CurrentMarkerNumber;

    //Marked Active by order of cards/markers
    public bool m_IsActive;

    //SPAWNING INFO
    public FairyMovement m_FairyPrefab;


    //PLACEMENT
    [SerializeField] private float m_FairyPlacementX;
    [SerializeField] private float m_FairyPlacementZ;

    //how far the fairies can spawn from the AR marker on the horizontal axis...
    [SerializeField] private float m_FairyPlacementHorizonMaxDist = 0.0f;

    [SerializeField] private float m_FairyPlacementY;
    //how far the fairies can spawn from the AR marker on the vertical axis...
    [SerializeField] private float m_FairyPlacementVerticalMaxDist = 0.0f;
    [SerializeField] private float m_FairyPlacementVerticalMinDist = 0.0f;

    //SPAWN TIMER
    [SerializeField] private float m_SpawnTimer;
    [SerializeField] private float m_SpawnDur;
    [SerializeField] private float m_SpawnDurMin;
    [SerializeField] private float m_SpawnDurMax;

    [SerializeField] private int m_SupplyOfFairies;
    [SerializeField] private int m_MinSupplyOfFairies = 0;
    [SerializeField] private int m_MaxSupplyOfFairies = 0;


	void Start () 
    {
        m_SupplyOfFairies = Random.Range(m_MinSupplyOfFairies, m_MaxSupplyOfFairies);

        m_CurrentMarkerNumber = 1;

        if (m_AnchorMarker == true)
        {
            m_IsActive = true;
            m_MarkerNumber = 1;
        }
        else if (m_SkullMarker == true)
        {
            m_MarkerNumber = 2;
        }
        else if (m_HelmMarker == true)
        {
            m_MarkerNumber = 3;
        }
        else if (m_ParrotMarker == true)
        {
            m_MarkerNumber = 4;
        }
        else
        {
            m_MarkerNumber = 0;
        }
	
	}
	
	void Update () 
    {
        m_SpawnTimer -= Time.deltaTime;
        if (m_SpawnTimer <= 0)
        {
            m_SpawnTimer = 0;
        }


        if (m_IsActive == true)
        {
            if (m_SupplyOfFairies > 0)
            {
                if (m_SpawnTimer <= 0)
                {
                    m_SpawnDur = Random.Range(m_SpawnDurMin, m_SpawnDurMax);

                    FairyMovement m_FairyObject;
                    m_FairyObject = Instantiate(m_FairyPrefab) as FairyMovement;

                    m_FairyPlacementX = Random.Range(0, m_FairyPlacementHorizonMaxDist);
                    m_FairyPlacementZ = Random.Range(0, m_FairyPlacementHorizonMaxDist);
                    m_FairyPlacementY = Random.Range(m_FairyPlacementVerticalMinDist, m_FairyPlacementVerticalMaxDist);

                    m_FairyObject.transform.position = new Vector3((transform.position.x + m_FairyPlacementX), (transform.position.y + m_FairyPlacementY), (transform.position.z + m_FairyPlacementZ));


                    m_SupplyOfFairies -= 1;
                    m_SpawnTimer = m_SpawnDur;
                }
            }
        }

        if ((m_SupplyOfFairies <= 0) && (m_IsActive == true))
        {
            m_IsActive = false;
            m_CurrentMarkerNumber += 1;
            m_SupplyOfFairies = Random.Range(m_MinSupplyOfFairies, m_MaxSupplyOfFairies);
        }

        if ((m_MarkerNumber == m_CurrentMarkerNumber) && (m_IsActive == false))
        {
            m_IsActive = true;
        }

        if (m_CurrentMarkerNumber > 4)
        {
            m_CurrentMarkerNumber = 0;
        }

	}


    //only used for testing, Vuforia doesn't need this to read the AR markers...
	private void OnBecameVisible()
	{
        //AR MARKER 1 starts off active
        if ((m_MarkerNumber == m_CurrentMarkerNumber) && (m_IsActive == true))
        {
            enabled = true;
        }

	}

	private void OnBecameInvisible()
	{
        enabled = false;
	}
}
