using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadingStartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {

        // Avataan startMenu-scene.

        StartCoroutine("WaitLoad");
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator WaitLoad()
    {

        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync("startMenu");
    }

}
