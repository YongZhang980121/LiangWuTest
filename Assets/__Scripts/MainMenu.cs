using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button button;
    public CanvasGroup cover;

    private void Awake()
    {
        Global.fromBattleToMenu = false;
        Global.fromMenuToBattle = false;
    }

    private void Start()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(StartGame);
        cover.alpha = 0f;
    }

    public void StartGame()
    {
        cover.DOFade(1f, 0.5f).From(0f).SetEase(Ease.Linear).OnComplete(() =>
        {
            SceneManager.LoadScene("Battle");
            Global.fromMenuToBattle = true;
        });
    }
}
