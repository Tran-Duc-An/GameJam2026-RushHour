using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour,ICooldown
{
    // Define the States
    private enum State { Normal, Rolling, Locked }
    private State currentState;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 moveInput;

    [Header("Dash / Roll")]
    public float rollSpeed = 15f;
    public float rollDuration = 0.2f;
    public float rollCooldown = 1f;
    [Tooltip("Reference to the Trail Renderer component")]
    public TrailRenderer tr;
    
    private float lastRollTime;
    private Vector2 rollDirection;

    void Start()
    {
        currentState = State.Normal;
    }

    void Update()
    {
        // 1. Check Inputs based on State
        switch (currentState)
        {
            case State.Normal:
                HandleInput();
                break;
            
            case State.Rolling:
                // Ignore movement input while rolling
                break;

            case State.Locked:
                // Cutscene or Death state
                break;
        }
        
        // 2. Animation (Optional)
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Normal:
                rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
                break;

            case State.Rolling:
                // Move very fast in the roll direction
                rb.MovePosition(rb.position + rollDirection * rollSpeed * Time.fixedDeltaTime);
                break;
        }
    }

    void HandleInput()
    {
        // Basic Movement
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(x, y).normalized;

        // Visual Flip
        if (moveInput.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);

        // Dash Input (Space or Shift)
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastRollTime + rollCooldown)
        {
            // Only roll if we are actually moving
            if (moveInput != Vector2.zero)
            {
                StartCoroutine(RollRoutine());
            }
        }
    }

    IEnumerator RollRoutine()
    {
        // 1. Enter Rolling State
        currentState = State.Rolling;
        rollDirection = moveInput;
        lastRollTime = Time.time;

        // 2. Smooth Trail Start
        if (tr != null) 
        {
            tr.Clear(); // Remove any leftover segments from the last dash
            tr.emitting = true;
        }

        yield return new WaitForSeconds(rollDuration);

        // 3. Smooth Trail End
        if (tr != null) 
        {
            tr.emitting = false; 
            // Note: The trail will naturally fade out based on its "Time" setting 
            // in the inspector rather than disappearing instantly.
        }
        
        rb.linearVelocity = Vector2.zero; 
        currentState = State.Normal;
    }

    void UpdateAnimation()
    {
        if (animator == null) return;
        
        animator.SetFloat("Speed", moveInput.sqrMagnitude);
        animator.SetBool("IsRolling", currentState == State.Rolling);
    }

    public float GetCooldownFactor()
    {
        // Calculate how much time has passed since the last roll
        float timeSinceRoll = Time.time - lastRollTime;
        
        // If we are past the cooldown time, return 0 (Empty fill = Ready)
        if (timeSinceRoll >= rollCooldown) return 0f;
        
        // Otherwise, return the percentage (1.0 = Just used, 0.5 = Halfway)
        return 1f - (timeSinceRoll / rollCooldown);
    }

    public bool IsReady()
    {
        return Time.time >= lastRollTime + rollCooldown;
    }

    public string GetAbilityName()
    {
        return "Dash";
    }
}