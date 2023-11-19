using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RifleBullet : Bullet
{
    private Tween lifeTimeTween;
    private Tween glowTween;
    private void OnEnable()
    {
        lifeTimeTween = DOVirtual.DelayedCall(2f, () =>
        {
            Global.bulletManager.ReturnRifleBulletToPool(this);
        });
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnHit();
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(Global.rifleDamage);

            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

            float knockbackDistance = 50f;
            float knockbackDuration = 0.2f;

            if (enemy.KnockbackTween != null && enemy.KnockbackTween.IsActive())
            {
                enemy.KnockbackTween.Kill();
            }

            enemy.KnockbackTween = other.transform.DOMove((Vector2)other.transform.position + knockbackDirection * knockbackDistance, knockbackDuration);
            
            if (glowTween != null)
            {
                glowTween.Kill();
            }

            enemy.spriteImage.material.SetFloat("_Glow", 0f);
            
            glowTween = DOTween.To(() => enemy.spriteImage.material.GetFloat("_Glow"), x => enemy.spriteImage.material.SetFloat("_Glow", x), 3f, knockbackDuration / 2)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    glowTween = null;
                });
        }
    }

    private void OnHit()
    {
        lifeTimeTween.Kill();
        Global.bulletManager.ReturnRifleBulletToPool(this);
    }
}
