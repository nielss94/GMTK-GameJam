using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event Action OnDeath = delegate {};

    private void Awake() {
        GameManager.Instance.OnEnemyDeath(this);
    }

    public void Die()
    {
        OnDeath();
        Destroy(gameObject);
    }
}
