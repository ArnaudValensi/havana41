using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour {

    #region InternalType
    enum BulletType
    {
        Null,
        Move,
        RotateClock,
        RotateNClock,
        Fall
    }
    [System.Serializable]
    class BulletTypePrefabAsso
    {
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

    [Space(10)]
    [SerializeField] List<BulletTypePrefabAsso> BulletConfiguration;

    GameObject bulletsHolder;
	AudioSource audioSource;
	CameraShake cameraShake;

	void Start() {
		bulletsHolder = GameObject.Find("BulletsHolder");
		audioSource = GetComponent<AudioSource>();
		cameraShake = Camera.main.GetComponent<CameraShake>();
	}

	public void Update() {

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Q)) {
			for (int i = 0; i < nbBulletsPerShot; i++) {
				Fire(BulletType.Move);
			}
		}

        if (Input.GetKeyDown(KeyCode.X))
        {
            for (int i = 0; i < nbBulletsPerShot; i++)
            {
                Fire(BulletType.RotateClock);
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < nbBulletsPerShot; i++)
            {
                Fire(BulletType.RotateNClock);
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            for (int i = 0; i < nbBulletsPerShot; i++)
            {
                Fire(BulletType.Fall);
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
			nbBulletsPerShot = (nbBulletsPerShot == 1) ? 3 : 1;
		}
	}

    


    GameObject GetBulletPrefab(BulletType bulletType) => BulletConfiguration.FirstOrDefault( i => i.type== bulletType)?.prefab ?? null;

    void Fire(BulletType bulletType= BulletType.Move) {

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
