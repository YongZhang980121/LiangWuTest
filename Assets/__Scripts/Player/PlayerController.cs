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
    public float moveSpeed;
    private bool jumping;
    public Transform spriteTransform;

    private void Awake()
    {
        input = new PlayerInput();
        jumping = false;
        // DOVirtual.DelayedCall(2f, () =>
        // {
        //     Debug.Log(123);
        //     spriteTransform.DOLocalJump(spriteTransform.localPosition, 30, 1, 0.5f);
        // });
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }
    
    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    private void Update()
    {
        rigidbody.velocity = moveVector * moveSpeed;
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
