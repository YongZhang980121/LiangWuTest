using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public Transform weaponTransform;
    public float rotationSpeed;
    private float currentAngle = 0f;
    public Weapon currentWeapon;

    private void Awake()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        Global.weaponController = this;
    }

    private void Update()
    {
        Vector3 characterScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseInput = Input.mousePosition;
        Vector3 directionOnScreen = mouseInput - characterScreenPos;
        directionOnScreen.z = 0;
        var targetAngle = Mathf.Atan2(directionOnScreen.y, directionOnScreen.x) * Mathf.Rad2Deg;
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        var tempCurrentAngle = Mathf.Repeat(currentAngle + 180f, 360f) - 180f;

        weaponTransform.localEulerAngles = new Vector3(0f, 0f, tempCurrentAngle);

        weaponTransform.localScale = new Vector3(1f, (tempCurrentAngle > 90 || tempCurrentAngle < -90) ? -1f : 1f, 1f);
    }
    
    public void Fire(){
        currentWeapon.Fire(currentAngle);
    }
}
