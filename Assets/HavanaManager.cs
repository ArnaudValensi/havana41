using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavanaManager : MonoBehaviour {

	public static HavanaManager Instance { get; set; }

    [SerializeField] float _globalTransitionInterval = 0.8f;
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



    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError("MultiHavanaManager");




    }


}
