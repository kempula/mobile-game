using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

	public GameObject FREDDY;
    public float cameraMaxY;
    public float cameraMinY;

    private float relativeHeight;
    public float minHeight = 10f;
    public float maxHeight = 20f;

    public float minOrthoSize = 20;
    public float maxOrthoSize = 21;

    public GameObject cam;

    //Audio
    private AudioSource audioSource;
    public AudioClip backgroundMusic; //taustamusiikki

	// Use this for initialization
	void Start () {


        //Tarkistetaan onko musiikki päällä.
        // Jos on, laitetaan AudioListenerin volumeksi 1.
        // Jos ei, laitetaan 0.
        string music = PlayerPrefs.GetString("music");

        if (music == "on")
        {
            AudioListener.volume = 1;
        } else if (music == "off")
        {
            AudioListener.volume = 0;

        } else
        {
            AudioListener.volume = 1;
        }
        

        

        //Taustamusiikki
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.volume = 0.50f;
        audioSource.Play();

    }
	
	// Update is called once per frame
	void Update () {

        // Vaihdetaan kameran zoomausta ketun korkeuden mukaan
        relativeHeight = (transform.position.y - minHeight) / (maxHeight - minHeight);
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(minOrthoSize, maxOrthoSize, relativeHeight);

        // Pidetään kettu oikeassa kohtaa kameraa
        cam.transform.position =  new Vector3 (FREDDY.transform.position.x + 20, FREDDY.transform.position.y, -10);

        // Ei päästetä kameraa nousemaan backgroundin yläpuolelle
        if (cam.transform.position.y > cameraMaxY)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cameraMaxY, cam.transform.position.z);
        }

        // Ei päästetä kameraa tippumaan ketun mukana backgroundin alapuolelle
        if (cam.transform.position.y < cameraMinY)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cameraMinY, cam.transform.position.z);
        }


    }
    }
