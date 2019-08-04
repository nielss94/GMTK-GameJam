using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private bool isDeath;
    public Action OnPlayerDeath;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDeath)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("PlayerDeath"))
            {
                isDeath = true;
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<PlayerPlatformerController>().CanMove = false;
                GetComponent<PlayerCombat>().CanThrow = false;
                GetComponent<Animator>().SetTrigger("death");
                OnPlayerDeath?.Invoke();
            }
        }
    }
}