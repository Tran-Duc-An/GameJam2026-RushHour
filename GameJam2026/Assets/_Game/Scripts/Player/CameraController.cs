using UnityEngine;

public class CameraDeadzone : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Tuning")]
    [Tooltip("The size of the box. Player can move freely inside this.")]
    public Vector2 deadzoneSize = new Vector2(3f, 2f);

    [Tooltip("How fast the camera catches up when pushed. 1 = Instant.")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.5f;

    // Keep the camera's Z position (-10) so we don't clip
    private float zPosition = -10f;

    void Start()
    {
        // Remember the initial Z position
        zPosition = transform.position.z;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Start with the current camera position
        Vector3 finalPos = transform.position;

        // --- X Axis Calculation ---
        // Distance between player and camera center
        float diffX = player.position.x - transform.position.x;
        
        // If player is too far right...
        if (diffX > deadzoneSize.x)
        {
            finalPos.x = player.position.x - deadzoneSize.x;
        }
        // If player is too far left...
        else if (diffX < -deadzoneSize.x)
        {
            finalPos.x = player.position.x + deadzoneSize.x;
        }

        // --- Y Axis Calculation ---
        float diffY = player.position.y - transform.position.y;

        // If player is too far up...
        if (diffY > deadzoneSize.y)
        {
            finalPos.y = player.position.y - deadzoneSize.y;
        }
        // If player is too far down...
        else if (diffY < -deadzoneSize.y)
        {
            finalPos.y = player.position.y + deadzoneSize.y;
        }

        // Apply Movement
        // We use Lerp to make the "push" feel slightly weighted, not robotic
        Vector3 smoothPos = Vector3.Lerp(transform.position, finalPos, smoothSpeed);
        
        // Force Z to stay correct
        smoothPos.z = zPosition;
        
        transform.position = smoothPos;
    }

    // This draws the Red Box in the Scene view!
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Draw a wire cube representing the deadzone
        // Multiply by 2 because the variable is "distance from center"
        Gizmos.DrawWireCube(transform.position, new Vector3(deadzoneSize.x * 2, deadzoneSize.y * 2, 0));
    }
}