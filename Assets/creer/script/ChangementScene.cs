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
            Debug.LogError("Dropdown non assign� !");
            return;
        }

        // R�cup�rer l'index de l'�l�ment s�lectionn�
        int selectedIndex = dropdown.value;
        string selectedSceneName = ""; // Initialiser la variable

        // D�finir le nom de la sc�ne en fonction de l'index s�lectionn�
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

        // V�rifier que le nom de la sc�ne est bien assign�
        if (!string.IsNullOrEmpty(selectedSceneName))
        {
            SceneManager.LoadScene(selectedSceneName);
        }
        else
        {
            Debug.LogWarning("Le nom de la sc�ne s�lectionn�e est vide ou non d�fini !");
        }
    }
}
