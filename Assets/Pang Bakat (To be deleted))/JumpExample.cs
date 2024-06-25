using UnityEngine;

public class JumpExample : MonoBehaviour
{
    public float jumpForce = 10f; // Force applied when jumping
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check for jump input (e.g., spacebar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        // Add force to jump using Rigidbody
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
