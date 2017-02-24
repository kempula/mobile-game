using UnityEngine;
using System.Collections;

public class platformDestroy : MonoBehaviour {

	public GameObject platformDestroyPoint;

	// Use this for initialization
	void Start () {
	
		platformDestroyPoint = GameObject.Find ("PlatformDestroyPoint");

	}
	
	// Update is called once per frame
	void Update () {

        // Tuhotaan taaksejääneet platformit
        if (transform.position.x < platformDestroyPoint.transform.position.x)
		{
			Destroy (gameObject);
		}

	}
}
