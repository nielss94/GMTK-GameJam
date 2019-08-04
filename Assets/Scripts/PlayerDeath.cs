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
                GetComponent<Animator>().SetTrigger("death");
                GetComponent<Rigidbody2D>().isKinematic = true;
                OnPlayerDeath?.Invoke();
            }
        }
    }
}