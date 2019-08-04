using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
  [SerializeField] private GameObject deathSoundGameObject;

  public event Action OnDeath = delegate { };

  private void Awake()
  {
    GameManager.Instance.OnEnemyDeath(this);
  }

  public void Die()
  {
    OnDeath();
    Instantiate(deathSoundGameObject, transform.position, Quaternion.identity).GetComponent<EnemySFXPlayer>().PlayImpact();
    Destroy(gameObject);
  }
}
