/* WING FLUTTER WHILE IN JAR */
/* ALSO WING COLOR */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsInJar : MonoBehaviour 
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
    public Material[] m_FairyInJarWingMaterial = null;

    [SerializeField] private GameObject m_JarObject;
    [SerializeField] private JarOrbit m_Jar;

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
        m_JarObject = GameObject.FindWithTag("Jar");
        m_Jar = m_JarObject.GetComponent<JarOrbit>();

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


        //WING COLOR CHANGE
        if (m_Jar.fairyType == "Red")
        {
            m_FairyWingRenderer.sharedMaterial = m_FairyInJarWingMaterial[0];
        }
        if (m_Jar.fairyType == "Green")
        {
            m_FairyWingRenderer.sharedMaterial = m_FairyInJarWingMaterial[1];
        }
        if (m_Jar.fairyType == "Blue")
        {
            m_FairyWingRenderer.sharedMaterial = m_FairyInJarWingMaterial[2];
        }
        if (m_Jar.fairyType == "Yellow")
        {
            m_FairyWingRenderer.sharedMaterial = m_FairyInJarWingMaterial[3];
        }

    }
}
