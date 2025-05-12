using System.Collections;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    /// <summary>
    /// Un singleton static du script
    /// </summary>
    public static GameMaster Instance;

    /// <summary>
    /// Le score du joueur
    /// </summary>
    private int playerScore = 0;
    
    /// <summary>
    /// Le score de l'ia
    /// </summary>
    private int aiScore = 0;

    /// <summary>
    /// Le score qui est requis afin de gagner la partie
    /// </summary>
    [SerializeField]
    private int score2Win = 21;

    /// <summary>
    /// Variable qui permet de vérifier si c'est le tour au joueur de jouer
    /// </summary>
    private bool playerTurn = true;

    /// <summary>
    /// Variable qui permet de vérifier si le jeu est toujours actif
    /// </summary>
    private bool gameActive = true;

    /// <summary>
    /// La position du joueur dans l'espace
    /// </summary>
    [SerializeField]
    private Transform playerTransform;
    
    /// <summary>
    /// Le préfab de la ball de backetball
    /// </summary>
    [SerializeField]
    private GameObject ballPrefab;
    
    /// <summary>
    /// L'ia
    /// </summary>
    [SerializeField]
    private AI ai;

    /// <summary>
    /// La source d'audio
    /// </summary>
    [SerializeField]
    private AudioSource audio;

    /// <summary>
    /// Le composant qui permet au joueur de bouger
    /// </summary>
    private GameObject goInteractible;

    /// <summary>
    /// Méthode qui s'exécute au lancement du script
    /// </summary>
    private void Awake() {
        Instance = this;

        goInteractible = GameObject.FindGameObjectWithTag("mouvement");
    }

    /// <summary>
    /// Méthode qui s'exécute au lancement du jeu
    /// </summary>
    private void Start() {
        StartGame();

        if (goInteractible == null) return;

        goInteractible.SetActive(false);
    }

    /// <summary>
    /// Méthode qui permet de lancer le jeu
    /// </summary>
    private void StartGame() {
        playerScore = 0;
        aiScore = 0;

        playerTurn = true;
        gameActive = true;
        StartNextTurn();
    }

    /// <summary>
    /// Méthode qui est lancer à chaque fois qu'un but est détecter
    /// </summary>
    public void OnScore() {
        if (!gameActive) return;

        if (playerTurn) {
            playerScore++;
            PointDashboard.Instance.IncrementerPoint(1);
        }
        else {
            aiScore++;
            PointDashboard.Instance.IncrementerPoint(2);
        }
            
        CheckWinCondition();
        playerTurn = !playerTurn;
        audio.Play();
        StartNextTurn();
    }

    /// <summary>
    /// Méthode qui permet de valider si la condition de victoire à été atteinte par le joueur ou l'ia
    /// </summary>
    private void CheckWinCondition() {
        if (playerScore >= score2Win) {
            gameActive = false;
            EndGame.Instance.Show(true, playerScore, aiScore);
        }
        else if (aiScore >= score2Win) {
            gameActive = false;
            EndGame.Instance.Show(false, playerScore, aiScore);
        }

        goInteractible.SetActive(true);
    }

    /// <summary>
    /// Méthode qui permet de commencer le prochain tour lorsqu'un tir est manqué
    /// </summary>
    public void StartNextTurnMiss() {
        playerTurn = !playerTurn;
        StartNextTurn();
    }

    /// <summary>
    /// MÉthode qui permet de lancer le prochain tour lorsqu'un but est détecté
    /// </summary>
    private void StartNextTurn() {
        if (!gameActive) return;

        if (playerTurn) {
            Transform spawnPoint = playerTransform;
            Vector3 forwardOffset = spawnPoint.forward * 1f;
            Vector3 spawnPosition = spawnPoint.position + forwardOffset;

            spawnPosition.y = 0f;

            GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        }
        else {
            StartCoroutine(DelayedAIShoot(1f));
        }
    }
    
    /// <summary>
    /// Coroutine qui permet d'ajouter un délai dans le tir de l'ia au changement de tour
    /// </summary>
    private IEnumerator DelayedAIShoot(float delay) {
        yield return new WaitForSeconds(delay);
        ai.ShootBall();
    }
}