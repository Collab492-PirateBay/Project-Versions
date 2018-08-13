/* BUNDLE OF COINS */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleOfCoins : MonoBehaviour
{
    private float m_HangTimer;
    [SerializeField] float m_HangDuration = 0.0f;

    [SerializeField] private float m_FlightSpeed = 0.0f;

    [SerializeField] private float m_CloseUpSpeed = 0.0f;
    [SerializeField] private float m_CloseUpPositionX = -615.0f;
    [SerializeField] private float m_CloseUpPositionY = 17.0f;

    [SerializeField] private float m_Cushion = 0.3f;

    //............................................................
    //.................................................. * START *

    protected void Start()
    {
        m_HangTimer = m_HangDuration;
    }

    //............................................................
    //.................................................. * UPDATE *

    void Update()
    {
        m_HangTimer -= Time.deltaTime;

        if (m_HangTimer > 0)
        {
            Vector3 closeUpLocation = new Vector3(m_CloseUpPositionX, m_CloseUpPositionY, this.transform.position.z);

            if (Vector3.Distance(transform.position, closeUpLocation) <= m_Cushion)
            {
                //print(Vector3.Distance(transform.position, closeUpLocation));
                transform.Translate((m_CloseUpSpeed * Time.deltaTime), 0, 0);
            }
        }


        if (m_HangTimer <= 0)
        {
            m_HangTimer = 0;

            transform.Translate(0, m_FlightSpeed * Time.deltaTime, 0);
        }
    }
}