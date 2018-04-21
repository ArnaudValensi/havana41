using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour {

	public float bulletSpeed = 10f;
	public float timeToDestroy = 1f;

	void Start() {
		Destroy(gameObject, timeToDestroy);
	}

	void Update() {
		transform.position += transform.right * bulletSpeed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D col) {
		Destroy(gameObject);
	}

}
