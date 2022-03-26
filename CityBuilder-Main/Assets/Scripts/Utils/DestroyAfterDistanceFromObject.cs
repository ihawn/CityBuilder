using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDistanceFromObject : MonoBehaviour
{
    public GameObject obj;
    public float distance;
    public bool deactivate;
    float timestep = 0.5f;

    private void OnEnable()
    {
        StartCoroutine(CheckForDestroy());
    }

    IEnumerator CheckForDestroy()
    {
        yield return new WaitForSeconds(timestep);

        while (Vector3.Distance(transform.position, obj.transform.position) < distance)
        {
            yield return new WaitForSeconds(timestep);
        }

        transform.position = Vector3.one * 1000000; //hacky but this causes all on trigger exits to fire

        yield return new WaitForSeconds(0.2f);
        if (deactivate)
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
    }
}
