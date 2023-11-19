using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health;
    private Sequence wobbleSequence;
    public Image spriteImage;
    public Tween KnockbackTween { get; set; }
    private void OnEnable()
    {
        health = Global.enemyHp;
        ResetAndStartWobbleAnimation();
    }

    private void OnDisable()
    {
        if (wobbleSequence != null)
        {
            wobbleSequence.Kill();
        }
    }
    
    private void ResetAndStartWobbleAnimation()
    {
        spriteImage.transform.localScale = Vector3.one;

        if (wobbleSequence != null)
        {
            wobbleSequence.Kill();
        }
        wobbleSequence = DOTween.Sequence();
        wobbleSequence.Append(spriteImage.transform.DOScale(new Vector3(1.1f, 0.9f, 1f), 0.5f))
            .Append(spriteImage.transform.DOScale(new Vector3(0.9f, 1.1f, 1f), 0.5f))
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Global.enemyManager.ReturnEnemyToPool(this);
    }
}
