using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour {

    //Start menun ikkunat
    public GameObject highscorePanel;
    public GameObject startMenuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    //Settings-ikkunan natturat
    public Toggle musicToggle;
    public Toggle helpToggle;

    //Audio
    private AudioSource audioSource;
    public AudioClip menuSelection;

    // Use this for initialization
    void Start () {

        //Ikkunat piiloon
        settingsPanel.SetActive(false);
        highscorePanel.SetActive(false);
        creditsPanel.SetActive(false);
        startMenuPanel.SetActive(true);

        //Haetaan AudioSource, johon määritetään klikkaus-clippi. Soitetaan aina, kun painetaan jotain natturaa.
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = menuSelection;

        //Jos musiikille on tehty määärittelyjä (Settingsien ON/OFF),
        //Asetetaan toggle-natturat siihen asentoon missä niiden kuuluu olla.

        string music = PlayerPrefs.GetString("music");

        if (music == "on")
        {
            musicToggle.isOn = true;
        }
        else if (music == "off")
        {
            musicToggle.isOn = false;

        }
        else
        {
            
            musicToggle.isOn = true;
        }

        //Sama juttu, kuin ylempänä
        string help = PlayerPrefs.GetString("help");
        if (help == "on")
        {
            helpToggle.isOn = true;
        }
        else if (help == "off")
        {
            helpToggle.isOn = false;
        }
        else
        {
            helpToggle.isOn = true;
        }

    }


    public void StartGame()
    {

        SceneManager.LoadScene("loadingGame");
        audioSource.Play();
    }

    public void OpenHighScores()
    {

        highscorePanel.SetActive(true);
        startMenuPanel.SetActive(false);
        audioSource.Play();
    }

    public void CloseHighScores()
    {
        highscorePanel.SetActive(false);
        startMenuPanel.SetActive(true);
        audioSource.Play();

    }

    public void OpenSetting()
    {
        settingsPanel.SetActive(true);
        startMenuPanel.SetActive(false);
        audioSource.Play();

    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        startMenuPanel.SetActive(true);
        audioSource.Play();
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        startMenuPanel.SetActive(false);
        audioSource.Play();
    }

    public void CloseCredits()
    {

        creditsPanel.SetActive(false);
        startMenuPanel.SetActive(true);
        audioSource.Play();

    }

    //TOGGLET

    //Musiikki voidaan asettaa päälle ja pois
    public void ToggleMusic()
    {
        audioSource.Play();

        if (musicToggle.isOn)
        {

            PlayerPrefs.SetString("music", "on");
            
            AudioListener.volume = 1;

        }
        else
        {
            PlayerPrefs.SetString("music", "off");
            AudioListener.volume = 0;
        }
        
    }

    // Muutellaan helpin arvoja ToggleHelp-nappulan mukaan
    public void ToggleHelp()
    {
        if (helpToggle.isOn)
        {
            PlayerPrefs.SetString("help", "on");
        }
        else
        {
            PlayerPrefs.SetString("help", "off");
        }
    }
}
