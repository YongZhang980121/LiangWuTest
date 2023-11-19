using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RifleBullet : Bullet
{
    private Tween lifeTimeTween;
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

            // 计算击退方向
            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

            // 设置击退距离和时间
            float knockbackDistance = 50f;
            float knockbackDuration = 0.2f;

            // 如果之前的动画还在进行，则先停止它
            if (enemy.KnockbackTween != null && enemy.KnockbackTween.IsActive())
            {
                enemy.KnockbackTween.Kill();
            }

            // 开始新的击退动画
            enemy.KnockbackTween = other.transform.DOMove((Vector2)other.transform.position + knockbackDirection * knockbackDistance, knockbackDuration);
        }
    }

    private void OnHit()
    {
        lifeTimeTween.Kill();
        Global.bulletManager.ReturnRifleBulletToPool(this);
    }
}
