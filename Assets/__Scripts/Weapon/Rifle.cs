using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rifle : Weapon
{
    public override void FireProjectile(float angle)
    {
        float scatter = Random.Range(-Global.scatterAngle, Global.scatterAngle);
        float adjustedAngle = angle + scatter;

        Bullet projectile = Global.bulletManager.GetRifleBullet();
        projectile.transform.SetParent(Global.battleManager.rifleBulletHolder);
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = Quaternion.Euler(0, 0, adjustedAngle);
    
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector2 firingDirection = new Vector2(Mathf.Cos(adjustedAngle * Mathf.Deg2Rad), Mathf.Sin(adjustedAngle * Mathf.Deg2Rad));
        rb.velocity = firingDirection * projectileSpeed;
        
    }

}
