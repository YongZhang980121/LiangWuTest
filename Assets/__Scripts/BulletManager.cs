using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletManager : MonoBehaviour
{
    public Bullet rifleBulletPrefab;
    private ObjectPool<Bullet> rifleBulletPool;

    private void Awake()
    {
        Global.bulletManager = this;
    }

    private void Start()
    {
        rifleBulletPool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(rifleBulletPrefab, transform),
            actionOnGet: bullet => bullet.gameObject.SetActive(true),
            actionOnRelease: bullet => bullet.gameObject.SetActive(false),
            actionOnDestroy: bullet => Destroy(bullet.gameObject),
            collectionCheck: false,
            defaultCapacity: 30,
            maxSize: 60
        );
    }

    public Bullet GetRifleBullet()
    {
        var rifleBullet = rifleBulletPool.Get();
        return rifleBullet;
    }

    public void ReturnRifleBulletToPool(Bullet bullet)
    {
        rifleBulletPool.Release(bullet);
    }
}
