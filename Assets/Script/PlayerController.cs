using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private PlayerConditions playerConditions;
    public event Action<InputAction.CallbackContext> JumpEvent;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    public bool isRunning;

    [Header("Look")]
    public Transform cameraContainer;
    public float lookRange = 85;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        playerConditions = GetComponent<PlayerConditions>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        player = GetComponent<Player>();
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
            CameraLook();
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (playerConditions.OnJumpStaminaCost())
            if (context.phase == InputActionPhase.Started && IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
                playerConditions.OnJumpStaminaCost();
                JumpEvent?.Invoke(context);
            }
        
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && player.condition.is_Tired == false)
        {
            isRunning = true;
            moveSpeed = 8;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isRunning = false;
            moveSpeed = 5;
        }
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, -lookRange, lookRange);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward* 0.5f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward* 0.5f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right* 0.5f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right* 0.5f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.65f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}
