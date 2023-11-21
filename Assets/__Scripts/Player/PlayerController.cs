using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput input;
    private Vector2 moveVector = Vector2.zero;
    public new Rigidbody2D rigidbody;
    private bool jumping;
    public Transform spriteTransform;
    public WeaponController weaponController;
    private Coroutine firingCoroutine;

    private void Awake()
    {
        input = new PlayerInput();
        jumping = false;
        Global.playerController = this;
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
        input.Player.Fire.performed += OnFireClick;
        input.Player.Fire.canceled += OnFireRelease;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
        input.Player.Fire.performed -= OnFireClick;
        input.Player.Fire.canceled -= OnFireRelease;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }
    
    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    private void OnFireClick(InputAction.CallbackContext callbackContext)
    {
        if (firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
    }
    
    public void OnFireRelease(InputAction.CallbackContext callbackContext)
    {
        if (firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }
    
    private IEnumerator FireContinuously()
    {
        while (true)
        {
            weaponController.Fire();
            yield return new WaitForSeconds(Global.rifleFireRate);
        }
    }

    private void Update()
    {
        rigidbody.velocity = moveVector * Global.moveSpeed;
        UpdateCharacterFace(moveVector);
        if (moveVector.magnitude > 0)
        {
            if (!jumping)
            {
                jumping = true;
                spriteTransform.DOLocalJump(spriteTransform.localPosition, 10, 1, 0.25f).OnComplete(() =>
                {
                    jumping = false;
                });
            }
        }
    }

    private void UpdateCharacterFace(Vector2 inputVector)
    {
        if (inputVector.x > 0)
        {
            spriteTransform.localEulerAngles = new Vector3(0, 0f, 0);
        }
        
        if (inputVector.x < 0)
        {
            spriteTransform.localEulerAngles = new Vector3(0, 180f, 0);
        }
    }
}
