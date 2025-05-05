using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField]
    private Material dispawnMat;

    private bool scored = false;

    private bool dispawn = false;

    public bool GetDispawn() { return dispawn; }

    public void OnScore() {
        if (scored) return;
        scored = true;

        StartCoroutine(DelayedDestroyAndRespawn());
    }

    public void OnMiss() {
        StartCoroutine(DelayedDestroyAndRespawn());
    }

    private IEnumerator DelayedDestroyAndRespawn() {
        gameObject.GetComponent<MeshRenderer>().material = dispawnMat;

        GameObject go = GameObject.FindGameObjectWithTag("DistanceGrab");
        go.SetActive(false);

        dispawn = true;

        yield return new WaitForSeconds(1f);
        
        Destroy(gameObject);
    }
}
