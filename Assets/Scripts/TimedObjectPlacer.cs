using UnityEngine;
using System.Collections;

public class TimedObjectPlacer : MonoBehaviour
{
    public GameObject prefab;

    // public float speed = 5f;
    
    public float minimumSecondsToWait;
    public float maximumSecondsToWait;
    
    private Coroutine spawnCoroutine;

    protected virtual void Start()
    {
        RestartSpawnLoop();
    }

    protected virtual float GetMinimumSecondsToWait()
    {
        return minimumSecondsToWait;
    }

    protected virtual float GetMaximumSecondsToWait()
    {
        return maximumSecondsToWait;
    }

    protected void RestartSpawnLoop()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }

        spawnCoroutine = StartCoroutine(CountdownUntilCreation());
    }

    protected void StopSpawnLoop()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    IEnumerator CountdownUntilCreation()
    {
        yield return new WaitForSeconds(Random.Range(GetMinimumSecondsToWait(), GetMaximumSecondsToWait()));
        Place();
        spawnCoroutine = StartCoroutine(CountdownUntilCreation());
    }


    public virtual void Place()
    {
        Camera cam = Camera.main;
        float rightEdge = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane)).x;
        float topEdge   = cam.ViewportToWorldPoint(new Vector3(0, 0.95f, cam.nearClipPlane)).y;
        float botEdge   = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, cam.nearClipPlane)).y;

        Vector3 spawnPos = new Vector3(rightEdge + 1f, Random.Range(botEdge, topEdge), 0f);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
    
    
}