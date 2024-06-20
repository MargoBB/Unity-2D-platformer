using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;
    private Vector2 moveInput;
    private TouchingDirections touchingDirections;
    private Damageable damageable;

    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;
    public Button attackButton;
    public Button rangedAttackButton;

    private bool moveLeft;
    private bool moveRight;

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        return IsRunning ? runSpeed : walkSpeed;
                    }
                    else
                    {
                        return airWalkSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    public bool IsAlive
    {
        get { return animator.GetBool(AnimationStrings.isAlive); }
    }

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();


        AddButtonListeners(leftButton, () => moveLeft = true, () => moveLeft = false);
        AddButtonListeners(rightButton, () => moveRight = true, () => moveRight = false);
        jumpButton.onClick.AddListener(Jump);
        attackButton.onClick.AddListener(Attack);
        rangedAttackButton.onClick.AddListener(RangedAttack);
    }

    private void Update()
    {
        moveInput = Vector2.zero;
        if (moveLeft) moveInput.x = -1;
        if (moveRight) moveInput.x = 1;

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    private void Jump()
    {
        if (touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    private void Attack()
    {
        if (IsAlive)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    private void RangedAttack()
    {
        if (IsAlive)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    private void AddButtonListeners(Button button, Action onPointerDown, Action onPointerUp)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();

        var pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDownEntry.callback.AddListener((data) => onPointerDown());

        var pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUpEntry.callback.AddListener((data) => onPointerUp());

        trigger.triggers.Add(pointerDownEntry);
        trigger.triggers.Add(pointerUpEntry);
    }
}
