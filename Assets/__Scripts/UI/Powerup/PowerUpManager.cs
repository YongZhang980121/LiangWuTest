using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PowerUpManager : MonoBehaviour
{
    public List<PowerUp> powerUps;

    public Color commonColor;
    public Color unCommonColor;
    public Color rareColor;
    public Color epicColor;
    public Color legendColor;

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
        var quality = DetermineQuality(score);
        ModifyName(powerUpType, powerUp);
        ModifyData(powerUpType, powerUp, score);
        ModifyQuality(powerUp, quality);
    }

    public void ModifyQuality(PowerUp powerUp, Quality quality)
    {
        Color backgroundColor;
        int arrowNum = 0;
        switch (quality)
        {
            case Quality.Common:
                arrowNum = 1;
                backgroundColor = commonColor;
                break;
            case Quality.UnCommon:
                arrowNum = 2;
                backgroundColor = unCommonColor;
                break;
            case Quality.Rare:
                arrowNum = 3;
                backgroundColor = rareColor;
                break;
            case Quality.Epic:
                arrowNum = 4;
                backgroundColor = epicColor;
                break;
            case Quality.Legend:
                arrowNum = 5;
                backgroundColor = legendColor;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(quality), quality, null);
        }

        powerUp.frontBackground.color = backgroundColor;
        powerUp.backBackground.color = backgroundColor;
        foreach (var content in powerUp.contents)
        {
            if (content.positive)
            {
                for (int i = 0; i < arrowNum; i++)
                {
                    content.upArrows[i].gameObject.SetActive(true);
                }
            }
            else
            {
                content.downArrow.gameObject.SetActive(true);
            }
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
        powerUp.Reset();
        switch (type)
        {
            case PowerUpType.QiangKou:
                powerUp.scatterAngleModifier -= score;
                powerUp.bulletSpeedModifier += score;

                powerUp.damageModifier -= 6;

                powerUp.ActiveContent(3);
                powerUp.contents[0].contentDescription.text = "弹道控制";
                powerUp.contents[0].positive = true;
                powerUp.contents[1].contentDescription.text = "弹道速度";
                powerUp.contents[1].positive = true;
                powerUp.contents[2].contentDescription.text = "子弹伤害";
                powerUp.contents[2].positive = false;

                break;
            case PowerUpType.QiangGuan:
                powerUp.fireRateModifier -= score;
                powerUp.bulletSpeedModifier += score;

                powerUp.scatterAngleModifier += 10;

                powerUp.ActiveContent(3);
                powerUp.contents[0].contentDescription.text = "射速";
                powerUp.contents[0].positive = true;
                powerUp.contents[1].contentDescription.text = "弹道速度";
                powerUp.contents[1].positive = true;
                powerUp.contents[2].contentDescription.text = "弹道控制";
                powerUp.contents[2].positive = false;

                break;
            case PowerUpType.QiangTuo:
                powerUp.moveSpeedModifier += score;

                powerUp.scatterAngleModifier += 10;

                powerUp.ActiveContent(2);
                powerUp.contents[0].contentDescription.text = "移动速度";
                powerUp.contents[0].positive = true;
                powerUp.contents[1].contentDescription.text = "弹道控制";
                powerUp.contents[1].positive = false;

                break;
            case PowerUpType.DanJia:
                powerUp.ammoModifier += score;
                powerUp.reloadTimeModifier -= score;

                powerUp.moveSpeedModifier -= 10;

                powerUp.ActiveContent(3);
                powerUp.contents[0].contentDescription.text = "弹夹上限";
                powerUp.contents[0].positive = true;
                powerUp.contents[1].contentDescription.text = "换弹时间";
                powerUp.contents[1].positive = true;
                powerUp.contents[2].contentDescription.text = "移动速度";
                powerUp.contents[2].positive = false;

                break;
            case PowerUpType.DanYao:
                powerUp.damageModifier += score;

                powerUp.reloadTimeModifier += 10;

                powerUp.ActiveContent(2);
                powerUp.contents[0].contentDescription.text = "子弹伤害";
                powerUp.contents[0].positive = true;
                powerUp.contents[1].contentDescription.text = "换弹时间";
                powerUp.contents[1].positive = false;

                break;
            case PowerUpType.WoBa:
                powerUp.scatterAngleModifier -= score;
                powerUp.fireRateModifier -= score;

                powerUp.moveSpeedModifier -= 10;

                powerUp.ActiveContent(3);
                powerUp.contents[0].contentDescription.text = "弹道控制";
                powerUp.contents[0].positive = true;
                powerUp.contents[1].contentDescription.text = "射速";
                powerUp.contents[1].positive = true;
                powerUp.contents[2].contentDescription.text = "移动速度";
                powerUp.contents[2].positive = false;

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private Quality DetermineQuality(float score)
    {
        if (score >= 40) return Quality.Legend;
        else if (score >= 30) return Quality.Epic;
        else if (score >= 20) return Quality.Rare;
        else if (score >= 10) return Quality.UnCommon;
        else return Quality.Common;
    }
}