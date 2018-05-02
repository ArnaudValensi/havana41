using UnityEngine;

public class GameScoreManager : MonoBehaviour {

	[ReadOnly][SerializeField] int score = 0;
	public int Score { get { return score; } } 

	public void SetScore(int score) {
		this.score = score;
	}

}
