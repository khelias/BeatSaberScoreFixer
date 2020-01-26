using System;
using System.Collections.Generic;

namespace fixScores.models
{
	public class Score
	{
		public int _score { get; set; }
		public string _playerName { get; set; }
		public bool _fullCombo { get; set; }
		public int _timestamp { get; set; }
	}

	public class PartyLeaderBoards
	{
		public PartyLeaderBoards(List<LeaderboardsData> leaderboardsData)
		{
			_leaderboardsData = leaderboardsData;
		}

		public List<LeaderboardsData> _leaderboardsData { get; set; }
	}

	public class LeaderboardsData
	{
		public LeaderboardsData(string leaderboardId, List<Score> scores)
		{
			_leaderboardId = leaderboardId;
			_scores = scores;
		}

		public string _leaderboardId { get; set; }
		public List<Score> _scores { get; set; }

	}

	public class Difficulties
	{
		public bool easy { get; set; }
		public bool expert { get; set; }
		public bool expertPlus { get; set; }
		public bool hard { get; set; }
		public bool normal { get; set; }
	}

	public class Easy
	{
		public string duration { get; set; }
		public string length { get; set; }
		public string njs { get; set; }
		public string njsOffset { get; set; }
		public string bombs { get; set; }
		public string notes { get; set; }
		public string obstacles { get; set; }
	}

	public class Expert
	{
		public string duration { get; set; }
		public string length { get; set; }
		public string njs { get; set; }
		public string njsOffset { get; set; }
		public string bombs { get; set; }
		public string notes { get; set; }
		public string obstacles { get; set; }
	}

	public class ExpertPlus
	{
		public string duration { get; set; }
		public string length { get; set; }
		public string njs { get; set; }
		public string njsOffset { get; set; }
		public string bombs { get; set; }
		public string notes { get; set; }
		public string obstacles { get; set; }
	}

	public class Hard
	{
		public string duration { get; set; }
		public string length { get; set; }
		public string njs { get; set; }
		public string njsOffset { get; set; }
		public string bombs { get; set; }
		public string notes { get; set; }
		public string obstacles { get; set; }
	}

	public class Normal
	{
		public string duration { get; set; }
		public string length { get; set; }
		public string njs { get; set; }
		public string njsOffset { get; set; }
		public string bombs { get; set; }
		public string notes { get; set; }
		public string obstacles { get; set; }
	}

	public class Difficulties2
	{
		public Easy easy { get; set; }
		public Expert expert { get; set; }
		public ExpertPlus expertPlus { get; set; }
		public Hard hard { get; set; }
		public Normal normal { get; set; }
	}

	public class Characteristic
	{
		public Difficulties2 difficulties { get; set; }
		public string name { get; set; }
	}

	public class Metadata
	{
		public Difficulties difficulties { get; set; }
		public int duration { get; set; }
		public List<Characteristic> characteristics { get; set; }
		public string levelAuthorName { get; set; }
		public string songAuthorName { get; set; }
		public string songName { get; set; }
		public string songSubName { get; set; }
		public string bpm { get; set; }
	}

	public class Stats
	{
		public int downloads { get; set; }
		public int plays { get; set; }
		public int downVotes { get; set; }
		public int upVotes { get; set; }
		public double heat { get; set; }
		public double rating { get; set; }
	}

	public class Uploader
	{
		public string _id { get; set; }
		public string username { get; set; }
	}

	public class BeatSaverSong
	{
		public Metadata metadata { get; set; }
		public Stats stats { get; set; }
		public string description { get; set; }
		public DateTime? deletedAt { get; set; }
		public string _id { get; set; }
		public string key { get; set; }
		public string name { get; set; }
		public Uploader uploader { get; set; }
		public string hash { get; set; }
		public DateTime uploaded { get; set; }
		public string directDownload { get; set; }
		public string downloadURL { get; set; }
		public string coverURL { get; set; }
	}
}
