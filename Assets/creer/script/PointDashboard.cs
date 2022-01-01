using TMPro;
using UnityEngine;

public class PointDashboard : MonoBehaviour {
    /// <summary>
    /// Le nombre de point pour le joueur 1
    /// </summary>
    private float nbrPointJoueur1;

    /// <summary>
    /// Le nombre de point pour le joueur 2
    /// </summary>
    private float nbrPointJoueur2;

    /// <summary>
    /// Le composant texte qui est affiché dans l'interface
    /// </summary>
    private TextMeshPro pointDashboardText;

    /// <summary>
    /// Un singleton static du script
    /// </summary>
    public static PointDashboard Instance { get; private set; }

    /// <summary>
    /// Méthode qui s'exécute au lancement du script
    /// </summary>
    private void Awake() {
        nbrPointJoueur1 = 0;
        nbrPointJoueur2 = 0;

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        pointDashboardText = GetComponent<TextMeshPro>();

        UpdateInterfaceText();
    }

    /// <summary>
    /// Méthode qui permet d'incrémenter le nombre de point
    /// </summary>
    public void IncrementerPoint(int joueur) {
        if (joueur == 1)
            nbrPointJoueur1++;
        else if (joueur == 2)
            nbrPointJoueur2++;
        
        UpdateInterfaceText();
    }

    private void UpdateInterfaceText() {
        pointDashboardText.SetText($"Joueur: {nbrPointJoueur1}\nIA: {nbrPointJoueur2}");
    }
}