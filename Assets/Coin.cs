using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour {

    [SerializeField] int ScoreUpgrade = 10000;
    [SerializeField] UnityEvent onCollect;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            onCollect.Invoke();
            ScoreBanner.Instance.AddScore(ScoreUpgrade);
            Destroy(gameObject);
        }
    }

}
