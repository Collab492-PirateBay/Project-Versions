/* WING FLUTTER */
/* ALSO WING COLOR */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingFlutter : MonoBehaviour
{
    //SET IN INSPECTOR FOR EACH WING
    [SerializeField] private bool isRightWingObject;

    //..............................................
    //STATS
    [SerializeField] private float m_FlapSpeed = 0.0f;

    private bool isFlappingLeft;

    private float flapTimer;
    [SerializeField] private float m_FlapDur = 0.0f;

    //WING COLOR CHANGE
    private Renderer m_FairyWingRenderer = null;
    public Material[] m_FairyWingMaterial = null;

    [SerializeField] private GameObject m_Fairy;
    [SerializeField] private FairyMovement ownerOfTheseWings;

    //...............................................................
    //..................................................... * START *
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        if (isRightWingObject == true)
        {
            isFlappingLeft = false;
        }
        else if (isRightWingObject == false)
        {
            isFlappingLeft = true;
        }

        //Getting the color of the fairy
        m_Fairy = GameObject.FindWithTag("Fairy");
        ownerOfTheseWings = m_Fairy.GetComponent<FairyMovement>();

        m_FairyWingRenderer = GetComponent<Renderer>();
    }

    //...............................................................
    //.................................................... * UPDATE *
    void Update()
    {
        flapTimer -= Time.deltaTime;


        if (flapTimer <= 0)
        {
            flapTimer = 0;

            if (isFlappingLeft == true)
            {
                isFlappingLeft = false;
            }
            else if (isFlappingLeft == false)
            {
                isFlappingLeft = true;
            }

            flapTimer = m_FlapDur;
        }


        //FLAPPING VARIABLES
        float aFlapAmount;
        aFlapAmount = m_FlapSpeed * Time.deltaTime;

        Vector3 aFlapVector;
        aFlapVector = aFlapAmount * Vector3.up;


        if (isFlappingLeft == true)
        {
            transform.Rotate(aFlapVector);
        }

        if (isFlappingLeft == false)
        {
            transform.Rotate(-aFlapVector);
        }

        /*
        //WING COLOR CHANGE
        //FAIRY TYPE DETERMINES...
        if (ownerOfTheseWings.m_FairyIsRed == true)
        {
            m_FairyWingRenderer.sharedMaterial = m_FairyWingMaterial[0];
        }
        if (ownerOfTheseWings.m_FairyIsGreen == true)
        {
            m_FairyWingRenderer.sharedMaterial = m_FairyWingMaterial[1];
        }
        if (ownerOfTheseWings.m_FairyIsBlue == true)
        {
            m_FairyWingRenderer.sharedMaterial = m_FairyWingMaterial[2];
        }
        if (ownerOfTheseWings.m_FairyIsYellow == true)
        {
            m_FairyWingRenderer.sharedMaterial = m_FairyWingMaterial[3];
        }
        */
    }
}
