using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public Transform weaponTransform;
    public float rotationSpeed;
    private float currentAngle = 0f;

    private void Update()
    {
        Vector3 characterScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseInput = Input.mousePosition;
        Vector3 directionOnScreen = mouseInput - characterScreenPos;
        directionOnScreen.z = 0;
        var targetAngle = Mathf.Atan2(directionOnScreen.y, directionOnScreen.x) * Mathf.Rad2Deg;
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        weaponTransform.localEulerAngles = new Vector3(0f, 0f, currentAngle);
        
        weaponTransform.localScale = new Vector3(1f, (currentAngle > 90 || currentAngle < -90) ? -1f : 1f, 1f);
    }
}
