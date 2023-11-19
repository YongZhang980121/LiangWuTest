using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class BulletShellManager : MonoBehaviour
{
    public GameObject shellPrefab;
    private ObjectPool<GameObject> shellPool;
    private void Awake()
    {
        Global.bulletShellManager = this;
    }

    void Start()
    {
        shellPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(shellPrefab),
            actionOnGet: (obj) =>
            {
                obj.SetActive(true);
                obj.GetComponentInChildren<Image>().DOFade(1f, 0f);
            },
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 20,
            maxSize: 40
        );
    }

    public GameObject GetShell()
    {
        return shellPool.Get();
    }

    public void ReturnShell(GameObject shell)
    {
        shellPool.Release(shell);
    }
}
