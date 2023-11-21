using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    public string powerUpName;
    public float damageModifier;
    public float scatterAngleModifier;
    public float bulletSpeedModifier;
    public float reloadTimeModifier;
    public float moveSpeedModifier;
    public float fireRateModifier;
    public float ammoModifier;

    public Transform front;
    public Transform back;
    public List<Content> contents;
    public Image frontBackground;
    public Image backBackground;

    public void Reset()
    {
        foreach (var content in contents)
        {
            content.Reset();
            content.gameObject.SetActive(false);
        }
    }

    public void ActiveContent(int num)
    {
        if (num >= contents.Count)
        {
            foreach (var content in contents)
            {
                content.gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < num; i++)
            {
                contents[i].gameObject.SetActive(true);
            }
        }
    }

    private void Start()
    {
        DOVirtual.DelayedCall(2f, () =>
        {
            Flip();
        });
    }

    public void ApplyData()
    {
        Global.rifleDamage = Mathf.Max(1, Global.rifleDamage/2 + damageModifier);
        
        float targetValue = 20f;
        float minValue = 0f;
        float maxModifier = scatterAngleModifier;
        float distanceToTarget = Mathf.Clamp01((targetValue - Global.scatterAngle) / targetValue);
        float actualScatterModifier = maxModifier * distanceToTarget;
        Global.scatterAngle = Mathf.Max(minValue, Global.scatterAngle + actualScatterModifier);

        Global.bulletSpeed += (bulletSpeedModifier * 50);
        Global.bulletSpeed = Mathf.Clamp(Global.bulletSpeed, 1000f, 8000f);

        float maxReloadTime = 4f;
        float minReloadTime = 0.1f;
        float distanceToNearestBoundaryReload = Mathf.Min(Global.rifleReloadTime - minReloadTime, maxReloadTime - Global.rifleReloadTime);
        float boundaryFactorReload = distanceToNearestBoundaryReload * distanceToNearestBoundaryReload;
        float actualReloadModifier = (reloadTimeModifier / 50f) * boundaryFactorReload;
        Global.rifleReloadTime += actualReloadModifier;
        Global.rifleReloadTime = Mathf.Clamp(Global.rifleReloadTime, minReloadTime, maxReloadTime);

        Global.moveSpeed += (moveSpeedModifier * 2);
        Global.moveSpeed = Mathf.Clamp(Global.moveSpeed, 300, 800);
        
        float maxFireRate = 0.5f;
        float minFireRate = 0.1f;
        float distanceToNearestBoundaryFireRate = Mathf.Min(Global.rifleFireRate - minFireRate, maxFireRate - Global.rifleFireRate);
        float boundaryFactorFireRate = distanceToNearestBoundaryFireRate * distanceToNearestBoundaryFireRate;
        float actualModifierFireRate = (fireRateModifier / 400f) * boundaryFactorFireRate;
        Global.rifleFireRate += actualModifierFireRate;
        Global.rifleFireRate = Mathf.Clamp(Global.rifleFireRate, minFireRate, maxFireRate);

        Global.ammo += (int)(ammoModifier / 10);
        Global.ammo = Mathf.Max(1, Global.ammo);
    }
    
    

    public void Flip()
    {
        front.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
        back.DOScaleX(0, 0.5f).From(1f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            back.gameObject.SetActive(false);
            front.gameObject.SetActive(true);

            front.DOScaleX(1f, 0.5f).From(0f).SetEase(Ease.Linear).SetUpdate(true);
        });
    }
}
