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
    public static PowerUpManager powerUpManager;
    public static UIManager uiManager;
    public static Chest chest;

    public static float enemyHp;
    public static float rifleDamage;
    public static float rifleFireRate;
    public static float chaos;
    public static float rifleReloadTime;
    public static float scatterAngle;
    public static int ammo;
    public static float score;
}
