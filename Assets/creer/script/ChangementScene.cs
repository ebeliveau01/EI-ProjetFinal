using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangementScene : MonoBehaviour
{
    // Nom de la scène à charger
    public string sceneName;

    // Fonction à appeler depuis un bouton UI
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Le nom de la scène est vide !");
        }
    }
}
