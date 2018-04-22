using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour {

    public void PlayAnimation_(AnimationClip clip)
    {
        AnimationManagement.Instance.PlayAnimation(clip.name);
    }

}
