using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefabCoin;
    [SerializeField] float DestroyTime = 5f;

    public bool isRunning = false;

    GameObject currentCoin;

    public void Spawn()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        isRunning = true;
        currentCoin = GameObject.Instantiate(prefabCoin, transform, false);
        yield return new WaitForSeconds(DestroyTime);

        if (currentCoin != null)
            Destroy(currentCoin);

        isRunning = false;
    }

    
}
