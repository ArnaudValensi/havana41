using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationManagement : MonoBehaviour {

    [SerializeField] List<Animation> animations;
    public static AnimationManagement Instance;

    private void Start()
    {
        Instance = this;
    }
    public void PlayAnimation(string name)
    {
        foreach (var t in animations)
        {
            if(t[name] != null || t.clip?.name == name)
            {
                t.Play(name);
            }
        }
    }   



}
