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
        GenerateScore();
    }

    public void GenerateScore()
    {
        var score = Global.enemyManager.GetScore();
        score.transform.SetParent(Global.battleManager.scoreHolder);
        score.transform.position = transform.position;
        score.transform.DOScale(1f, 0.4f).From(0f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            var distance = Vector3.Distance(score.transform.position, Global.chest.transform.position);

            var duration = distance / 1000f;

            score.transform.DOMove(Global.chest.transform.position, duration).SetEase(Ease.InSine).OnComplete(() =>
            {
                Global.score += 1;
                Global.enemyManager.ReturnScore(score);
            });
        });
        
    }
    
    public void MeltAndDisappear()
    {
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.isKinematic = true;

        var collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        Sequence meltSequence = DOTween.Sequence();

        meltSequence.Append(spriteImage.DOFade(0f, 1f)); // 淡出

        meltSequence.Join(DOTween.To(() => spriteImage.material.GetFloat("_FadeAmount"), x => spriteImage.material.SetFloat("_FadeAmount", x), 1f, 1f));

        meltSequence.OnComplete(() => 
        {
            ResetObjectState();

            Global.enemyManager.ReturnEnemyToPool(this);
        });
    }

    private void ResetObjectState()
    {
        spriteImage.DOKill();

        spriteImage.color = new Color(spriteImage.color.r, spriteImage.color.g, spriteImage.color.b, 1f);

        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.isKinematic = false;

        var collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = true;

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
