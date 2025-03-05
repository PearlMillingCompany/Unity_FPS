using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;          // Reference to the player
    public float mouseSensitivity = 100f;
    public float distanceFromPlayer = 5f; // Distance behind the player
    public float heightOffset = 2f;   // Height above the player
    public float smoothTime = 0.1f;   // Smoothing time for camera movement

    private float xRotation = 0f;
    private float yRotation = 0f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the camera around the player
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 70f); // Limit vertical rotation

        // Calculate camera position and rotation
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Vector3 offset = new Vector3(0, heightOffset, -distanceFromPlayer);
        Vector3 desiredPosition = player.position + rotation * offset;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.LookAt(player.position + Vector3.up * heightOffset);
    }
}