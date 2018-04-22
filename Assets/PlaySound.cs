using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

	public void PlaySound_(string name)
    {
        SoundManager.Instance.PlaySound(name);
    }
}
