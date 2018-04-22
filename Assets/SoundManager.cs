using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    #region InternalStruct
    [System.Serializable]
    class NameAndSound
    {
        public string name;
        public AudioSource audio;
    }
    #endregion
    public static SoundManager Instance;
    [SerializeField] List<NameAndSound> conf;

    private void Start()
    {
        Instance = this;
    }

    public void PlaySound(string name)
    {
        conf.Where(i => i.name == name).FirstOrDefault()?.audio.Play();
    }


}
