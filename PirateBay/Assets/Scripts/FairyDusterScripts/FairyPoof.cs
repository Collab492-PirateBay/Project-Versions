using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FairyPoof : MonoBehaviour, ITrackableEventHandler {

    [SerializeField]
    private float waitTime;
    [SerializeField]
    private GameObject smoke,
                       explosion;
    public GameObject fairy_queen;
    [SerializeField]
    private AudioClip[] fairysounds;
    private AudioSource playSound;
    private TrackableBehaviour mTrackableBehaviour;

    // Use this for initialization
    void Start () {
        playSound = GetComponent<AudioSource>();
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
    public void OnTrackableStateChanged(
                                   TrackableBehaviour.Status previousStatus,
                                   TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // Play audio when target is found
            playSound.Play();
        }
        else
        {
            // Stop audio when target is lost
            playSound.Stop();
        }
    }
    // Update is called once per frame
    void Update () {
        //transform.rotation = new Quaternion(0,0,0,0);
        //fairy_queen.GetComponent<Animator>().SetFloat
	}

    public IEnumerator fairyScreech()
    {
        int radNumb = Random.Range(0, fairysounds.Length);
        playSound.PlayOneShot(fairysounds[radNumb]);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("fairyScreech");
    }

    public IEnumerator explodeBody()
    {
        var smoker  = Instantiate(smoke, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z-0.5f), Quaternion.identity);
        smoker.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(waitTime);
        var exploder = Instantiate(explosion, transform.position, Quaternion.identity);
        fairy_queen.SetActive(false);
        exploder.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
