using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField] int ScoreUpgrade = 10000;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            ScoreBanner.Instance.AddScore(ScoreUpgrade);
            Destroy(gameObject);
        }
    }

}
