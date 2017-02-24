using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	private SQLiteConnection _connection;

	public DataService(string DatabaseName){

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);     

	}

	public void CreateDB(){
		_connection.DropTable<Highscore> ();
		_connection.CreateTable<Highscore> ();

		_connection.InsertAll (new[]{
			new Highscore{
				Id = 1,
				Name = "Tom",
				Score = 10
			},
			new Highscore{
				Id = 2,
				Name = "Fred",
                Score = 20
            },
			new Highscore{
				Id = 3,
				Name = "John",
                Score = 30
            },
			new Highscore{
				Id = 4,
				Name = "Roberto",
                Score = 40
            }
		});
	}

	

	public IEnumerable<Highscore> GetPersonsNamedRoberto(){
		return _connection.Table<Highscore>().Where(x => x.Name == "Roberto");
	}

	public Highscore GetJohnny(){
		return _connection.Table<Highscore>().Where(x => x.Name == "Johnny").FirstOrDefault();
	}
	
    public IEnumerable<Highscore> GetHighscore(){
		    return _connection.Table<Highscore> ();
    }

    //public IEnumerable<Highscore> GetHighscoreOrderByScoreTop5()
    //{
    //    return _connection.Table<Highscore>().OrderByDescending(x => x.Score).Take(5);
    //}

    // ADD HIGHSCORE
    public Highscore AddHighscore(string name, int score){
		var p = new Highscore{
				Name = name,
                Score = score
        };
		_connection.Insert (p);
        Debug.Log("Database Write Success");
		return p;
	}
	
    // DELETE ALL
    public void DeleteAllFromHighscore()
    {
        _connection.DeleteAll<Highscore>();
    }


    // GET TOP 5 HIGHSCORES ONE BY ONE
    public IEnumerable<Highscore> GetOneHighscore1()
    {
        return _connection.Table<Highscore>().OrderByDescending(x => x.Score).Skip(0).Take(1);
    }
    public IEnumerable<Highscore> GetOneHighscore2()
    {
        return _connection.Table<Highscore>().OrderByDescending(x => x.Score).Skip(1).Take(1);
    }
    public IEnumerable<Highscore> GetOneHighscore3()
    {
        return _connection.Table<Highscore>().OrderByDescending(x => x.Score).Skip(2).Take(1);
    }
    public IEnumerable<Highscore> GetOneHighscore4()
    {
        return _connection.Table<Highscore>().OrderByDescending(x => x.Score).Skip(3).Take(1);
    }
    public IEnumerable<Highscore> GetOneHighscore5()
    {
        return _connection.Table<Highscore>().OrderByDescending(x => x.Score).Skip(4).Take(1);
    }



}