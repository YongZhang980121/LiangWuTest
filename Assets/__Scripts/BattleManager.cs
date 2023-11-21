using System;
using System.Collections;
using System.Collections.Generic;
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
         Global.enemyHp = 30f;
         Global.rifleDamage = 10f;
         Global.rifleFireRate = 0.2f;
         Global.chaos = 0f;
         Global.scatterAngle = 10f;
         Global.ammo = 20;
         Global.rifleReloadTime = 2f;
         Global.score = 0f;
         Global.chaos = 0f;
    }
}
