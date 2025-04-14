using UnityEngine;

public class BouncingBall : MonoBehaviour {
    public float bounceForce = 5f;
    public float damping = 0.5f;

    private Rigidbody rb;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (transform.position.y <= 0.5f && rb.linearVelocity.y < 0)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, bounceForce, rb.linearVelocity.z);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.y > 0)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * damping, rb.linearVelocity.z);
    }

}