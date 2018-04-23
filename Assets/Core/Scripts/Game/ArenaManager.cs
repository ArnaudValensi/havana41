using UnityEngine;
using TMPro;

public class ArenaManager : MonoBehaviour {

	public Animator animator;
	public TextMeshProUGUI gameOverScore;
	public TextMeshProUGUI pauseScore;

	bool isPaused;

	void Start() {
		animator = GetComponent<Animator>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (!isPaused) {
				Pause();
			} else {
				UnPause();
			}
		}
	}

	public void GameOverToTitle() {
		Debug.Log("GameOverToTitle");

		animator.SetInteger("InternalState", 0);
	}

	public void Pause() {
		Debug.Log("Pause");

		isPaused = true;
		Time.timeScale = 0f;
		animator.SetBool("IsPaused", true);
	}

	public void UnPause() {
		Debug.Log("UnPause");

		isPaused = false;
		Time.timeScale = 1f;
		animator.SetBool("IsPaused", false);
	}

	public void PauseToGameOver() {
		Debug.Log("PauseToGameOver");
		UnPause();
		animator.SetInteger("InternalState", 2);
		Managers.Game.SetState(typeof(GameOverState));
	}

	public void QuitGame() {
		// In most cases termination of application under iOS should be left at the user discretion.
		// https://developer.apple.com/library/content/qa/qa1561/_index.html
		#if !UNITY_IPHONE
			Application.Quit(); // Do not work in editor mode.
		#endif
	}

	public void SetScore(int score) {
		gameOverScore.SetText(score.ToString());
		pauseScore.SetText(score.ToString());
	}

}
