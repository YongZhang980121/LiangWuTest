using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Global
{
    public static BattleManager battleManager;
    public static BulletManager bulletManager;
    public static EnemyManager enemyManager;
    public static WeaponController weaponController;
    public static BulletShellManager bulletShellManager;
    public static PlayerController playerController;

    public static float enemyHp;
    public static float rifleDamage;
    public static float rifleFireRate;
}
