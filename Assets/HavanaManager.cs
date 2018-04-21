using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Helper
{
    public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
    {
        foreach (var t in @this) action(t);
    }
}

public class HavanaManager : MonoBehaviour {

	public static HavanaManager Instance { get; set; }

    [SerializeField] float _fastTransitionInterval;

    public float GlobalTransitionInterval
    {
        get
        {
            if (GlobalTransitionCurve.keys.Last().time <= TransitionCurveCursor) return GlobalTransitionCurve.keys.Last().value;
            return GlobalTransitionCurve.Evaluate(TransitionCurveCursor);
        }
    }
    public float FastTransitionInterval
    {
        get
        {
            return _fastTransitionInterval;
        }
    }

    [SerializeField] AnimationCurve GlobalTransitionCurve;

    float TransitionCurveCursor = 0f;
    static public bool isSpeedBlocked = false;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(IncreaseGameSpeed());
    }

    IEnumerator IncreaseGameSpeed()
    {
        while(true)
        {
            if (isSpeedBlocked) yield return null;

            TransitionCurveCursor += Time.deltaTime;
            Debug.Log($"{TransitionCurveCursor} => {GlobalTransitionInterval}");
            yield return null;
        }

    }


}
