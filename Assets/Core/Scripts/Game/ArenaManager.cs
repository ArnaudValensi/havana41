using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour {

	public Animator animator;

	void Start() {
		animator = GetComponent<Animator>();
	}

	public void GameOverToTitle() {
		Debug.Log("GameOverToTitle");

		animator.SetInteger("InternalState", 0);
	}

}
