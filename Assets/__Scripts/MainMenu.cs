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
        if (Global.fromBattleToMenu)
        {
            ReturnToMenu();
        }
        Global.fromBattleToMenu = false;
        Global.fromMenuToBattle = false;
    }

    private void Start()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(StartGame);
        cover.alpha = 0f;
    }

    public void ReturnToMenu()
    {
        cover.DOFade(0f, 0.5f).From(1f).SetEase(Ease.Linear);
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
