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
        SpeedUp,
        Bomb
    }
    #endregion

    [SerializeField] UnityEvent onFire;
    [SerializeField] UnityEvent onStopEffect;
    [SerializeField] public UnityEvent OnShapeStopFall;
    [SerializeField] BonusMalusType type;

    [Header("SpeedUp Configuration")]
    [SerializeField] AnimationCurve _speedOffsetCurve;

    [Header("Lineargrid conf")]
    [SerializeField] int _elementToSpawn=3;
    [SerializeField] GameObject _singleBlockprefab;

    [SerializeField] int bumperScoreValue = 0;


    [Header("BombConf")]
    [SerializeField] Animation _animation;
    [SerializeField] AnimationClip _readyToFire;

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
            case BonusMalusType.Bomb:
                CanFire = false;
                StartCoroutine(BombExplosion());
                break;
            case BonusMalusType.Null:
            default:
                break;
        }
    }
    
    IEnumerator BombExplosion()
    {
        Debug.Log(transform.parent.position);

        if(_animation != null && _readyToFire != null && _animation[_readyToFire.name] != null)
            _animation.Play(_readyToFire.name);

        var shape= GetComponentInParent<ShapeMovementController>();
        var next = false;
        shape._onStopFalling.AddListener(() => { next = true; });
        yield return new WaitWhile(() => !next);


        DestroyElement(transform.parent.position.x-1, transform.parent.position.y-1);
        DestroyElement(transform.parent.position.x-1, transform.parent.position.y);
        DestroyElement(transform.parent.position.x-1, transform.parent.position.y+1);

        DestroyElement(transform.parent.position.x+1, transform.parent.position.y-1);
        DestroyElement(transform.parent.position.x+1, transform.parent.position.y);
        DestroyElement(transform.parent.position.x+1, transform.parent.position.y+1);

        DestroyElement(transform.parent.position.x, transform.parent.position.y-1);
        DestroyElement(transform.parent.position.x, transform.parent.position.y+1);

        DestroyElement(transform.parent.position.x, transform.parent.position.y);
    }
    void DestroyElement(float x, float y)
    {
        try
        {
            Destroy(Managers.Grid.gameGridcol[(int)x].row[(int)y].gameObject);
            Managers.Grid.gameGridcol[(int)x].row[(int)y] = null;
        }
        catch { }
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

        foreach (var el in allGridElement)
        {
            Managers.Spawner.Spawn(_singleBlockprefab, el.colIdx, el.rowIdx);
            if (++spawned >= _elementToSpawn) break;
        }
        
        Debug.Log(gameObject.GetComponent<Renderer>());

        CanFire = false;
        yield break;
    }

    void ScoreBumper()
    {
        ScoreBanner.Instance.AddScore(bumperScoreValue);
        onStopEffect.Invoke();
    }

    IEnumerator SpeedUpRoutine()
    {
        float startTime = 0;

        while (true)
        {
            HavanaManager.Instance.SpeedOffset = _speedOffsetCurve.Evaluate(startTime, true);
            //Debug.Log(HavanaManager.Instance.SpeedOffset);
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
