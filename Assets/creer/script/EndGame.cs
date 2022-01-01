using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGame : MonoBehaviour {
    [SerializeField]
    private GameObject statusGame_Object;
    private TextMeshProUGUI statusGame;

    [SerializeField]
    private GameObject finalScore_Object;
    private TextMeshProUGUI finalScore;

    public static EndGame Instance;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() {
        if (statusGame_Object == null || finalScore_Object == null) return;

        statusGame = statusGame_Object.GetComponent<TextMeshProUGUI>();
        finalScore = finalScore_Object.GetComponent<TextMeshProUGUI>();

        gameObject.SetActive(false);
    }

    public void Show(bool isWin, int score1, int score2) {
        if (statusGame == null || finalScore == null) return;

        string status = isWin ? "Gagner": "Perdu";
        statusGame.SetText($"{status}");

        finalScore.SetText($"Joueur: {score1}\nIA: {score2}");

        gameObject.SetActive(true);
    }

    public void OnReplay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuit() {
        SceneManager.LoadScene("Menu");
    }
}