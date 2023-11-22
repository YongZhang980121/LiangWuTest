using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("obs"))
        {
            Global.chest.GeneratePowerUp();
        }
        if (col.gameObject.CompareTag("Enemy"))
        {
            Global.playerHp -= 1;
            col.gameObject.GetComponent<Enemy>().Kill();
            Global.mmfManager.playerGetHit.PlayFeedbacks();
            if (Global.playerHp <= 0)
            {
                Debug.Log("gameover");
            }
        }
    }
}
