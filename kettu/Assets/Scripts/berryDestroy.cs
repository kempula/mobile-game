using UnityEngine;
using System.Collections;

public class berryDestroy : MonoBehaviour {

    public GameObject platformDestroyPoint;


    // Use this for initialization
    void Start () {
        platformDestroyPoint = GameObject.Find("PlatformDestroyPoint");
    }
	
	// Update is called once per frame
	void Update () {

        //BERRY
        // Tuhotaan taaksejääneet berryt
        if (transform.position.x < platformDestroyPoint.transform.position.x)
        {
            Destroy(gameObject);
        }


    }
}
