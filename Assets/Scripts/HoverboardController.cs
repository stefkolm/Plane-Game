using UnityEngine;

public class HoverboardController : MonoBehaviour
{
    public Transform[] hoverPoints; // Array to store the contact points
    public float hoverHeight = 1f; // Desired hover height
    public float hoverForce = 10f;
    public float forwardSpeed = 5f;// Force to apply when hovering
    public LayerMask groundLayer; // Layer mask to specify what objects are considered as ground

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * forwardSpeed, ForceMode.VelocityChange);
        foreach (Transform point in hoverPoints)
        {
            RaycastHit hit;
            if (Physics.Raycast(point.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
            {
                // Draw debug raycast
                Debug.DrawRay(point.position, Vector3.down * hit.distance, Color.red);

                float distanceToGround = hoverHeight - hit.distance;
                if (distanceToGround > 0)
                {
                    Vector3 hoverForceVector = Vector3.up * hoverForce * distanceToGround;
                    rb.AddForceAtPosition(hoverForceVector, point.position);
                }
            }
            else
            {
                // Draw debug raycast
                Debug.DrawRay(point.position, Vector3.down * hoverHeight, Color.red);
            }
        }
    }
}
