using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour {

    [SerializeField] Animator _animator;
    [SerializeField] string _parameterName = "InternalState";

    private void Awake()
    {
        NotificationManager.Instance.AttachNotif(EventNotification.MainMenuStart, this, (o) => _animator.SetInteger(_parameterName, 0));
        NotificationManager.Instance.AttachNotif(EventNotification.GameStart, this, (o) => 
        _animator.SetInteger(_parameterName, 1));
        NotificationManager.Instance.AttachNotif(EventNotification.GameOver, this, (o) => _animator.SetInteger(_parameterName, 2));
    }
}
