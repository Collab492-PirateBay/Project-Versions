using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyPoof : MonoBehaviour {

    [SerializeField]
    private float waitTime;
    [SerializeField]
    private GameObject smoke,
                       explosion;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator explodeBody()
    {
        Instantiate(smoke, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z-0.5f), Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
