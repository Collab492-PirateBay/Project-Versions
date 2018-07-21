/* BALL FAIRY : COLOR & LIGHTS */
//only for the visuals and animations
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFairyInJar : MonoBehaviour
{
    //MATERIAL/COLOR CHANGE
    private Renderer m_FairyRenderer = null;
    public Material[] m_FairyInJarColorMaterial = null;


    [SerializeField] private GameObject m_JarObject;
    [SerializeField] private JarOrbit m_Jar;

    //LIGHTS
    [SerializeField] private Light FairyFrontLight = null;
    [SerializeField] private Light FairyBackLight = null;

    //LIGHTS COLOR
    private Color m_LightColor;

    //Use the below int's if you want to play around with the colors...
    //[SerializeField] private int m_LightModifierRed = 0;
    //[SerializeField] private int m_LightModifierGreen = 0;
    //[SerializeField] private int m_LightModifierBlue = 0;

    //...............................................................
    //............................................ * METHOD : START *

    protected void Start()
    {
        //Getting the color of the fairy
        m_JarObject = GameObject.FindWithTag("Jar");
        m_Jar = m_JarObject.GetComponent<JarOrbit>();

        m_FairyRenderer = GetComponent<Renderer>();
    }

    //...............................................................
    //.................................................... * UPDATE *
    void Update()
    {

        //COLOR CHANGE
        if (m_Jar.fairyType == "Red")
        {
            m_FairyRenderer.sharedMaterial = m_FairyInJarColorMaterial[0];

            //LIGHTS
            if ((FairyFrontLight != null) && (FairyBackLight != null))
            {
                Light frontLight = FairyFrontLight.GetComponent<Light>();
                Light backLight = FairyBackLight.GetComponent<Light>();

                m_LightColor.r = 90 / 100f;
                m_LightColor.g = 30 / 100f;
                m_LightColor.b = 85 / 100f;

                //alpha is between 0 (transparent) and 1 (opaque)
                m_LightColor.a = 1;

                frontLight.color = m_LightColor;
                backLight.color = m_LightColor;
            }
        }
        if (m_Jar.fairyType == "Green")
        {
            m_FairyRenderer.sharedMaterial = m_FairyInJarColorMaterial[1];

            //LIGHTS
            if ((FairyFrontLight != null) && (FairyBackLight != null))
            {
                Light frontLight = FairyFrontLight.GetComponent<Light>();
                Light backLight = FairyBackLight.GetComponent<Light>();

                m_LightColor.r = 55 / 100f;
                m_LightColor.g = 95 / 100f;
                m_LightColor.b = 45 / 100f;

                m_LightColor.a = 1;

                frontLight.color = m_LightColor;
                backLight.color = m_LightColor;
            }
        }
        if (m_Jar.fairyType == "Blue")
        {
            m_FairyRenderer.sharedMaterial = m_FairyInJarColorMaterial[2];

            //LIGHTS
            if ((FairyFrontLight != null) && (FairyBackLight != null))
            {
                Light frontLight = FairyFrontLight.GetComponent<Light>();
                Light backLight = FairyBackLight.GetComponent<Light>();

                m_LightColor.r = 30 / 100f;
                m_LightColor.g = 80 / 100f;
                m_LightColor.b = 90 / 100f;

                m_LightColor.a = 1;

                frontLight.color = m_LightColor;
                backLight.color = m_LightColor;
            }
        }
        if (m_Jar.fairyType == "Yellow")
        {
            m_FairyRenderer.sharedMaterial = m_FairyInJarColorMaterial[3];

            //LIGHTS
            if ((FairyFrontLight != null) && (FairyBackLight != null))
            {
                Light frontLight = FairyFrontLight.GetComponent<Light>();
                Light backLight = FairyBackLight.GetComponent<Light>();

                m_LightColor.r = 90 / 100f;
                m_LightColor.g = 95 / 100f;
                m_LightColor.b = 40 / 100f;

                m_LightColor.a = 1;

                frontLight.color = m_LightColor;
                backLight.color = m_LightColor;
            }
        }
    }

}
