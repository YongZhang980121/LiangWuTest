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
            other.gameObject.GetComponent<Enemy>().TakeDamage(Global.rifleDamage);
        }
    }

    private void OnHit()
    {
        lifeTimeTween.Kill();
        Global.bulletManager.ReturnRifleBulletToPool(this);
    }
}
