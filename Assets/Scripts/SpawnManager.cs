﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    
    [SerializeField]
    private GameObject _enemyContenedor;
    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawning = false;
    void Start()
    {
        
    }
    public void StartSpawing()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerRoutine());
    }
    /* void Update()
    {
        StartCoroutine(OnPlayerDeath());
    }
    */
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContenedor.transform;
            yield return new WaitForSeconds(2.0f);
        }
    }
    IEnumerator SpawnPowerRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp =  Random.Range(0 , powerups.Length);
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2, 5));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}