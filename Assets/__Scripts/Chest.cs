using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public TMP_Text valueText;
    public Transform reloadTransform;
    public Image reloadProgress;
    public CanvasGroup reloadCanvasGroup;
    public bool inCoolDown;
    private void Awake()
    {
        Global.chest = this;
    }

    private void Start()
    {
        reloadTransform.gameObject.SetActive(false);
        inCoolDown = false;
    }

    public void UpdateValueText()
    {
        valueText.text = "当前价值：" + (int)Global.score;
    }

    public void StartReload()
    {
        inCoolDown = true;
        reloadCanvasGroup.DOKill();
        reloadCanvasGroup.alpha = 1;
        reloadTransform.gameObject.SetActive(true);
        reloadProgress.fillAmount = 0f;
        DOTween.To(() => reloadProgress.fillAmount, x => reloadProgress.fillAmount = x, 1.0f, 10f).SetEase(Ease.Linear).OnComplete(
            () =>
            {
                inCoolDown = false;
                reloadCanvasGroup.DOFade(0f, 0.3f).From(1f).OnComplete(() =>
                {
                    reloadTransform.gameObject.SetActive(false);
                });
            });
    }

    public void GeneratePowerUp()
    {
        if (inCoolDown) return;
        if (Global.score <= 0) return; 
        Global.powerUpManager.TryGeneratePowerUp();
        
    }
    
    
}
