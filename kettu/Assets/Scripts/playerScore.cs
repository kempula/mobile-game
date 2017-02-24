using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerScore : MonoBehaviour
{

    public GameObject berryParticle;
    private int score = 0;
    int berryScoreBonus = 50;
    float scoreLimit = 250f;
    float scoreLimitAdd = 10f;
    public Text scoreText;
    public GameObject freddy;
    private Animation scoreFadeIn;


    // Use this for initialization
    void Start()
    {
        scoreText.enabled = false;

        //Jos Help on pois päältä, ei näytetä alkututoriaalia.
        string help = PlayerPrefs.GetString("help");
        if (help == "on")
        {
            StartCoroutine("WaitOnStart");
        }

        else if (help == "off")
        {
            StartCoroutine("WaitWithoutHelp");

        } else
        {
            StartCoroutine("WaitOnStart");

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (scoreText.isActiveAndEnabled) {

            // Lisätään pisteitä Freddyn etenemisen mukaan
            if (freddy.transform.position.x > scoreLimit)
            {
                score++;
                scoreLimit = scoreLimit + scoreLimitAdd;
                scoreText.text = "Score: " + score;
            }
        } else
        {
            
        }


    }

    // Lisätään Berrystä tulleet pisteet ja luodaan Berry Particle Effect
    public void AddBerryPoints(Vector2 berryPosition)
    {

        score = score + berryScoreBonus;
        scoreText.text = "Score: " + score.ToString();
        Instantiate(berryParticle, berryPosition, transform.rotation);


    }

    IEnumerator WaitOnStart()
    {
        yield return new WaitForSeconds(8.3f);

        scoreFadeIn = scoreText.GetComponent<Animation>();
        scoreFadeIn.Play("scoreFadeIn");
        scoreText.enabled = true;

        scoreText.text = "Score: " + score;
        score = 0;
    }

    IEnumerator WaitWithoutHelp()
    {
        yield return new WaitForSeconds(3f);

        scoreFadeIn = scoreText.GetComponent<Animation>();
        scoreFadeIn.Play("scoreFadeIn");
        scoreText.text = "Score: " + score;
        score = 0;
        scoreText.enabled = true;

        
    }

    // Lähetetään score
    public int GetScore()
    {
        return score;
    }

}
