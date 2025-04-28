using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Utiliser TextMesh Pro

public class ChangementScene : MonoBehaviour
{
    public GameObject dropdownObject; 

    private TMP_Dropdown dropdown; 

    private void Start()
    {
        // Trouver le Dropdown au lancement si besoin
        if (dropdownObject == null)
        {
            dropdownObject = GameObject.FindGameObjectWithTag("dropdown");
        }
        if (dropdownObject != null)
        {
            dropdown = dropdownObject.GetComponent<TMP_Dropdown>(); 
        }
        else
        {
            Debug.LogError("Dropdown introuvable !");
        }
    }

    public void LoadScene()
    {
        if (dropdown == null)
        {
            Debug.LogError("Dropdown non assigné !");
            return;
        }

        // Récupérer l'index de l'élément sélectionné
        int selectedIndex = dropdown.value;
        string selectedSceneName = ""; // Initialiser la variable

        // Définir le nom de la scène en fonction de l'index sélectionné
        if (selectedIndex == 0)
        {
            selectedSceneName = "map_urbain";
        }
        else if (selectedIndex == 1)
        {
            selectedSceneName = "map_nature";
        }
        else if (selectedIndex == 2)
        {
            selectedSceneName = "map_plage";
        }

        // Vérifier que le nom de la scène est bien assigné
        if (!string.IsNullOrEmpty(selectedSceneName))
        {
            SceneManager.LoadScene(selectedSceneName);
        }
        else
        {
            Debug.LogWarning("Le nom de la scène sélectionnée est vide ou non défini !");
        }
    }
}
