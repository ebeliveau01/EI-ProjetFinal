using System.Collections;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    public static GameMaster Instance;

    private int playerScore = 0;
    private int aiScore = 0;

    [SerializeField]
    private int score2Win = 21;

    private bool playerTurn = true;
    private bool gameActive = true;

    [SerializeField]
    private Transform playerTransform;
    
    [SerializeField]
    private GameObject ballPrefab;
    
    [SerializeField]
    private AI ai;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        StartGame();
    }

    private void StartGame() {
        playerScore = 0;
        aiScore = 0;

        playerTurn = true;
        gameActive = true;
        StartNextTurn();
    }

    public void OnScore(bool real) {
        if (!gameActive) return;

        if (real) {
            if (playerTurn) {
                playerScore++;
                PointDashboard.Instance.IncrementerPoint(1);
            }
            else {
                aiScore++;
                PointDashboard.Instance.IncrementerPoint(2);
            }
            
            CheckWinCondition();
        }

        playerTurn = !playerTurn;
        StartNextTurn();
    }

    private void CheckWinCondition() {
        if (playerScore >= score2Win) {
            gameActive = false;
            Debug.Log("Player Wins!");
        }
        else if (aiScore >= score2Win) {
            gameActive = false;
            Debug.Log("AI Wins!");
        }
    }

    private void StartNextTurn() {
        if (!gameActive) return;

        if (playerTurn) {
            Transform spawnPoint = playerTransform;
            Vector3 forwardOffset = spawnPoint.forward * 0.3f;
            Vector3 spawnPosition = spawnPoint.position + forwardOffset;

            GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Player's turn!");
        }
        else {
            StartCoroutine(DelayedAIShoot(1f));
            Debug.Log("AI's turn!");
        }
    }

    private IEnumerator DelayedAIShoot(float delay) {
        yield return new WaitForSeconds(delay);
        ai.ShootBall();
    }
}