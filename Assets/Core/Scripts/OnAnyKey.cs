using UnityEngine;
using UnityEngine.Events;

public class OnAnyKey : MonoBehaviour {

	public UnityEvent onAnyKey;

	void Update () {
		if (Input.anyKey) {
			onAnyKey.Invoke();
		}
	}
}
