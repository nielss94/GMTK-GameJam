using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private bool isDeath;
    public Action OnPlayerDeath;

    private new Rigidbody2D rigidbody;
    private PlayerCombat playerCombat;
    private Animator animator;
    private PlayerPlatformerController playerPlatformerController;

    private void Awake()
    {
        playerCombat = GetComponent<PlayerCombat>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerPlatformerController = GetComponent<PlayerPlatformerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDeath)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("PlayerDeath"))
            {
                isDeath = true;
                rigidbody.isKinematic = true;
                rigidbody.gravityScale = 0;
                rigidbody.velocity = Vector2.zero;
                playerPlatformerController.CanMove = false;
                playerCombat.CanThrow = false;
                animator.SetTrigger("death");

                if (playerCombat.chargingSpear)
                {
                    playerCombat.Throw();
                }
                OnPlayerDeath?.Invoke();
            }
        }
    }
}