using UnityEngine;

public class ArenaManager : MonoBehaviour {

	public Animator animator;

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
	}

}
