using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RifleBullet : Bullet
{
    private void OnEnable()
    {
        DOVirtual.DelayedCall(2f, () =>
        {
            Global.bulletManager.ReturnRifleBulletToPool(this);
        });
    }
}
