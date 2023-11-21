using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class PowerUpManager : MonoBehaviour
{
    public List<PowerUp> powerUps;
    public enum PowerUpType
    {
        QiangKou,
        QiangGuan,
        QiangTuo,
        DanJia,
        DanYao,
        WoBa,
    }

    public enum Quality
    {
        Common,
        UnCommon,
        Rare,
        Epic,
        Legend
    }

    private void Awake()
    {
        Global.powerUpManager = this;
    }

    public void GeneratePowerUp(float score, PowerUp powerUp)
    {
        var powerUpType = (PowerUpType)Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);
        ModifyName(powerUpType, powerUp);
        switch (powerUpType)
        {
            case PowerUpType.QiangKou:
                break;
            case PowerUpType.QiangGuan:
                break;
            case PowerUpType.QiangTuo:
                break;
            case PowerUpType.DanJia:
                break;
            case PowerUpType.DanYao:
                break;
            case PowerUpType.WoBa:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ModifyName(PowerUpType type, PowerUp powerUp)
    {
        var powerUpName = "";
        switch (type)
        {
            case PowerUpType.QiangKou:
                powerUpName = "强化枪口";
                break;
            case PowerUpType.QiangGuan:
                powerUpName = "强化枪管";
                break;
            case PowerUpType.QiangTuo:
                powerUpName = "强化枪托";
                break;
            case PowerUpType.DanJia:
                powerUpName = "强化弹夹";
                break;
            case PowerUpType.DanYao:
                powerUpName = "强化弹药";
                break;
            case PowerUpType.WoBa:
                powerUpName = "强化握把";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        powerUp.powerUpName = powerUpName;
    }
    
    public void ModifyData(PowerUpType type, PowerUp powerUp, float score)
    {
        switch (type)
        {
            case PowerUpType.QiangKou:
                powerUp.scatterAngleModifier -= score;
                powerUp.damageModifier -= score;
                powerUp.bulletSpeedModifier += score;
                break;
            case PowerUpType.QiangGuan:
                powerUp.fireRateModifier -= score;
                powerUp.bulletSpeedModifier += score;
                powerUp.scatterAngleModifier += score;
                break;
            case PowerUpType.QiangTuo:
                powerUp.scatterAngleModifier -= score;
                powerUp.moveSpeedModifier += score;
                break;
            case PowerUpType.DanJia:
                powerUp.ammoModifier += score;
                powerUp.reloadTimeModifier -= score;
                break;
            case PowerUpType.DanYao:
                powerUp.damageModifier += score;
                powerUp.reloadTimeModifier += score;
                break;
            case PowerUpType.WoBa:
                powerUp.scatterAngleModifier -= score;
                powerUp.fireRateModifier -= score;
                powerUp.moveSpeedModifier -= score;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private Quality DetermineQuality(float score)
    {
        if (score >= 1000) return Quality.Legend;
        else if (score >= 750) return Quality.Epic;
        else if (score >= 500) return Quality.Rare;
        else if (score >= 250) return Quality.UnCommon;
        else return Quality.Common;
    }
}