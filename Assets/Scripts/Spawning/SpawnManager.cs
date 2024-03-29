﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SpawnManager : MonoBehaviour
{
    public SpawnPoint[] spawnpoints = null;
    public SpawnOptions spawnOptions;

    private int currentWave = 1;
    private bool firstRound = true;

    public event Action<Enemy> OnNewSpawn = delegate {};

    private void Start() {
        spawnpoints = FindObjectsOfType<SpawnPoint>().Where(sp => (int)sp.spawnLevel <= (int)GameManager.Instance.gameMode).ToArray();
        LoadSpawnOptions();
        StartCoroutine(SpawnEnemies());
    }

    private void LoadSpawnOptions()
    {
        switch(GameManager.Instance.gameMode)
        {
            case Difficulty.Easy:
                spawnOptions = Instantiate(Resources.Load<SpawnOptions>("SpawnOptions/Easy"));
            break;
            case Difficulty.Medium:
                spawnOptions = Instantiate(Resources.Load<SpawnOptions>("SpawnOptions/Medium"));
            break;
            case Difficulty.Hard:
                spawnOptions = Instantiate(Resources.Load<SpawnOptions>("SpawnOptions/Hard"));
            break;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        bool enemyOverflow = FindObjectsOfType<Enemy>().Length >= 40;


        float timeBeforeNextWave = UnityEngine.Random.Range(spawnOptions.minSpawnCooldown, spawnOptions.maxSpawnCooldown);
        
        List<SpawnPoint> availableSpawns = null;
        if(!enemyOverflow){
            int randomEnemyAmount = UnityEngine.Random.Range((int)spawnOptions.minEnemiesToSpawn, (int)spawnOptions.maxEnemiesToSpawn);
            availableSpawns = FindAvailableSpawnpoints(randomEnemyAmount);
        }
        
        if(firstRound)
        {
            timeBeforeNextWave = 0.5f;
            firstRound = false;
        }

        yield return new WaitForSeconds(timeBeforeNextWave);
    
        if(!enemyOverflow)
        {
            if(currentWave % 15 == 0)
            {
                IncreaseDifficulty();
            }

            currentWave++;
            
            foreach (var spawn in availableSpawns)
            {
                int randomEnemyNumber = UnityEngine.Random.Range(0,spawnOptions.enemies.Length);
                spawn.Spawn(spawnOptions.enemies[randomEnemyNumber]);
            }
        }
        StartCoroutine(SpawnEnemies());
    }

    private List<SpawnPoint> FindAvailableSpawnpoints(int amount)
    {
        List<SpawnPoint> result = new List<SpawnPoint>();

        for (int i = 0; i < amount; i++)
        {
            SpawnPoint[] availableSpawns = spawnpoints.Where(sp => sp.available == true).ToArray();
            if(availableSpawns.Length == 0)
                break;

            int randomNum = UnityEngine.Random.Range(0,availableSpawns.Length);
            result.Add(availableSpawns[randomNum]);
        }

        return result;
    }

    private void IncreaseDifficulty()
    {
        spawnOptions.minEnemiesToSpawn *= spawnOptions.difficultyMultiplier;
        spawnOptions.maxEnemiesToSpawn *= spawnOptions.difficultyMultiplier;

        spawnOptions.minSpawnCooldown = Mathf.Clamp(spawnOptions.minSpawnCooldown  / spawnOptions.difficultyMultiplier, 0.5f, Mathf.Infinity);
        spawnOptions.maxSpawnCooldown = Mathf.Clamp(spawnOptions.maxSpawnCooldown  / spawnOptions.difficultyMultiplier, 1f, Mathf.Infinity);
    }
}
