using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Transform canvasTransform;
    public Transform rifleBulletHolder;
    public Transform enemyHolder;
    public Transform slimeHolder;
    public Transform scoreHolder;
    private void Awake()
    {
         Application.targetFrameRate = 120;
         Global.battleManager = this;
         Global.enemyHp = 5f;
         Global.rifleDamage = 10f;
         Global.rifleFireRate = 0.25f;
         Global.chaos = 0f;
         Global.scatterAngle = 10f;
         Global.ammo = 20;
         Global.rifleReloadTime = 2f;
         Global.score = 0f;
         Global.chaos = 0f;
         Global.numOfEnemyPerMinute = 20;
         Global.bulletSpeed = 3000f;
         Global.moveSpeed = 500f;
    }

    private void Start()
    {
        IncrementChaos();
    }

    private void IncrementChaos()
    {
        DOVirtual.DelayedCall(10, () =>
        {
            Global.chaos += 1;
            IncrementChaos();
        });
    }
}
