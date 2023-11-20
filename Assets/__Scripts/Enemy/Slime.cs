using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
    public Image slimeImage;

    void Awake()
    {
        float scaleMultiplier = Random.Range(1f, 1.5f);
        transform.localScale = new Vector3(1.2f * scaleMultiplier, 0.5f * scaleMultiplier, 1f);
    }

    void OnEnable()
    {// 设置为随机绿色
        float red = Random.Range(0f, 0.2f); // 红色通道设置得比较低
        float green = Random.Range(0.6f, 1f); // 绿色通道设置得比较高
        float blue = Random.Range(0f, 0.2f); // 蓝色通道设置得比较低

        slimeImage.color = new Color(red, green, blue, 1f); // 设置随机绿色
        FadeOutAndReturnToPool();
    }

    private void FadeOutAndReturnToPool()
    {
        var randomFadeDuration = Random.Range(0.8f, 1.2f);
        slimeImage.DOFade(0f, randomFadeDuration).OnComplete(() => Global.enemyManager.ReturnSlime(gameObject));
    }
}
