using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangementScene : MonoBehaviour
{
    // Nom de la sc�ne � charger
    public string sceneName;

    // Fonction � appeler depuis un bouton UI
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Le nom de la sc�ne est vide !");
        }
    }
}
