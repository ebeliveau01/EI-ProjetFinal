using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGame : MonoBehaviour {
    /// <summary>
    /// L'objet qui contient le texte afin d'afficher le status final du jeu
    /// </summary>
    [SerializeField]
    private GameObject statusGame_Object;

    /// <summary>
    /// Le texte qui affiche le status final du jeu
    /// </summary>
    private TextMeshProUGUI statusGame;

    /// <summary>
    /// L'objet qui contient le texte afin d'affcher le score final du jeu
    /// </summary>
    [SerializeField]
    private GameObject finalScore_Object;

    /// <summary>
    /// Le texte qui affiche le score final du jeu
    /// </summary>
    private TextMeshProUGUI finalScore;

    /// <summary>
    /// Un singleton static du script
    /// </summary>
    public static EndGame Instance;

    /// <summary>
    /// Méthode qui est appelé lors du début du jeu
    /// </summary>
    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Méthode qui est appelé lorsque le jeu débute réellement
    /// </summary>
    private void Start() {
        if (statusGame_Object == null || finalScore_Object == null) return;

        statusGame = statusGame_Object.GetComponent<TextMeshProUGUI>();
        finalScore = finalScore_Object.GetComponent<TextMeshProUGUI>();

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Méthode qui met à jour les valeurs du menu de fin et affiche le menu
    /// </summary>
    /// <param name="isWin">Indique si le joueur à gagner ou pas</param>
    /// <param name="score1">Le score du joueur</param>
    /// <param name="score2">Le score de l'ia</param>
    public void Show(bool isWin, int score1, int score2) {
        if (statusGame == null || finalScore == null) return;

        string status = isWin ? "Gagner": "Perdu";
        statusGame.SetText($"{status}");

        finalScore.SetText($"Joueur: {score1}\nIA: {score2}");

        gameObject.SetActive(true);
    }

    /// <summary>
    /// Méthode qui est lancer lorsque le joueur sélectionne le bouton pour rejouer après la fin d'une partie
    /// </summary>
    public void OnReplay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Méthode qui est lancer lorsque le joueur sélectionne le bouton pour quitter le jeu à la fin de la partie
    /// </summary>
    public void OnQuit() {
        SceneManager.LoadScene("Menu");
    }
}