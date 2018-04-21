using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] float _globalTransitionInterval = 0.8f;
    [SerializeField] float _fastTransitionInterval;

    public float GlobalTransitionInterval
    {
        get
        {
            return _globalTransitionInterval;
        }
        set
        {
            _globalTransitionInterval = value;
        }
    }
    public float FastTransitionInterval
    {
        get
        {
            return _fastTransitionInterval;
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError("MultiHavanaManager");




    }


}
