using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
