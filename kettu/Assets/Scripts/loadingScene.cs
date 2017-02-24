using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadingScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
    
        //Ladataan peli.
        StartCoroutine("WaitLoad");
       

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator WaitLoad()
    {
        
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync("game");
    }
}
