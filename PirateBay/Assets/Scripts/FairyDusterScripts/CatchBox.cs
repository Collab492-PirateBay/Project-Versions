/* CATCH BOX */
//invisible box attached to and infront of camera
//when a fairy enters it, they become catchable
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBox : MonoBehaviour
{
    [SerializeField] public bool m_FairyIsInCatchZone = false;
    [SerializeField] public bool m_PlayerIsInShakeMode = false;

    

    public bool m_SwingIsStrongEnough = false;

    //FAIRIES IN CATCH BOX
    private int m_AmountOfReds = 0;
    private int m_AmountOfGreens = 0;
    private int m_AmountOfBlues = 0;
    private int m_AmountOfYellows = 0;
    private int m_TotalAmountOfFairiesInBox = 0;
    //RANDOM RANGE VARIABLES
    private int m_FairyPicker = 0;
    public int m_FairyCaughtID;



    //get type of all fairies in the box w/trigger and function
    //catch ratio picks one of those fairies and throws their id in a randomizer
    //picks the fairy type and sends the info to the shaking script


    void Start ()
    {
		

    }
	
	void Update ()
    {
		if (m_SwingIsStrongEnough)
        {
            m_FairyPicker = Random.Range(1, (m_TotalAmountOfFairiesInBox + 1));

            //RED FAIRY IS CAUGHT
            if (m_FairyPicker >= (m_TotalAmountOfFairiesInBox - (m_TotalAmountOfFairiesInBox * 0.15f)))
            {
                if (m_AmountOfReds > 0)
                {
                    //RED ID = 4
                    m_FairyCaughtID = 4;
                }
                else if (m_AmountOfGreens > 0)
                {
                    //GREEN ID = 3
                    m_FairyCaughtID = 3;
                }
                else if (m_AmountOfBlues > 0)
                {
                    //BLUES ID = 2
                    m_FairyCaughtID = 2;
                }
                else if (m_AmountOfYellows > 0)
                {
                    //YELLOW ID = 1
                    m_FairyCaughtID = 1;
                }
                else
                {
                    m_FairyCaughtID = 0;
                }
            }
            else if ((m_FairyPicker < (m_TotalAmountOfFairiesInBox - (m_TotalAmountOfFairiesInBox * 0.15f))) && (m_FairyPicker > (m_TotalAmountOfFairiesInBox - (m_TotalAmountOfFairiesInBox * 0.35f))))
            {
                //GREEN FAIRY IS CAUGHT
              
                if (m_AmountOfGreens > 0)
                {
                    m_FairyCaughtID = 3;
                }
                else if (m_AmountOfBlues > 0)
                {
                    m_FairyCaughtID = 2;
                }
                else if (m_AmountOfYellows > 0)
                {
                    m_FairyCaughtID = 1;
                }
                else if (m_AmountOfReds > 0)
                {
                    m_FairyCaughtID = 4;
                }
                else
                {
                    m_FairyCaughtID = 0;
                }
            }
            else if ((m_FairyPicker <= (m_TotalAmountOfFairiesInBox - (m_TotalAmountOfFairiesInBox * 0.35f))) && (m_FairyPicker > (m_TotalAmountOfFairiesInBox - (m_TotalAmountOfFairiesInBox * 0.5f))))
            {
                //BLUE FAIRY IS CAUGHT
                
                if (m_AmountOfBlues > 0)
                {
                    m_FairyCaughtID = 2;
                }
                else if (m_AmountOfYellows > 0)
                {
                    m_FairyCaughtID = 1;
                }
                else if (m_AmountOfGreens > 0)
                {
                    m_FairyCaughtID = 3;
                }
                else if (m_AmountOfReds > 0)
                {
                    m_FairyCaughtID = 4;
                }
                else
                {
                    m_FairyCaughtID = 0;
                }
            }
            else if (m_FairyPicker <= (m_TotalAmountOfFairiesInBox - (m_TotalAmountOfFairiesInBox * 0.5f)))
            {
                //YELLOW FAIRY IS CAUGHT
                if (m_AmountOfYellows > 0)
                {
                    m_FairyCaughtID = 4;
                }
                else if (m_AmountOfBlues > 0)
                {
                    m_FairyCaughtID = 3;
                }
                else if (m_AmountOfGreens > 0)
                {
                    m_FairyCaughtID = 2;
                }
                else if (m_AmountOfYellows > 0)
                {
                    m_FairyCaughtID = 1;
                }
                else
                {
                    m_FairyCaughtID = 0;
                }
            }
            m_SwingIsStrongEnough = false;
        }

        if (m_PlayerIsInShakeMode)
        {
            gameObject.SetActive(false);
        }
        else if (m_PlayerIsInShakeMode == true)
        {
            gameObject.SetActive(true);
        }

	}

    protected void OnTriggerEnter(Collider catchBoxCollider)
    {
        GameObject aCollisionObject;
        aCollisionObject = catchBoxCollider.gameObject;

        FairyMovement aFairy;
        aFairy = catchBoxCollider.GetComponent<FairyMovement>();

        if (aFairy != null)
        {
            m_FairyIsInCatchZone = true;

            if (aFairy.m_FairyIsRed)
            {
                m_AmountOfReds += 1;
                m_TotalAmountOfFairiesInBox += 1;
            }
            else if (aFairy.m_FairyIsGreen)
            {
                m_AmountOfGreens += 1;
                m_TotalAmountOfFairiesInBox += 1;
            }
            else if (aFairy.m_FairyIsBlue)
            {
                m_AmountOfBlues += 1;
                m_TotalAmountOfFairiesInBox += 1;
            }
            else if (aFairy.m_FairyIsYellow)
            {
                m_AmountOfYellows += 1;
                m_TotalAmountOfFairiesInBox += 1;
            }
        }
    }

    protected void OnTriggerExit(Collider catchBoxContainer)
    {
        GameObject aCollisionObject;
        aCollisionObject = catchBoxContainer.gameObject;

        FairyMovement aFairy;
        aFairy = catchBoxContainer.GetComponent<FairyMovement>();

        m_FairyIsInCatchZone = false;

        if (aFairy.m_FairyIsRed)
        {
            m_AmountOfReds -= 1;
            m_TotalAmountOfFairiesInBox -= 1;
        }
        else if (aFairy.m_FairyIsGreen)
        {
            m_AmountOfGreens -= 1;
            m_TotalAmountOfFairiesInBox -= 1;
        }
        else if (aFairy.m_FairyIsBlue)
        {
            m_AmountOfBlues -= 1;
            m_TotalAmountOfFairiesInBox -= 1;
        }
        else if (aFairy.m_FairyIsYellow)
        {
            m_AmountOfYellows -= 1;
            m_TotalAmountOfFairiesInBox -= 1;
        }
    }

}
