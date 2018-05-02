using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Leaderboard : MonoBehaviour {

	public GameObject entryPrefab;
	public GameScoreManager scoreManager;

	List<GameObject> entriesList = new List<GameObject>();

	void OnEnable() {
		Debug.Log("OnEnable");

		Entries entries = LoadScoresFromFile();

		AddNewScore(entries, scoreManager.Score);
		entries.Sort();

		Clear();

		int i = 1;
		foreach (Entry entry in entries.GetTopEntries()) {
			Debug.Log("entry: " + entry);
			AddEntry(entry, i);
			i++;
		}

		SaveScoreToFile(entries);
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

	void AddEntry(Entry entry, int index) {
		var entryGO = Instantiate(entryPrefab, transform);
		var leaderboardEntry = entryGO.GetComponent<LeaderboardEntry>();

		entriesList.Add(entryGO);
		leaderboardEntry.Init(index, entry.playerName, entry.score);
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
	struct Entries {
		public List<Entry> entries;

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
	}

	[System.Serializable]
	struct Entry {
		public string playerName;
		public int score;
	}

}
