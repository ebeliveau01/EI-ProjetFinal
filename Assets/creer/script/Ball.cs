using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour {
    private bool scored = false;

    public void OnScore() {
        if (scored) return;
        scored = true;

        StartCoroutine(DelayedDestroyAndRespawn());
    }

    private IEnumerator DelayedDestroyAndRespawn() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
