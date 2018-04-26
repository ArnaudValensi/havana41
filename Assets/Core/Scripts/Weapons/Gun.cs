using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour {

	#region InternalType

	public enum BulletType {
		Null,
		Move,
		TurnRight,
		TurnLeft,
		Fall
	}

	[System.Serializable]
	class BulletTypePrefabAsso {
		public BulletType type;
		public GameObject prefab;
	}

	#endregion

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
	public Laser laser1;
	public Laser laser2;
	public Color laserColor1;
	public Color laserColor2;
	public Color laser2Color1;
	public Color laser2Color2;

	[Space(10)]
	[SerializeField] bool _useRaycast = false;

	[Space(10)]
	[SerializeField] List<BulletTypePrefabAsso> BulletConfiguration;
	[SerializeField] UnityEvent onShootNormal;
	[SerializeField] UnityEvent onShootRotate;

	GameObject bulletsHolder;
	AudioSource audioSource;
	CameraShake cameraShake;

	void Start() {
		bulletsHolder = GameObject.Find("BulletsHolder");
		audioSource = GetComponent<AudioSource>();
		cameraShake = Camera.main.GetComponent<CameraShake>();
	}

	public void Update() {
		if (Time.timeScale == 0f) {
			return;
		}

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.X)) {
			for (int i = 0; i < nbBulletsPerShot; i++) {
				Fire(BulletType.Move);
			}
		}

		if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Mouse1))
		{
			for (int i = 0; i < nbBulletsPerShot; i++)
			{
				Fire(BulletType.TurnRight);
			}
			onShootRotate.Invoke();
		}

		if (Input.GetKeyDown(KeyCode.M))
		{
			for (int i = 0; i < nbBulletsPerShot; i++)
			{
				Fire(BulletType.TurnLeft);
			}
			onShootRotate.Invoke();
		}
	}


	GameObject GetBulletPrefab(BulletType bulletType) => BulletConfiguration.FirstOrDefault( i => i.type== bulletType)?.prefab ?? null;

	void Fire(BulletType bulletType = BulletType.Move) {

		if (!_useRaycast) {
			var bulletPrefab = GetBulletPrefab(bulletType) ?? this.bulletPrefab;

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
		} else {
			RaycastHit2D result;

			Color color1;
			Color color2;

			if (bulletType == BulletType.Move) {
				color1 = laserColor1;
				color2 = laserColor2;
			} else {
				color1 = laser2Color1;
				color2 = laser2Color2;
			}

			Debug.DrawRay(transform.position, transform.right * 1000 + Vector3.up * 0.01f, Color.blue, 2f);

			int layer = 1 << 9;
			layer += 1 << 13;
			if (result = Physics2D.Raycast(transform.position, transform.right, 1000, layer)) {
				//Debug.Log($"touch {result.transform}");
				System.Action<RaycastHit2D, bool> TouchAction = (r, inverse) => r.transform.GetComponent<BlockCollider>()?.Touch(gameObject, bulletType, inverse);
				if (result.transform.gameObject.layer == 13) { // Warp
					result.transform.GetComponent<LoopFire>().ReFire(transform.position, transform.right, laser2, color1, color2, TouchAction);
				} else {
					TouchAction(result, false);
				}
			}

			laser1.Shoot(color1, color2, transform.position, result.point - result.normal);
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
