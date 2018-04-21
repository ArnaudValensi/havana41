using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour {

	public bool flipX;
	public float bulletFrequency = 2f;
	public GameObject bulletPrefab;
	public AudioClip[] audioClips;
	public bool randomPitch;
	public Vector2 randomPitchRange;
	public float fireShakeDuration = 0.5f;
	public bool knockBackOnFire = true;
	public float knockbackDistance = 0.5f;
	public GameObject gunExplosion;
	public bool randomBulletRotation;
	public float maxBulletRotation;
	public int nbBulletsPerShot = 3;

	GameObject bulletsHolder;
	AudioSource audioSource;
	CameraShake cameraShake;

	void Start() {
		bulletsHolder = GameObject.Find("/BulletsHolder");
		audioSource = GetComponent<AudioSource>();
		cameraShake = Camera.main.GetComponent<CameraShake>();
	}

	public void Update() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			for (int i = 0; i < nbBulletsPerShot; i++) {
				Fire();
			}
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			nbBulletsPerShot = (nbBulletsPerShot == 1) ? 3 : 1;
		}
	}

	void Fire() {
		// Create bullet
		GameObject bullet = Instantiate(
			bulletPrefab,
			transform.position,
			transform.rotation,
			bulletsHolder.transform
		);

		if (randomBulletRotation) {
			float halfAngle = maxBulletRotation - maxBulletRotation / 2;
			bullet.transform.Rotate(0f, 0f, Random.Range(-halfAngle, halfAngle));
		}

		// Gun sound
		PlayerSound();

		// Fire animation
		if (gunExplosion) {
			gunExplosion.SetActive(true);
		}

		// Camera shake
		cameraShake.Shake(fireShakeDuration);

		// Knockback
		if (knockBackOnFire) {
			transform.parent.position += transform.parent.right * -knockbackDistance;
		}
	}

	void PlayerSound() {
		audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];

		if (randomPitch) {
			audioSource.pitch = Random.Range(randomPitchRange.x, randomPitchRange.y);
		}

		audioSource.Play();
	}

}
