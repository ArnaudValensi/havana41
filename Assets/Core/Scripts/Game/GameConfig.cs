using UnityEngine;

public class GameConfig : MonoBehaviourSingleton<GameConfig> {

	public string saveFolder = "blocksnblasters";

	public string GetSavePath() {
		return Application.persistentDataPath + "/" + saveFolder;
	}

}
