using SQLite4Unity3d;

public class Highscore  {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string Name { get; set; }
	public int Score { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Highscore: Id={0}, Name={1},  Score={2}]", Id, Name, Score);
	}
}
