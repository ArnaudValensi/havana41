using UnityEngine;
using System.Collections.Generic;

public class Leaderboard : MonoBehaviour {

	public GameObject entryPrefab;

	List<GameObject> entriesList = new List<GameObject>();

	void Start () {
		List<Entry> entries = new List<Entry> {
			new Entry() { position = 1, playerName = "Ragnar", score = 12353499 },
			new Entry() { position = 1, playerName = "Floki", score = 987987 },
			new Entry() { position = 1, playerName = "Lagertha", score = 876876 },
			new Entry() { position = 1, playerName = "Rollo", score = 765765 },
			new Entry() { position = 1, playerName = "Siggy", score = 654654 },
			new Entry() { position = 1, playerName = "Athelstan", score = 543543 },
			new Entry() { position = 1, playerName = "Bjorn Ironside", score = 432432 },
			new Entry() { position = 1, playerName = "Kalf", score = 321321 },
			new Entry() { position = 1, playerName = "Harbard", score = 2345 },
			new Entry() { position = 1, playerName = "Ivar", score = 100 },
		};

		int i = 1;
		foreach (Entry entry in entries) {
			Debug.Log("entry: " + entry);
			AddEntry(entry, i);
			i++;
		}
	}

	void AddEntry(Entry entry, int index) {
		var entryGO = Instantiate(entryPrefab, transform);
		var leaderboardEntry = entryGO.GetComponent<LeaderboardEntry>();

		entriesList.Add(entryGO);
		leaderboardEntry.Init(index, entry.playerName, entry.score);
	}

	struct Entry {
		public int position;
		public string playerName;
		public int score;
	}

}
