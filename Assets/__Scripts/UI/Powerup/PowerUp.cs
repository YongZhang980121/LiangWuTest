using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    public TMP_Text powerUpName;
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
    public Button button;
    public bool chosen;

    private void Awake()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Onclick);
        button.interactable = false;
    }

    public void Reset()
    {
        foreach (var content in contents)
        {
            content.Reset();
            content.gameObject.SetActive(false);
        }

        chosen = false;
        damageModifier = 0f;
        scatterAngleModifier = 0f;
        bulletSpeedModifier = 0f;
        reloadTimeModifier = 0f;
        moveSpeedModifier = 0f;
        fireRateModifier = 0f;
        ammoModifier = 0f;
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
        
    }

    public void Show()
    {
        front.gameObject.SetActive(false);
        front.DOScale(1f, 0f);
        back.gameObject.SetActive(true);
        back.DOLocalMoveY(back.localPosition.y, 0.75f).From(back.localPosition.y - 500f).SetUpdate(true).SetEase(Ease.OutBack);
        back.DOScale(1f, 0.75f).From(0f).SetUpdate(true).SetEase(Ease.OutBack).OnComplete(() =>
        {
            Flip();
        });
    }

    public void ApplyData()
    {
        Global.rifleDamage = Mathf.Max(1, Global.rifleDamage + damageModifier / 2);
        
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

        Global.ammo += Mathf.Max(1, (int)(ammoModifier / 10));
        Global.ammo = Mathf.Max(1, Global.ammo);
        
        Global.uiManager.UpdateMaxAmmo();
    }

    public void Onclick()
    {
        chosen = true;
        ApplyData();
        Global.score = 0;
        Global.chest.UpdateValueText();
        front.DOScale(1.2f, 0.5f).SetEase(Ease.OutBack).SetUpdate(true).OnComplete(() =>
        {
            front.DOScale(0f, 0.5f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
            {
                Global.chest.StartReload();
                Time.timeScale = 1f;
                Global.playerController.input.Enable();
            });
        });
        Global.powerUpManager.Onclick();
    }

    public void Disappear()
    {
        if (!chosen)
        {
            front.DOScale(0f, 0.5f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
    
    

    public void Flip()
    {
        front.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
        back.DOScaleX(0, 0.25f).From(1f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            back.gameObject.SetActive(false);
            front.gameObject.SetActive(true);

            front.DOScaleX(1f, 0.25f).From(0f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                button.interactable = true;
            });
        });
    }
}
