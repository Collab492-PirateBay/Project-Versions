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
        transform.rotation = new Quaternion(0,0,0,0);
	}

    public IEnumerator explodeBody()
    {
        var smoker  = Instantiate(smoke, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z-0.5f), Quaternion.identity);
        smoker.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(waitTime);
        var exploder = Instantiate(explosion, transform.position, Quaternion.identity);
        exploder.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
