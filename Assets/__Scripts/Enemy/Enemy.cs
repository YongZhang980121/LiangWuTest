using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float health;
    private Sequence wobbleSequence;
    public Image spriteImage;
    private Material originalMaterial;
    public EnemyAI enemyAI;
    private Coroutine slimeCoroutine;
    public Transform slimePosition;
    public Tween KnockbackTween { get; set; }
    private void OnEnable()
    {
        health = Global.enemyHp;
        ResetAndStartWobbleAnimation();
        originalMaterial = spriteImage.material;
        spriteImage.material = new Material(originalMaterial);
        spriteImage.material.SetFloat("_Glow", 0f);
        spriteImage.material.SetFloat("_FadeAmount", -0.1f);
        ResetObjectState();
        enemyAI.enabled = true;
        slimeCoroutine = StartCoroutine(SpawnSlimeRoutine());
    }

    private void OnDisable()
    {
        if (wobbleSequence != null)
        {
            wobbleSequence.Kill();
        }

        spriteImage.material = originalMaterial;
        spriteImage.material.SetFloat("_Glow", 0f);
        spriteImage.material.SetFloat("_FadeAmount", -0.1f);
        KnockbackTween.Kill();
        if (slimeCoroutine != null)
        {
            StopCoroutine(slimeCoroutine);
        }
    }
    
    private void ResetAndStartWobbleAnimation()
    {
        spriteImage.transform.localScale = Vector3.one;

        if (wobbleSequence != null)
        {
            wobbleSequence.Kill();
        }
        wobbleSequence = DOTween.Sequence();
        wobbleSequence.Append(spriteImage.transform.DOScale(new Vector3(1.1f, 0.9f, 1f), 0.5f))
            .Append(spriteImage.transform.DOScale(new Vector3(0.9f, 1.1f, 1f), 0.5f))
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        if (slimeCoroutine != null)
        {
            StopCoroutine(slimeCoroutine);
        }
        MeltAndDisappear();
    }
    
    public void MeltAndDisappear()
    {
        // 禁用 Rigidbody2D 和 Collider
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.isKinematic = true;

        var collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        // 创建一个序列用于融化效果
        Sequence meltSequence = DOTween.Sequence();

        // 淡出效果
        meltSequence.Append(spriteImage.DOFade(0f, 1f)); // 淡出

        // 同时改变 _FadeAmount 从0到1
        meltSequence.Join(DOTween.To(() => spriteImage.material.GetFloat("_FadeAmount"), x => spriteImage.material.SetFloat("_FadeAmount", x), 1f, 1f));

        // 融化完毕后的回调
        meltSequence.OnComplete(() => 
        {
            // 重置对象状态
            ResetObjectState();

            // 返回到池中或销毁
            Global.enemyManager.ReturnEnemyToPool(this);
        });
    }

    private void ResetObjectState()
    {
        // 停止所有与此对象关联的DoTween动画
        spriteImage.DOKill();

        // 重置透明度
        spriteImage.color = new Color(spriteImage.color.r, spriteImage.color.g, spriteImage.color.b, 1f);

        // 启用 Rigidbody2D 和 Collider
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.isKinematic = false;

        var collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = true;

        // 重置其他可能被改变的状态
        // ...
    }
    
    IEnumerator SpawnSlimeRoutine()
    {
        while (true)
        {
            var randomInterval = Random.Range(0.2f, 0.5f);
            yield return new WaitForSeconds(randomInterval); // 每3秒生成一次
            SpawnSlime();
        }
    }

    private void SpawnSlime()
    {
        GameObject slime = Global.enemyManager.GetSlime();
        slime.transform.SetParent(Global.battleManager.slimeHolder);
        slime.transform.position = slimePosition.position; // 设置粘液的位置
        slime.SetActive(true);
    }
}
