using UnityEngine;

public class DetectionPoint : MonoBehaviour {

    /// <summary>
    /// Méthode qui est appelée à chaque fois qu'une collision est détectée avec le but du panier
    /// <param name="other">L'autre objet qui est rentré dans le but</param>
    /// </summary>
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")) {

            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            Ball ball = other.GetComponent<Ball>();
            if (ballRb != null && ballRb.linearVelocity.y < 0) {
                if (ball.GetDispawn()) return;

                GameMaster.Instance.OnScore();
                ball.OnScore();
            }
        }
    }
}