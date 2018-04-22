using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BonusMalus : MonoBehaviour {

    #region
    enum BonusMalusType
    {
        Null,
        AutoLiner_NOIMPL,
        ScoreBumper,
        SpeedUp_NOIMPL
    }
    #endregion

    [SerializeField] UnityEvent onFire;
    [SerializeField] UnityEvent onStopEffect;
    [SerializeField] BonusMalusType type;

    [Header("SpeedUp Configuration")]
    [SerializeField] AnimationCurve _speedOffsetCurve;

    bool CanFire = true;
    
	public void Fire()
    {
        if (!CanFire) return;
        onFire.Invoke();

        switch (type)
        {
            case BonusMalusType.AutoLiner_NOIMPL:
                break;
            case BonusMalusType.ScoreBumper:
                ScoreBumper();
                break;
            case BonusMalusType.SpeedUp_NOIMPL:
                CanFire = false;
                StartCoroutine(SpeedUpRoutine());
                break;
            case BonusMalusType.Null:
            default:
                break;
        }
    }

    void ScoreBumper()
    {
        ScoreBanner.Instance.AddScore(5000);
        onStopEffect.Invoke();
    }

    IEnumerator SpeedUpRoutine()
    {
        float startTime = 0;

        while(true)
        {
            HavanaManager.Instance.SpeedOffset = _speedOffsetCurve.Evaluate(startTime, true);
            Debug.Log(HavanaManager.Instance.SpeedOffset);
            if (startTime > _speedOffsetCurve.keys.Last().time)
            {
                onStopEffect.Invoke();
                yield break;
            }

            yield return null;
            startTime += Time.deltaTime;
        }

    }

}
