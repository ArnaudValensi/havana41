using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBanner : MonoBehaviour {

    public static ScoreBanner Instance;

    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _bestScoreText;
    [SerializeField] AnimationCurve _rewardPerSpeed;

    [SerializeField] bool _useShapeTick;
    [SerializeField] float _routineInterval = 1f;

    [SerializeField] int InstantFallScore = 110;

	ArenaManager arenaManager;

	int _internalScore = 0;
    static int _bestScore = 0;

	private void Awake() {
		Instance = this;
		_internalScore = 0;
		StartCoroutine(ScoreUpgrade());

	}

	void Start() {
		arenaManager = GameObject.Find("/Arena").GetComponent<ArenaManager>();

	}

	IEnumerator ScoreUpgrade() {
        // Prepare sync with ShapeMovement
        bool next = false;
        NotificationManager.Instance.AttachNotif(EventNotification.OnShapeMove, this, (o) => { next = true; });

		while (true) {

			if(!_useShapeTick) yield return new WaitForSeconds(_routineInterval);
            else
            {
                yield return new WaitWhile(() => !next);
                next = false;
            }

			_internalScore += (int)_rewardPerSpeed.Evaluate(HavanaManager.Instance.GlobalTransitionInterval);
			arenaManager.SetScore(_internalScore);
			UpdateUI();
		}

	}

	void UpdateUI() {
        if(_internalScore > _bestScore)
        {
            _bestScore = _internalScore;
            _bestScoreText.text = "Best Score :\n"+_bestScore;
            _bestScoreText.gameObject.SetActive(true);
        }

		_text.text = _internalScore.ToString();

	}

	internal void AddScore(int v) {
		_internalScore += v;
		UpdateUI();
	}

    public void InstantFallReward()
    {
        _internalScore += ((int)(InstantFallScore / HavanaManager.Instance.GlobalTransitionInterval)) * 2;
        UpdateUI();
    }
}
