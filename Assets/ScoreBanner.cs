using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBanner : MonoBehaviour {

    public static ScoreBanner Instance;

    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] AnimationCurve _rewardPerSpeed;
    [SerializeField] float _routineInterval = 1f;

    int _internalScore = 0;

    private void Awake()
    {
        Instance = this;
        _internalScore = 0;
        StartCoroutine(ScoreUpgrade());
    }

    IEnumerator ScoreUpgrade()
    {

        while(true)
        {
            yield return new WaitForSeconds(_routineInterval);

            _internalScore += (int)_rewardPerSpeed.Evaluate(HavanaManager.Instance.GlobalTransitionInterval);
            _text.text = _internalScore.ToString();
        }

    }




}
