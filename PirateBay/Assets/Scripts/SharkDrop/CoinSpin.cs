/* COIN SPIN */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpin : MonoBehaviour 
{
    [SerializeField] private float m_SpinRate = 0.0f;


    //............................................................
    //.................................................. * UPDATE *

    void Update()
    {
        // SPINNING
        float a_SpinAmount;
        a_SpinAmount = m_SpinRate * Time.deltaTime;

        Vector3 a_SpinVector;
        a_SpinVector = a_SpinAmount * Vector3.up;

        transform.Rotate(a_SpinVector);
    }
}
