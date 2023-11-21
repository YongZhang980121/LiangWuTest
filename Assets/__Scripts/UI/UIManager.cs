using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text currentAmmoText;
    public TMP_Text maxAmmoText;
    private void Awake()
    {
        Global.uiManager = this;
    }

    private void Start()
    {
        UpdateMaxAmmo();
    }

    public void UpdateCurrentAmmo(int currentAmmo)
    {
        currentAmmoText.text = currentAmmo.ToString();
    }

    public void UpdateMaxAmmo()
    {
        maxAmmoText.text = "Max " + Global.ammo.ToString();
    }
}
