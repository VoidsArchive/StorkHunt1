using UnityEngine;
using System.Collections;
using NUnit.Framework.Constraints;

public class TimedObjectPlacer : MonoBehaviour
{
    public GameObject prefab;

    // public float speed = 5f;
    
    public float minimumSecondsToWait;
    public float maximumSecondsToWait;
    
    private bool isOkToCreate = true;

    void Update()
    {
        if (isOkToCreate)
        {
            StartCoroutine(CountdownUntilCreation());
        }
    }
    IEnumerator CountdownUntilCreation()
    {
        isOkToCreate = false;
        yield return new WaitForSeconds(Random.Range(minimumSecondsToWait, maximumSecondsToWait));
        Place();
        isOkToCreate = true;
    }


    public virtual void Place()
    {
        //Instantiate(prefab, SpawnTools.RandomLocationWorldSpace(), Quaternion.identity);
        
        // Spawn just off the right edge of the screen at a random Y position
        Camera cam = Camera.main;
        float rightEdge = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane)).x;
        float topEdge   = cam.ViewportToWorldPoint(new Vector3(0, 0.95f, cam.nearClipPlane)).y;
        float botEdge   = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, cam.nearClipPlane)).y;

        Vector3 spawnPos = new Vector3(
            rightEdge + 1f,                          // just off-screen right
            Random.Range(botEdge, topEdge),          // random height
            0f
        );

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
    
    
}