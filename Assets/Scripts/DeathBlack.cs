using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DeathBlack : MonoBehaviour
{
    private PlayerDeath playerDeath;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerDeath = FindObjectOfType<PlayerDeath>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerDeath.OnPlayerDeath += PlayAnimation;
    }

    private void PlayAnimation()
    {
        spriteRenderer.DOFade(1f, 1f);
    }
}