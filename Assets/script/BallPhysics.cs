using UnityEngine;

public class BouncingBall : MonoBehaviour

{

    public float bounceForce = 5f;

    public float damping = 0.5f;

    private Rigidbody rb;

    void Start()

    {

        rb = GetComponent<Rigidbody>();

    }

    void Update()

    {

        // Simulate gravity and bounce effect

        if (transform.position.y <= 0.5f && rb.linearVelocity.y < 0)

        {

            // Apply a bounce force in the upward direction

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, bounceForce, rb.linearVelocity.z);

        }

    }

    void OnCollisionEnter(Collision collision)

    {

        // Dampen the bounce when the ball hits the ground

        if (collision.relativeVelocity.y > 0)

        {

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * damping, rb.linearVelocity.z);

        }

    }

}
