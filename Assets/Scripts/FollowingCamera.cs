using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    // Reference to the player object
    public Transform player;

    // Offset from the player's position (optional)
    public Vector3 offset = new Vector3(0, 0, -10);

    // Smoothing factor for camera movement
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player transform not assigned.");
            return;
        }

        // Target position based on player's position plus offset
        Vector3 targetPosition = player.position + offset;

        // Smoothly interpolate between current position and target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;
    }
}
