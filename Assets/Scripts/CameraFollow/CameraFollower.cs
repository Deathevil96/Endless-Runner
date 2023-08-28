using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;           // The target to follow (usually the player)
    public Vector3 offset = new Vector3(0f, 2f, -10f);  // Offset from the target
    public float smoothSpeed = 0.1f;   // Smoothing factor for camera movement

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate the desired position based on the target's position and offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the new position to the camera
        transform.position = smoothedPosition;

        // Look at the target's position
        transform.LookAt(target);
    }
}
