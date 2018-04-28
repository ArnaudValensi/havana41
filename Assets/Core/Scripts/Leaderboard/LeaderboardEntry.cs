using UnityEngine;
using TMPro;

public class LeaderboardEntry : MonoBehaviour {

	[ReadOnly] int position;
	[ReadOnly] string playerName;
	[ReadOnly] int score;

	TextMeshProUGUI positionText;
	TextMeshProUGUI playerText;
	TextMeshProUGUI scoreText;

	public void Init(int position, string playerName, int score) {
		this.position = position;
		this.playerName = playerName;
		this.score = score;

		positionText = transform.Find("PositionText").GetComponent<TextMeshProUGUI>();
		playerText = transform.Find("PlayerText").GetComponent<TextMeshProUGUI>();
		scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();

		positionText.SetText("#" + position);
		playerText.SetText(playerName);
		scoreText.SetText(score.ToString());
	}

}
