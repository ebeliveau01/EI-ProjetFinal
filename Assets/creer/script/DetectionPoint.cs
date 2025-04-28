using UnityEngine;

public class DetectionPoint : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Score valide!");

        if (other.CompareTag("Ball")) {
            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            if (ballRb != null && ballRb.linearVelocity.y < 0)
                Debug.Log("Score valide!");
            else
                Debug.Log("Score Invalide!");
        }
    }
}