using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    public string powerUpName;
    public float damageModifier;
    public float scatterAngleModifier;
    public float bulletSpeedModifier;
    public float reloadTimeModifier;
    public float moveSpeedModifier;
    public float fireRateModifier;
    public float ammoModifier;

    public Transform front;
    public Transform back;
    public List<Content> contents;
    public Image frontBackground;
    public Image backBackground;

    public void Reset()
    {
        foreach (var content in contents)
        {
            content.Reset();
            content.gameObject.SetActive(false);
        }
    }

    public void ActiveContent(int num)
    {
        if (num >= contents.Count)
        {
            foreach (var content in contents)
            {
                content.gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < num; i++)
            {
                contents[i].gameObject.SetActive(true);
            }
        }
    }
    
}
