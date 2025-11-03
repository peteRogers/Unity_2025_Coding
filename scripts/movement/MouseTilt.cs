using UnityEngine;


public class MouseTilt : MonoBehaviour
{
    public float tiltAmount = 10f; // Maximum tilt angle
    public float smoothSpeed = 5f; // Smoothing speed

    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void Update()
    {
        // Get mouse position as a percentage of screen width and height
        float mouseX = (Input.mousePosition.x / screenWidth) * 2 - 1; // Range -1 to 1
        float mouseY = (Input.mousePosition.y / screenHeight) * 2 - 1; // Range -1 to 1

        // Calculate tilt angles
        float tiltX = mouseX * tiltAmount; // Invert Y for natural tilt
        float tiltZ = mouseY * tiltAmount;

        // Smoothly interpolate rotation
        Quaternion targetRotation = Quaternion.Euler(tiltX, 0, tiltZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}