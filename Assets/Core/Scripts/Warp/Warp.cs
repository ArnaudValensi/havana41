using UnityEngine;

public class Warp : MonoBehaviour {
	public bool isLeft = true;
	public Transform otherWarp;
	public float offset = 2f;

	[ReadOnly][SerializeField] float distanceBetweenWarps;

	void Start() {
		distanceBetweenWarps = Mathf.Abs(transform.position.x - otherWarp.position.x) - offset;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Warpable>() != null) {
			float warpDistance = isLeft ? distanceBetweenWarps : -distanceBetweenWarps;
			other.transform.TranslateX(warpDistance);
		}
	}
}
