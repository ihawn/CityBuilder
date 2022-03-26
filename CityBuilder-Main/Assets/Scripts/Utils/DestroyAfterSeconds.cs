using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float seconds;
    public bool deactivate;

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterTimeElapsed());
    }

    IEnumerator DestroyAfterTimeElapsed()
    {
        yield return new WaitForSeconds(seconds);
        if (deactivate)
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
    }
}
