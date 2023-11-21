using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{

    public Transform weaponTransform;
    public float rotationSpeed;
    private float currentAngle = 0f;
    public Weapon currentWeapon;
    public CanvasGroup reloadCanvasGroup;
    public Transform reload;
    public Image reloadProgress;

    private void Awake()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        Global.weaponController = this;
        reload.gameObject.SetActive(false);
    }

    public void Reload(Action completeAction)
    {
        reloadCanvasGroup.DOKill();
        reloadCanvasGroup.alpha = 1;
        reload.gameObject.SetActive(true);
        reloadProgress.fillAmount = 0f;
        DOTween.To(() => reloadProgress.fillAmount, x => reloadProgress.fillAmount = x, 1.0f, Global.rifleReloadTime).SetEase(Ease.Linear).OnComplete(
            () =>
            {
                completeAction();
                reloadCanvasGroup.DOFade(0f, 0.3f).From(1f).OnComplete(() =>
                {
                    reload.gameObject.SetActive(false);
                });
            });
    }

    private void Update()
    {
        Vector3 characterScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseInput = Input.mousePosition;
        Vector3 directionOnScreen = mouseInput - characterScreenPos;
        directionOnScreen.z = 0;
        var targetAngle = Mathf.Atan2(directionOnScreen.y, directionOnScreen.x) * Mathf.Rad2Deg;
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        var tempCurrentAngle = Mathf.Repeat(currentAngle + 180f, 360f) - 180f;

        weaponTransform.localEulerAngles = new Vector3(0f, 0f, tempCurrentAngle);

        weaponTransform.localScale = new Vector3(1f, (tempCurrentAngle > 90 || tempCurrentAngle < -90) ? -1f : 1f, 1f);
    }
    
    public void Fire(){
        currentWeapon.Fire(currentAngle);
    }
}
