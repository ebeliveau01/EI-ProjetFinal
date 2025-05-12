using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour {
    /// <summary>
    /// Le matériel de la ball lorsqu'elle est mise en mode de disparition
    /// </summary>
    [SerializeField]
    private Material dispawnMat;

    /// <summary>
    /// Variable qui permet de vérifier si la ball à déjà marquée un point
    /// </summary>
    private bool scored = false;

    /// <summary>
    /// Variable qui permet de vérifier si la ball est déjà en mode de disparition
    /// </summary>
    private bool dispawn = false;

    /// <summary>
    /// Méthode qui permet d'obtenir la variable dispawn
    /// </summary>
    public bool GetDispawn() { return dispawn; }

    /// <summary>
    /// Méthode qui est appeler lorsque la ball marque un point
    /// </summary>
    public void OnScore() {
        if (scored) return;
        scored = true;

        StartCoroutine(DelayedDestroyAndRespawn());
    }

    /// <summary>
    /// Méthode qui est appeler lorsque la ball manque le but
    /// </summary>
    public void OnMiss() {
        StartCoroutine(DelayedDestroyAndRespawn());
    }

    /// <summary>
    /// Coroutine qui permet de faire disparaitre la ball après une seconde et qui change le matériel de la ball
    /// </summary>
    private IEnumerator DelayedDestroyAndRespawn() {
        gameObject.GetComponent<MeshRenderer>().material = dispawnMat;

        GameObject go = GameObject.FindGameObjectWithTag("DistanceGrab");
        go.SetActive(false);

        dispawn = true;

        yield return new WaitForSeconds(1f);
        
        Destroy(gameObject);
    }
}
