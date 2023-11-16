using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed;
    public float fireRate;
    public virtual void FireProjectile(float angle)
    {
        throw new System.NotImplementedException();
    }
}
