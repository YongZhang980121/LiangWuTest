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
            Debug.Log("powerup");
            Global.chest.GeneratePowerUp();
        }
    }
}
