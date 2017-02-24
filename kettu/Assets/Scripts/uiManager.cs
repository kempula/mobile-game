using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{

    //UI-Elementit
    public GameObject controlls;
    public GameObject pauseMenu;
    public GameObject endMenu;
    public GameObject hud;
    public GameObject pauseButton;
    public GameObject freddy;
    public GameObject go;

    //Tekstikentät scoreille
    public Text scoreText;
    public Text endScoreText;

    //Animaatiot (helppi alussa, jos päällä + GO!)
    private Animation controllsFadeOutAnim;
    private Animation goAnim;

    //Scriptit, joita tarvitaan
    private playerScore playerScoreScript;
    private ExistingDBScript dbScript;

    //Audiot
    private AudioSource audioSource;
    public AudioClip menuSelection;

    // Use this for initialization
    void Start()
    {
        go.SetActive(false); //Go on false, ettei se näkyisi ennen aikojaan.

        //Ei pauseteta, eli timeScale pitää olla 1.
        Time.timeScale = 1f;

        //Ikkunat piiloon.
        pauseMenu.SetActive(false);
        endMenu.SetActive(false);
        hud.SetActive(true);

        //Scoren teksti on false, ettei se näkyisi alkututoriaalin takana.
        scoreText.enabled = false;


        //Jos helppi on päällä, näytetään alkututoriaali normaalisti.
        //Jos ei, näytetään vain go.
        string help = PlayerPrefs.GetString("help");

        if (help == "on")
        {
            StartCoroutine("HideControlls");
        }

        else if (help == "off")
        {
            // Piilotetaan controllit
            controlls.SetActive(false);

            //Näytetään vain Go!
            StartCoroutine("WaitGo");

            // Siirretään freddyä eteenpäin, niin pääsee heti pelaamaan
            freddy.transform.position = new Vector2(freddy.transform.position.x + 210, freddy.transform.position.y);
        }
        else
        {
            StartCoroutine("HideControlls");
        }


        //Haetaan audioSource, jotta saadaan klikkausäänet kuulumaan.
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = menuSelection;

        //Haetaan playerScore-scripti
        GameObject scoreManager = GameObject.FindGameObjectWithTag("scoremanager");
        playerScoreScript = scoreManager.GetComponent<playerScore>();

        //Haetaan DB-scripti
        GameObject highscoreManager = GameObject.FindGameObjectWithTag("highscoremanager");
        dbScript = highscoreManager.GetComponent<ExistingDBScript>();
    }


    //Käytetään animaatiota pelin alkaessa kontrollien piilottamiseen.
    //Näytetään myös GO!

    IEnumerator HideControlls()
    {

        yield return new WaitForSeconds(5);

        controllsFadeOutAnim = controlls.GetComponent<Animation>();
        controllsFadeOutAnim.Play("fadeOut");

        yield return new WaitForSeconds(1.5f);

        go.SetActive(true);
        goAnim = go.GetComponent<Animation>();

        goAnim.Play("goAnimation");



    }


    //Jos helppi ei ole päällä, näytetään vain GO!
    IEnumerator WaitGo()
    {

        yield return new WaitForSeconds(1.5f);
        go.SetActive(true);
        goAnim = go.GetComponent<Animation>();

        goAnim.Play("goAnimation");


    }

    public void EndGame()
    {

        endMenu.SetActive(true);
        controlls.SetActive(false);
        hud.SetActive(false);
        endScoreText.text = "Your Score: " + playerScoreScript.GetScore();
        Time.timeScale = 0f; //Pysäytetään peli
        dbScript.GetTop5Scores(); // Haetaan highscoret db:stä

    }

    public void RestartGame()
    {   
        // Aloitetaan peli alusta
        Time.timeScale = 1f; 
        endMenu.SetActive(false);
        pauseMenu.SetActive(false);
        audioSource.Play(); // Klikkausääni
        SceneManager.LoadScene("game"); 
    }

    public void OpenStartMenu()
    {

        //Mennään takaisin startmenuun
        Time.timeScale = 1f;
        audioSource.Play();
        SceneManager.LoadSceneAsync("startMenu");

    }

    public void PauseGame()
    {
        //Pausetetaan peli
        audioSource.Play();
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);


    }

    public void ResumeGame()
    {
        // Jatketaan peliä
        Time.timeScale = 1f;
        audioSource.Play();
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);

    }
}
