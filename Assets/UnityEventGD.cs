using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventGD : MonoBehaviour {

    [SerializeField] UnityEvent _onStart;

    // Use this for initialization
    void Start () {
        _onStart.Invoke();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
