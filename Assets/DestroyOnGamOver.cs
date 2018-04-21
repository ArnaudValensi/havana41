using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGamOver : MonoBehaviour {

    private void Start()
    {
        NotificationManager.Instance.AttachNotif(EventNotification.GameOver, this, (o) => { Destroy(gameObject); });
    }
}
