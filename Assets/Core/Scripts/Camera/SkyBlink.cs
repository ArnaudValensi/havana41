using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class SkyBlink : MonoBehaviour {

	public Color blinkColor = Color.white;
	public float duration = 0.2f;

	Camera cameraToBlink;
	Color originalColor;

	void Start () {
		cameraToBlink = GetComponent<Camera>();

		originalColor = cameraToBlink.backgroundColor;
	}
	
	public void Blink () {
		StartCoroutine(BlinkCoroutine());
	}

	IEnumerator BlinkCoroutine() {
		cameraToBlink.backgroundColor = blinkColor;
		yield return new WaitForSeconds(duration);
		cameraToBlink.backgroundColor = originalColor;
	}
}
