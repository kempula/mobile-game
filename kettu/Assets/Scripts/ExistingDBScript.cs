using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ExistingDBScript : MonoBehaviour {

    public Text num1;
    public Text num2;
    public Text num3;
    public Text num4;
    public Text num5;

    private bool newHighScore = true;


    public Text score1;
    public Text score2;
    public Text score3;
    public Text score4;
    public Text score5;

    public Text signText;

    public playerScore playerScoreScript;



    // Use this for initialization
    void Start () {
        GetTop5Scores();
        GameObject scoreManager = GameObject.FindGameObjectWithTag("scoremanager");
        playerScoreScript = scoreManager.GetComponent<playerScore>();
        newHighScore = true;
    }
    // ADD HIGHSCORE
    public void AddHighscore(string name, int score)
    {
        var ds = new DataService("highscoreDB.db");
        ds.AddHighscore(name,score);
    }

    //DELETE ALL
    public void deleteAll()
    {
        var ds = new DataService("highscoreDB.db");
        ds.DeleteAllFromHighscore();
    }

    // 1. Haetaan Highscoret yksi kerrallaan
    public void GetTop5Scores()
    {
        GetOneHighscore1();
        GetOneHighscore2();
        GetOneHighscore3();
        GetOneHighscore4();
        GetOneHighscore5();
    }

    // 2. Luetaan tietokannasta saatu tieto
    private void ToConsole(IEnumerable<Highscore> highscore, int number){
		foreach (var scoreLine in highscore) {
			FindNameAndScore(scoreLine.ToString(), number);
        }
    }

    private void ToConsole(string msg)
    {
        Debug.Log(msg);
    }

    // 3. Etsitään tietokannasta saadusta tekstistä nimi ja pisteet
    public void FindNameAndScore(string line, int number)
    {
        var startTagName = "Name=";
        int startIndexName = line.IndexOf(startTagName) + startTagName.Length;
        int endIndexName = line.IndexOf(",", startIndexName);

        var startTagScore = "Score=";
        int startIndexScore = line.IndexOf(startTagScore) + startTagScore.Length;
        int endIndexScore = line.IndexOf("]", startIndexScore);

        WriteNameAndScore(line.Substring(startIndexName, endIndexName - startIndexName), line.Substring(startIndexScore, endIndexScore - startIndexScore), number);

    }

    // 4. Kirjoitetaan nimet ja pisteet tekstiruutuihin
    private void WriteNameAndScore(string name, string score, int number)
    {
        if (number == 1)
        {
            
            score1.text = score;
            signText.text = score;
            if (int.Parse(score) == playerScoreScript.GetScore() && newHighScore == true)
            {
                
                score1.color = Color.green;
                num1.color = Color.green;
                newHighScore = false;
            }

        }
        if (number == 2)
        {
            
            score2.text = score;
            if (int.Parse(score) == playerScoreScript.GetScore() && newHighScore == true)
            {
                
                score2.color = Color.green;
                num2.color = Color.green;
                newHighScore = false;
            }
        }
        if (number == 3)
        {
            
            score3.text = score;
            if (int.Parse(score) == playerScoreScript.GetScore() && newHighScore == true)
            {
               
                score3.color = Color.green;
                num3.color = Color.green;
                newHighScore = false;
            }
        }
        if (number == 4)
        {
            
            score4.text = score;
            if (int.Parse(score) == playerScoreScript.GetScore() && newHighScore == true)
            {
                
                score4.color = Color.green;
                num4.color = Color.green;
                newHighScore = false;
            }
        }
        if (number == 5)
        {
            
            score5.text = score;
            if (int.Parse(score) == playerScoreScript.GetScore() && newHighScore == true)
            {
                
                score5.color = Color.green;
                num5.color = Color.green;
                newHighScore = false;
            }

        }
    }


    public void GetOneHighscore1()
    {
        var ds = new DataService("highscoreDB.db");
        var oneHighscore = ds.GetOneHighscore1();

        ToConsole(oneHighscore, 1);
    }
    public void GetOneHighscore2()
    {
        var ds = new DataService("highscoreDB.db");
        var oneHighscore = ds.GetOneHighscore2();
        ToConsole(oneHighscore, 2);
    }
    public void GetOneHighscore3()
    {
        var ds = new DataService("highscoreDB.db");
        var oneHighscore = ds.GetOneHighscore3();
        ToConsole(oneHighscore, 3);
    }
    public void GetOneHighscore4()
    {
        var ds = new DataService("highscoreDB.db");
        var oneHighscore = ds.GetOneHighscore4();
        ToConsole(oneHighscore, 4);
    }
    public void GetOneHighscore5()
    {
        var ds = new DataService("highscoreDB.db");
        var oneHighscore = ds.GetOneHighscore5();
        ToConsole(oneHighscore, 5);
    }
}
