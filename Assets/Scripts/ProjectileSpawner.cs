using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    
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
        
        float secondsToWait = Random.Range(GameParameters.ProjectileMinimumSecondsToWait, GameParameters.ProjectileMaximumSecondsToWait);
        yield return new WaitForSeconds(secondsToWait);
        Place();
        
        isOkToCreate = true;
    }

    public virtual void Place()
    {
        Instantiate(ProjectilePrefab, transform.position , Quaternion.identity);
    }
    
    
}