using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    private void OnEnable()
    {
        health = Global.enemyHp;
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
        Global.enemyManager.ReturnEnemyToPool(this);
    }
}
