using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBanner : MonoBehaviour {

	public static ScoreBanner Instance;

	[SerializeField] TextMeshProUGUI _text;
	[SerializeField] AnimationCurve _rewardPerSpeed;
	[SerializeField] float _routineInterval = 1f;

	ArenaManager arenaManager;

	int _internalScore = 0;

	private void Awake() {
		Instance = this;
		_internalScore = 0;
		StartCoroutine(ScoreUpgrade());
	}

	void Start() {
		arenaManager = GameObject.Find("/Arena").GetComponent<ArenaManager>();
	}

	IEnumerator ScoreUpgrade() {

		while (true) {
			yield return new WaitForSeconds(_routineInterval);

			_internalScore += (int)_rewardPerSpeed.Evaluate(HavanaManager.Instance.GlobalTransitionInterval);
			arenaManager.SetScore(_internalScore);
			UpdateUI();
		}

	}

	void UpdateUI() {
		_text.text = _internalScore.ToString();

	}

	internal void AddScore(int v) {
		_internalScore += v;
		UpdateUI();
	}
}
