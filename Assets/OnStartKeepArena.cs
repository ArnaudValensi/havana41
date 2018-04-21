using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartKeepArena : MonoBehaviour {

    [SerializeField] Transform _toKeep;

    private void Awake()
    {
        Keep();
    }

    void Keep()
    {
        _toKeep.gameObject.SetActive(false);
    }

    public void CreateSession()
    {
        GameObject.Instantiate(_toKeep.gameObject, _toKeep.position, _toKeep.rotation, _toKeep.parent).SetActive(true);
    }
}
