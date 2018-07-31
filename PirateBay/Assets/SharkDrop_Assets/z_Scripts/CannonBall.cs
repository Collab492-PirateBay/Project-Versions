/* CANNON BALL */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour 
{
    //........................................................
    // VARIABLES

    private Rigidbody m_RigidBody;

    [SerializeField] private float m_Speed;
    [SerializeField] private float m_LaunchSpeed;
    [SerializeField] private float m_DecliningSpeed;

    private float m_BeforeArchPeakTimer;
    [SerializeField] private float m_BeforeArchPeakDuration = 0.0f;

    public Vector3 m_CannonBallDir = Vector3.forward;

    private float m_BallLifeTimer;
    [SerializeField] private float m_BallLifeDuration = 0.0f;

    //EXPLODES ON CONTACT
    //[SerializeField] private Explosion m_BallExplosionPrefab;
    //[SerializeField] private int m_CannonballDamage = 0;

    //............................................................
    //.................................................. * START *
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_BallLifeTimer = m_BallLifeDuration;
        m_BeforeArchPeakTimer = m_BeforeArchPeakDuration;
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {
        Vector3 a_Velocity;
        a_Velocity = m_Speed * m_CannonBallDir;

        m_BeforeArchPeakTimer -= Time.deltaTime;

        if (m_BeforeArchPeakTimer <= 0)
        {
            m_BeforeArchPeakTimer = 0;
            m_Speed = m_DecliningSpeed;
            //m_CannonBallDir = (Vector3.down + Vector3.forward);
        }
        else if (m_BeforeArchPeakTimer > 0)
        {
            m_Speed = m_LaunchSpeed;
            //m_CannonBallDir = Vector3.forward;
        }


        // LIFESPAN
        m_BallLifeTimer -= Time.deltaTime;

        if (m_BallLifeTimer <= 0)
        {
            m_BallLifeTimer = 0;
            Destroy(gameObject);
        }

        m_RigidBody.velocity = a_Velocity;
    }

    ////............................................................
    ////............................................... * TRIGGERS *

    //protected void OnTriggerEnter(Collider aCollider)
    //{
    //    GameObject aCollisionObject;
    //    aCollisionObject = aCollider.gameObject;

    //    //Health aVulnerableBody;
    //    //aVulnerableBody = aCollisionObject.GetComponent<Health>();

    //    //if (aVulnerableBody != null)
    //    //{
    //    //    Explosion aExplosionObject;
    //    //    aExplosionObject = Instantiate(m_BallExplosionPrefab) as Explosion;
    //    //    aExplosionObject.transform.position = transform.position;

    //    //    //take some damage from contact with the cannon, along with damage from the explosion
    //    //    aVulnerableBody.OnHurt(m_CannonballDamage);
    //    //    Destroy(gameObject);
    //    //}

    //}

}