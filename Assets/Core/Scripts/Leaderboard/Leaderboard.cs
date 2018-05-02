using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Leaderboard : MonoBehaviour {

	public GameObject entryPrefab;
	public GameScoreManager scoreManager;

	List<GameObject> entriesList = new List<GameObject>();

	void OnEnable() {
		Entries entries = LoadScoresFromFile();
		Entry newEntry = new Entry() { playerName = "You", score = scoreManager.Score };

		entries.Add(newEntry);
		entries.Sort();
		SaveScoreToFile(entries);

		Clear();

		int i = 1;
		foreach (Entry entry in entries.GetTopEntries()) {
			AddEntry(entry, i);
			i++;
		}

		int rank = entries.GetEntryRank(newEntry);

		if (rank <= 10) {
			entriesList[rank].GetComponent<LeaderboardEntry>().Highlight();
		} else {
			LeaderboardEntry leaderboardEntry = AddEntry(newEntry, rank);
			leaderboardEntry.Highlight();
		}
	}

	void Clear() {
		foreach (var entrieGo in entriesList) {
			Destroy(entrieGo);
		}

		entriesList = new List<GameObject>();
	}

	void AddNewScore(Entries entries, int score) {
		Entry newEntry = new Entry() { playerName = "You", score = score };

		entries.entries.Add(newEntry);
	}

	LeaderboardEntry AddEntry(Entry entry, int index) {
		var entryGO = Instantiate(entryPrefab, transform);
		var leaderboardEntry = entryGO.GetComponent<LeaderboardEntry>();

		entriesList.Add(entryGO);
		leaderboardEntry.Init(index, entry.playerName, entry.score);

		return leaderboardEntry;
	}

	Entries LoadScoresFromFile() {
		string savePath = GetFilePath();
		string jsonData = null;

		Debug.Log("savePath: " + savePath);

		if (File.Exists(savePath)) {
			jsonData = File.ReadAllText(savePath);
		}

		return Entries.CreateFromJSON(jsonData);
	}

	void SaveScoreToFile(Entries entries) {
		string savePath = GetFilePath();
		string jsonData = entries.SaveToString();

		File.WriteAllText(savePath, jsonData);
	}

	string GetFilePath() {
		string folderath = GameConfig.Instance.GetSavePath();
		string filePath = folderath + "/leaderboard.json";

		if (!Directory.Exists(folderath)) {
			Directory.CreateDirectory(folderath);
		}

		return filePath;
	}

	[System.Serializable]
	class Entries {
		public List<Entry> entries;

		public void Add(Entry entry) {
			entries.Add(entry);
		}

		public string SaveToString() {
			return JsonUtility.ToJson(this, true);
		}

		public static Entries CreateFromJSON(string jsonString) {
			if (string.IsNullOrEmpty(jsonString)) {
				Entries entries = new Entries();
				entries.entries = new List<Entry>();
				return entries;
			}
			return JsonUtility.FromJson<Entries>(jsonString);
		}

		public void Sort() {
			entries = entries
				.OrderByDescending(entry => entry.score)
				.ToList();
		}

		public IEnumerable<Entry> GetTopEntries() {
			return entries.Take(10);
		}

		public int GetEntryRank(Entry entry) {
			return entries.FindIndex(currentEntry => currentEntry == entry);
		}
	}

	[System.Serializable]
	class Entry {
		public string playerName;
		public int score;
	}

}
