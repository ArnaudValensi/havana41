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
        AutoLiner,
        ScoreBumper,
        SpeedUp
    }
    #endregion

    [SerializeField] UnityEvent onFire;
    [SerializeField] UnityEvent onStopEffect;
    [SerializeField] BonusMalusType type;

    [Header("SpeedUp Configuration")]
    [SerializeField] AnimationCurve _speedOffsetCurve;

    [Header("Lineargrid conf")]
    [SerializeField] int _elementToSpawn=3;
    [SerializeField] GameObject _singleBlockprefab;

    bool CanFire = true;
    
	public void Fire()
    {
        if (!CanFire) return;
        onFire.Invoke();

        switch (type)
        {
            case BonusMalusType.AutoLiner:
                StartCoroutine(AutoLiner());
                break;
            case BonusMalusType.ScoreBumper:
                ScoreBumper();
                break;
            case BonusMalusType.SpeedUp:
                CanFire = false;
                StartCoroutine(SpeedUpRoutine());
                break;
            case BonusMalusType.Null:
            default:
                break;
        }
    }
    

    IEnumerator AutoLiner()
    {
        var allGridElement = Managers.Grid.gameGridcol
            .Select((a, idx) => new { col = a, idx = idx })
            .OrderByDescending(i => i.idx)
            .SelectMany((a) => a.col.row.Select((b, idx) => new { col = a.col, colIdx = a.idx, rowIdx = idx, element = b }))
            .Where(el => el.element == null)
            .OrderBy(el => el.rowIdx);

        var spawned = 0; 

        foreach(var el in allGridElement)
        {
            Managers.Spawner.Spawn(_singleBlockprefab, el.colIdx, el.rowIdx);
            if (++spawned >= _elementToSpawn) break;
        }
        CanFire = false;
        yield break;
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
