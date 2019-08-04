using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;

  private SpawnManager spawnManager;
  private PlayerDeath playerDeath;

  [SerializeField] private int score;

  public Difficulty gameMode;

  public event Action<int> OnScoreChanged = delegate { };

  private bool gameOver = false;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
      return;
    }
    spawnManager = FindObjectOfType<SpawnManager>();
    playerDeath = FindObjectOfType<PlayerDeath>();
  }

  private void Start()
  {
    playerDeath.OnPlayerDeath += () => gameOver = true;
  }

  public void OnEnemyDeath(Enemy enemy)
  {
    enemy.OnDeath += () =>
    {
      if (!gameOver)
      {
        score++;
        OnScoreChanged(score);
      }
    };
  }

  public void SetGameMode(Difficulty difficulty)
  {
    gameMode = difficulty;
  }
}
