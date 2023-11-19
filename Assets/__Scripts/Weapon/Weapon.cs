using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    public Bullet projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed;
    public float fireRate;
    public float kickbackDistance;
    public float kickbackDuration;
    private Coroutine gunKickCoroutine;
    public Transform weaponTransform;
    private Vector3 weaponStartPosition;
    public Transform ejectPoint;

    private void Awake()
    {
        weaponStartPosition = weaponTransform.localPosition;
    }

    public void Fire(float angle)
    {
        FireProjectile(angle);
        if (gunKickCoroutine != null)
        {
            StopCoroutine(gunKickCoroutine);
            weaponTransform.localPosition = weaponStartPosition;
        }
        gunKickCoroutine = StartCoroutine(GunKick(angle));
        
        
        GameObject shell = Global.bulletShellManager.GetShell();
        shell.transform.SetParent(Global.battleManager.rifleBulletHolder);

// 获取枪的旋转角度，转换为0到360度
        float gunRotation = transform.eulerAngles.z;
        if (gunRotation < 0) gunRotation += 360;

// 判断枪是否翻转
        bool isGunFlipped = gunRotation > 90 && gunRotation < 270;

// 根据枪是否翻转调整弹壳的起始位置和弹出方向
        Vector3 adjustedEjectionPoint = ejectPoint.position;
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1f), 0);

// 如果枪翻转，调整弹壳方向
        if (isGunFlipped)
        {
            randomDirection.x *= -1; // 反转X方向
            gunRotation = (gunRotation + 180) % 360; // 调整旋转角度以适应翻转
        }

// 将随机方向旋转以适应枪的旋转
        Quaternion rotation = Quaternion.Euler(0, 0, gunRotation);
        randomDirection = rotation * randomDirection;

        float randomRotation = Random.Range(-180f, 180f);
        shell.transform.position = adjustedEjectionPoint;
        shell.transform.DOJump(adjustedEjectionPoint + randomDirection * 100, 20f, 1, 0.3f)
            .OnComplete(() =>
            {
                shell.GetComponentInChildren<Image>().DOFade(0f, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Global.bulletShellManager.ReturnShell(shell);
                });
            });
        shell.transform.DORotate(new Vector3(0, 0, randomRotation), 0.3f);
        
        
    }
    public virtual void FireProjectile(float angle)
    {
        throw new System.NotImplementedException();
    }
    
    public virtual void Setup()
    {
        throw new System.NotImplementedException();
    }
    
    private IEnumerator GunKick(float angle)
    {
        Vector3 originalPosition = weaponTransform.localPosition;

        Vector3 kickDirection = new Vector3(-Mathf.Cos(angle * Mathf.Deg2Rad), -Mathf.Sin(angle * Mathf.Deg2Rad), 0);

        Vector3 kickbackPosition = originalPosition + kickDirection * kickbackDistance;

        float startTime = Time.time;
        while (Time.time < startTime + kickbackDuration / 2)
        {
            weaponTransform.localPosition = Vector3.Lerp(originalPosition, kickbackPosition, (Time.time - startTime) / (kickbackDuration / 2));
            yield return null;
        }

        startTime = Time.time;
        while (Time.time < startTime + kickbackDuration / 2)
        {
            weaponTransform.localPosition = Vector3.Lerp(kickbackPosition, originalPosition, (Time.time - startTime) / (kickbackDuration / 2));
            yield return null;
        }

        weaponTransform.localPosition = originalPosition;
    }
    
}
