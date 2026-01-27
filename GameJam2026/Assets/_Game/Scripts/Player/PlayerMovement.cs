using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("How fast the character moves.")]
    public float moveSpeed = 5f;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator; // Optional: Link this if you have animations later

    private Vector2 movement;

    // Update is called once per frame
    // We use this for processing Inputs (Key presses)
    void Update()
    {
        // Get Input (Returns -1, 0, or 1)
        // "Raw" makes the movement stop immediately when you let go (Snappy feel)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Prevent diagonal movement from being faster than straight movement
        movement = movement.normalized;

        // Optional: Simple sprite flipping based on direction
        if (movement.x > 0)
            transform.localScale = new Vector3(1, 1, 1); // Face Right
        else if (movement.x < 0)
            transform.localScale = new Vector3(-1, 1, 1); // Face Left

        // Optional: Send data to Animator
        if (animator != null)
        {
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    // FixedUpdate is called at a fixed interval
    // We use this for Physics calculations to ensure smooth collision
    void FixedUpdate()
    {
        // Move the Rigidbody
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}