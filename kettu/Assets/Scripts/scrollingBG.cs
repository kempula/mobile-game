using UnityEngine;
using System.Collections;

public class scrollingBG : MonoBehaviour {

    public float speed;
    public float backgroundY;

	
	// Update is called once per frame
	void Update () {
	
		Vector2 offset = new Vector2 (Time.time * speed, 0);

		GetComponent<Renderer>().material.mainTextureOffset = offset;
        GetComponent<Renderer>().transform.position = new Vector3(GetComponent<Renderer>().transform.position.x, backgroundY,20);

    }

}
