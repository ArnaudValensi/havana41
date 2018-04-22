using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinSpawnerManager : MonoBehaviour {

    [SerializeField] List<CoinSpawner> allSpawners;
    [SerializeField] float _spawnInterval = 2f;

    private void Start()
    {
        StartCoroutine(SpawnCoins());
    }


    IEnumerator SpawnCoins()
    {
        while(true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            allSpawners.Where(i=>!i.isRunning).Random().Spawn();
        }

    }

}
