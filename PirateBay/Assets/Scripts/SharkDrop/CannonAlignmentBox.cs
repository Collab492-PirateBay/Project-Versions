/* CANNON ALIGNMENT BOX */
/* BLAKE CURIA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAlignmentBox : MonoBehaviour 
{
    //........................................................
    // VARIABLES

    public bool m_IsReadyingFire = false;
    public bool m_IsFiring = false;

    [SerializeField] private float m_FiringCooldownTimer;
    [SerializeField] private float m_FiringCooldownDur = 0.0f;

    public BundleOfCoins m_CoinBundlePrefab;
    [SerializeField] private float m_SpawnPosition;

    [SerializeField] public GameObject confettiPrefab;

    //[SerializeField] private GameObject fx_CannonFire;
    //[SerializeField] private Light fx_CannonLight;

    //........................................................
    // RELATIVE SCRIPTS
    public CannonMovement m_Cannon;

    //............................................................
    //.................................................. * START *
    void Start()
    {
        GameObject cannonObject = GameObject.FindGameObjectWithTag("Cannon");
        m_Cannon = cannonObject.GetComponent<CannonMovement>();

        //fx_CannonFire.gameObject.SetActive(false);
        //fx_CannonLight.gameObject.SetActive(false);
    }

    //............................................................
    //................................................. * UPDATE *
    void Update()
    {
        m_FiringCooldownTimer -= Time.deltaTime;

        if (m_FiringCooldownTimer <= 0)
        {
            m_FiringCooldownTimer = 0;
        }

        if (m_FiringCooldownTimer > 0)
        {
            m_IsFiring = false;
        }
 

    }

    //............................................................
    //............................................... * TRIGGERS *

    private void OnTriggerEnter(Collider colliderBox)
    {
        GameObject colliderObject;
        colliderObject = colliderBox.gameObject;

        BalloonedShark sharkObject;
        sharkObject = colliderObject.GetComponent<BalloonedShark>();

        BalloonedChest treasureChestObject;
        treasureChestObject = colliderObject.GetComponent<BalloonedChest>();

        if (sharkObject != null)
        {
            if (m_FiringCooldownTimer <= 0)
            {
                m_IsFiring = true;

                m_Cannon.m_PlayerControlsAreActive = false;
                m_Cannon.FireCannon();

                //make prefab particle fx smoke

                GameObject confettiParticleFX;
                confettiParticleFX = Instantiate(confettiPrefab);
                confettiParticleFX.transform.position = new Vector3(sharkObject.transform.position.x, sharkObject.transform.position.y + m_SpawnPosition, sharkObject.transform.position.z);


                sharkObject.m_IsGoingUp = false;
                sharkObject.m_HangTimer = sharkObject.m_HangDur;

                m_FiringCooldownTimer = m_FiringCooldownDur;
            }
        }

        if (treasureChestObject != null)
        {
            if (m_FiringCooldownTimer <= 0)
            {
                m_IsFiring = true;

                m_Cannon.m_PlayerControlsAreActive = false;
                m_Cannon.FireCannon();

                //CannonFireAndSmoke();

                GameObject confettiParticleFX;
                confettiParticleFX = Instantiate(confettiPrefab);
                confettiParticleFX.transform.position = new Vector3(treasureChestObject.transform.position.x, treasureChestObject.transform.position.y + m_SpawnPosition, treasureChestObject.transform.position.z);

                BundleOfCoins coinBundleObject;
                coinBundleObject = Instantiate(m_CoinBundlePrefab) as BundleOfCoins;
                coinBundleObject.transform.position = new Vector3(treasureChestObject.transform.position.x, treasureChestObject.transform.position.y + m_SpawnPosition, treasureChestObject.transform.position.z);

                treasureChestObject.m_IsGoingUp = false;
                treasureChestObject.m_HangTimer = treasureChestObject.m_HangDur;

                m_FiringCooldownTimer = m_FiringCooldownDur;
            }
        }
    }

    //IEnumerator CannonFireAndSmoke()
    //{
    //    fx_CannonFire.gameObject.SetActive(true);
    //    fx_CannonLight.gameObject.SetActive(true);
    //    yield return new WaitForSeconds(1.5f);
    //    fx_CannonFire.gameObject.SetActive(false);
    //    fx_CannonLight.gameObject.SetActive(false);
    //}
}
