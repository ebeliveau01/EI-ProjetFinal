using UnityEngine;

public class AI : MonoBehaviour {
    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private Transform shootPoint;
    
    [SerializeField]
    private Transform targetHoop;
    
    private enum Difficulty { MEDIUM, HARD, IMPOSSIBLE }

    [SerializeField]
    private Difficulty difficulty = Difficulty.MEDIUM;

    private void Start() {
        // Invoke(nameof(ShootBall), 5f);
    }

    public void ShootBall() {
        GameObject ball = Instantiate(ballPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();

        Vector3 error = GetError();
        Vector3 targetPosition = targetHoop.position + error;

        Vector3 velocity = CalculateLaunchVelocity(targetPosition, true);

        // TODO => Test which one is better to use
        // ballRb.linearVelocity = velocity;
        ballRb.AddForce(velocity, ForceMode.VelocityChange);
    }

    private Vector3 GetError() {
        float errorRange = 0f;

        switch (difficulty) {
            case Difficulty.MEDIUM:
                errorRange = 0.5f;
                break;
            case Difficulty.HARD:
                errorRange = 0.2f;
                break;
            case Difficulty.IMPOSSIBLE:
                errorRange = 0.05f;
                break;
        }

        return new Vector3(
            Random.Range(-errorRange, errorRange),
            Random.Range(-errorRange * 0.5f, errorRange * 0.5f),
            Random.Range(-errorRange, errorRange)
        );
    }

    /// <summary>
    /// Calcul la vélocité de lancement du ballon de l'ia selon la position du panier et la position initial de lancement
    /// 
    /// <credit> Les formules de trajection, de calcul d'angle et de vitesse ont été donnée par OpenAI. (2025). ChatGPT GPT-4o (version 20 mars 2025) [Modèle massif de langage]. https://chat.openai.com/chat
    ///     Puis je les est adaptées pour ma situation
    /// </credit>
    /// </summary>
    /// <param name="target">La position choisie par l'ia pour lancer le ballon</param>
    /// <param name="preferHighArc">Quelle sorte de trajectoire à utiliser selon la difficulté de l'ia, haute ou basse</param>
    /// <returns>La vélocité qui permet au ballon d'atteindre un point choisie par l'ia</returns>
    private Vector3 CalculateLaunchVelocity(Vector3 target, bool preferHighArc) {
        Vector3 toTarget = target - shootPoint.position;
        Vector3 toTargetXZ = new Vector3(toTarget.x, 0, toTarget.z);

        float y = toTarget.y;
        float x = toTargetXZ.magnitude;
        float gravity = Mathf.Abs(Physics.gravity.y);

        float speedSquared = gravity * (x * x) / (x - y);
        if (speedSquared < 0) {
            Debug.LogWarning("Target too high or too close. Impossible shot!");
            return Vector3.zero;
        }

        float launchSpeed = Mathf.Sqrt(speedSquared);

        float underTheSqrt = speedSquared * speedSquared - gravity * (gravity * x * x + 2 * y * speedSquared);

        if (underTheSqrt < 0) {
            Debug.LogWarning("Target too far or speed too low!");
            return Vector3.zero;
        }

        float sqrt = Mathf.Sqrt(underTheSqrt);

        float angleHigh = Mathf.Atan((speedSquared + sqrt) / (gravity * x));

        float chosenAngle = angleHigh;

        Vector3 velocity = toTargetXZ.normalized * launchSpeed * Mathf.Cos(chosenAngle);
        velocity.y = launchSpeed * Mathf.Sin(chosenAngle);

        return velocity;
    }

    /* ****** Pour le Debugging ****** */
    [SerializeField]
    private uint gizmoPointCount = 20;
    [SerializeField]
    private Color gizmoColor = Color.red;

    private void OnDrawGizmosSelected() {
        if (targetHoop == null || shootPoint == null)
            return;

        Gizmos.color = gizmoColor;

        for (int i = 0; i < gizmoPointCount; i++) {
            Vector3 error = GetError();
            Vector3 possibleTarget = targetHoop.position + error;
            Gizmos.DrawSphere(possibleTarget, 0.1f);
        }

        Gizmos.color = Color.green;
        DrawTrajectory(shootPoint.position, targetHoop.position);
    }

    private void DrawTrajectory(Vector3 startPoint, Vector3 targetPoint) {
        Vector3 velocity = CalculateLaunchVelocity(targetPoint, true);

        Vector3 previousPoint = startPoint;
        float simulationTimeStep = 0.05f;
        int maxSteps = 300;

        for (int i = 1; i < maxSteps; i++) {
            float time = i * simulationTimeStep;
            Vector3 displacement = velocity * time + 0.5f * Physics.gravity * time * time;
            Vector3 currentPoint = startPoint + displacement;

            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;

            if (currentPoint.y < 0)
                break;
        }
    }
}