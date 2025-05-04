using UnityEngine;

public class DetectionPoint : MonoBehaviour {
    [SerializeField]
    private bool realPoint;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")) {

            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            Ball ball = other.GetComponent<Ball>();
            if (ballRb != null && ballRb.linearVelocity.y < 0) {
                if (realPoint) Debug.Log("Valid Score!"); else Debug.Log("Better chance later!");
                GameMaster.Instance.OnScore(realPoint);
                ball.OnScore();
            }
            else
                Debug.Log("Invalid Score!");
        }
    }
}