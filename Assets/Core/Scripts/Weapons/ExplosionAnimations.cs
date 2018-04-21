using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class ExplosionAnimations : MonoBehaviour {

	public string animationName;
	public string criticAnimation = "";
	public float criticChances;

	Animator animator;
	SkyBlink skyBlink;

	void Awake() {
		animator = GetComponent<Animator>();
		skyBlink = Camera.main.GetComponent<SkyBlink>();
	}

	void OnEnable() {
		string animationToPlay;

		if (criticAnimation.Length > 0 && Random.Range(0f, 1f) < criticChances) {
			animationToPlay = criticAnimation;
			skyBlink.Blink();
		} else {
			animationToPlay = animationName;
		}

		animator.Play(animationToPlay);
		StartCoroutine(OnComplete(animationToPlay));
	}

	IEnumerator OnComplete(string animation) {
		yield return null;

		while (animator.GetCurrentAnimatorStateInfo(0).IsName(animation)) {
			yield return null;
		}

		gameObject.SetActive(false);
	}
}
