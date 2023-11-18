using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Transform canvasTransform;
    public Transform rifleBulletHolder;
    public Transform enemyHolder;
    private void Awake()
    {
         Application.targetFrameRate = 120;
         Global.battleManager = this;
         Global.enemyHp = 10f;
         Global.rifleDamage = 10f;
         Global.rifleFireRate = 0.2f;
    }
}
