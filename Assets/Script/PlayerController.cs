using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
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

    public Action UiInventory;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
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
        if (GameManager.Instance.Player.condition.OnJumpStaminaCost())
            if (context.phase == InputActionPhase.Started && IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
                GameManager.Instance.Player.condition.OnJumpStaminaCost();
                JumpEvent?.Invoke(context);
            }
        
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && GameManager.Instance.Player.condition.is_Tired == false)
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

    public void OnInventoryButton(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            UiInventory?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void OnEquipInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (context.control.name == "1")
                Equip(0);
            else if (context.control.name == "2")
                Equip(1);
            else if (context.control.name == "3")
                Equip(2);
            else if (context.control.name == "4")
                Equip(3);
        }
    }

    void Equip(int input)
    {
        for (int i = 0; i < GameManager.Instance.Player.equipInventory.slots.Length; i++)
        {
            GameManager.Instance.Player.equipInventory.slots[i].outline.enabled = false;
        }

        if (GameManager.Instance.Player.equipInventory.slots[input].item != null)
        {
            Debug.Log($"{input}번째 아이템 장착");
            GameManager.Instance.Player.equipment.UnEquip();
            GameManager.Instance.Player.equipment.EquipNew(GameManager.Instance.Player.equipInventory.slots[input].item);
            GameManager.Instance.Player.equipInventory.slots[input].outline.enabled = true;
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
