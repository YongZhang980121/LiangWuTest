using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text currentAmmoText;
    public TMP_Text maxAmmoText;
    public CanvasGroup cover;
    public Transform gameOverPage;
    public Button gameOverButton;
    private void Awake()
    {
        Global.uiManager = this;
        gameOverPage.gameObject.SetActive(false);
        gameOverButton.onClick.RemoveAllListeners();
        gameOverButton.onClick.AddListener(GameOverButton);
    }

    private void Start()
    {
        UpdateMaxAmmo();
    }

    public void GameOver()
    {
        gameOverPage.gameObject.SetActive(true);
        Time.timeScale = 0f;
        Global.playerController.input.Disable();
    }

    public void GameOverButton()
    {
        cover.DOFade(1f,0.5f).From(0f).SetUpdate(true).SetEase(Ease.Linear).OnComplete(() =>
        {
            Time.timeScale = 1f;
            gameOverButton.interactable = false;
            Global.fromBattleToMenu = true;
            SceneManager.LoadScene("Menu");
        });
    }

    public void UpdateCurrentAmmo(int currentAmmo)
    {
        currentAmmoText.text = currentAmmo.ToString();
    }

    public void UpdateMaxAmmo()
    {
        maxAmmoText.text = "Max " + Global.ammo.ToString();
    }

    public void Uncover()
    {
        cover.DOFade(0f, 0.5f).From(1f).SetEase(Ease.Linear);
    }
}
