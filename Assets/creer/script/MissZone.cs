using UnityEngine;

public class MissZone : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")) {

            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            Ball ball = other.GetComponent<Ball>();
            if (ballRb != null) {
                if (ball.GetDispawn()) return;
                Debug.Log("Better chance later!");
                GameMaster.Instance.StartNextTurnMiss();
                ball.OnMiss();
            }
            else
                Debug.Log("Invalid Score!");
        }
    }
}