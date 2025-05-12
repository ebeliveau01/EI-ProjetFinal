using UnityEngine;

public class MissZone : MonoBehaviour {
    
    /// <summary>
    /// Méthode qui est appelée à chaque fois qu'une collision est détectée avec le sol de la map
    /// <param name="other">L'autre objet qui est rentré en collision avec le sol</param>
    /// </summary>
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")) {

            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            Ball ball = other.GetComponent<Ball>();
            if (ballRb != null) {
                if (ball.GetDispawn()) return;

                GameMaster.Instance.StartNextTurnMiss();
                ball.OnMiss();
            }
        }
    }
}