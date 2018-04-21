using UnityEngine;

public class KnockbackOnHit : MonoBehaviour {

	public float distance = 0.5f;

	int bulletLayerId = int.MaxValue;

	void Start() {
		bulletLayerId = LayerMask.NameToLayer("Bullet");
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.layer == bulletLayerId) {
			transform.position += transform.right * -distance;
		}
	}

}
