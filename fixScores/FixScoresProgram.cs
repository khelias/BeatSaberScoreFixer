using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using fixScores.models;
using Newtonsoft.Json;
using static System.IO.File;

namespace fixScores
{
	class Program
	{
		private static readonly string BeatSaverPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\LocalLow\Hyperbolic Magnetism\Beat Saber\";
		public static Dictionary<string, BeatSaverSong> BeatSaverSongs;

		private static void Main(string[] args)
		{
			ImportBeatSaverSongsFromApi();
			var convertedLeaderBoard = ConvertLeaderBoard();
			MakeCopyOfExistingLeaderBoard();
			SaveNewLeaderBoard(convertedLeaderBoard);
		}

		private static void SaveNewLeaderBoard(PartyLeaderBoards partyLeaderBoards)
		{
			WriteAllText(BeatSaverPath + "LocalLeaderboards.dat", JsonConvert.SerializeObject(partyLeaderBoards));
		}

		private static void MakeCopyOfExistingLeaderBoard()
		{
			Move(BeatSaverPath + "LocalLeaderboards.dat", BeatSaverPath + "LocalLeaderboards_" + GetTimestamp(DateTime.Now) + ".dat");
		}

		public static string GetTimestamp(DateTime value)
		{
			return value.ToString("yyyyMMddHHmmssffff");
		}

		private static PartyLeaderBoards ConvertLeaderBoard()
		{
			var dictionary = new Dictionary<string, LeaderboardsData>();
			foreach (var data in GetLocalLeaderBoardData())
			{
				var leaderBoardId = data._leaderboardId;
				AddToDictionary(dictionary, leaderBoardId.Contains(" ") ? GetNewId(leaderBoardId) : leaderBoardId, data);
			}

			return new PartyLeaderBoards(dictionary.Values.ToList());
		}

		private static string GetNewId(string leaderBoardId)
		{
			var split = leaderBoardId.Split(" ");
			var key = split[0].Split("_")[^1];
			var hash = GetHashFromBeatSaverSongList(key);
			var newId = "custom_level_" + hash.ToUpper() + split[^1].Split(")")[^1];
			return newId;
		}

		private static void AddToDictionary(IDictionary<string, LeaderboardsData> dictionary, string leaderBoardId, LeaderboardsData data)
		{
			if (dictionary.TryGetValue(leaderBoardId, out var existingData))
			{
				existingData._scores.AddRange(data._scores);
			}
			else
			{
				dictionary.Add(leaderBoardId, new LeaderboardsData(leaderBoardId, data._scores));
			}
		}


		private static string GetHashFromBeatSaverSongList(string key)
		{
			return BeatSaverSongs.TryGetValue(key, out var beatSaverSong) ? beatSaverSong.hash : "DEFAULT_HASH";
		}

		public static List<LeaderboardsData> GetLocalLeaderBoardData()
		{
			using var reader = new StreamReader(BeatSaverPath + "LocalLeaderboards.dat");
			var json = reader.ReadToEnd();
			return JsonConvert.DeserializeObject<PartyLeaderBoards>(json)._leaderboardsData;
		}

		public static void ImportBeatSaverSongsFromApi()
		{
			BeatSaverSongs = new Dictionary<string, BeatSaverSong>();
			var request = (HttpWebRequest) WebRequest.Create(@"https://beatsaver.com/api/download/dump/maps");
			request.AutomaticDecompression = DecompressionMethods.GZip;
			var response = (HttpWebResponse) request.GetResponse();
			var stream = response.GetResponseStream();
			string fullListOfSongs;
			using var reader = new StreamReader(stream);
			{
				fullListOfSongs = reader.ReadToEnd();
			}
			foreach (var beatSaverSong in JsonConvert.DeserializeObject<List<BeatSaverSong>>(fullListOfSongs))
			{
				BeatSaverSongs.Add(beatSaverSong.key, beatSaverSong);
			}

		}
	}
}
