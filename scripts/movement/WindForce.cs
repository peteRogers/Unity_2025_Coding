using UnityEngine;

public class WindForce : MonoBehaviour
{
    [Header("Wind Settings")]
    public Vector3 windDirection = new Vector3(1, 0, 0); // Direction of wind
    public float windStrength = 10f; // Base strength of the wind
    public float turbulenceStrength = 2f; // Random variation in wind force
    public bool applyVerticalForce = false; // Option to include vertical (Y-axis) force

    [Header("Visualization")]
    public bool showGizmos = true; // Show the wind zone in the Scene view

    void OnTriggerStay(Collider other)
    {
        // Check if the object has a Rigidbody
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            // Calculate the random turbulence
            Vector3 turbulence = new Vector3(
                Random.Range(-turbulenceStrength, turbulenceStrength),
                applyVerticalForce ? Random.Range(-turbulenceStrength, turbulenceStrength) : 0,
                Random.Range(-turbulenceStrength, turbulenceStrength)
            );

            // Apply the wind force to the Rigidbody
            Vector3 totalForce = (windDirection + turbulence) * windStrength;
            rb.AddForce(totalForce, ForceMode.Force); // ForceMode can be adjusted
        }
    }

    // Optional: Visualize the wind zone in the editor
    void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = new Color(0.5f, 0.8f, 1.0f, 0.5f);
            Gizmos.DrawCube(transform.position, transform.localScale);
        }
    }
}
