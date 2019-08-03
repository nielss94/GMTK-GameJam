using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private SpawnManager spawnManager;

    [SerializeField]private int score;

    public SpawnLevel gameMode;

    public event Action<int> OnScoreChanged = delegate{};

    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath += () => {
            score++;
            OnScoreChanged(score);
        };
    }
}
