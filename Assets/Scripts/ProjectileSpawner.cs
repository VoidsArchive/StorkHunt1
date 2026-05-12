using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    
    private bool isOkToCreate = true;
    private Stork stork;

    private void Start()
    {
        stork = GetComponent<Stork>();
    }

    void Update()
    {
        if (isOkToCreate && CanStillSpawnProjectiles())
        { 
            StartCoroutine(CountdownUntilCreation());
        }
        
    }

    IEnumerator CountdownUntilCreation()
    {
        if (isOkToCreate)
        {
            isOkToCreate = false;

            if (!CanStillSpawnProjectiles())
            {
                isOkToCreate = true;
                yield break;
            }

            float secondsToWait = Random.Range(GameParameters.ProjectileMinimumSecondsToWait,
                GameParameters.ProjectileMaximumSecondsToWait);
            yield return new WaitForSeconds(secondsToWait);

            if (!CanStillSpawnProjectiles())
            {
                isOkToCreate = true;
                yield break;
            }

            Place();

            isOkToCreate = true;
        }
    }

    public virtual void Place()
    {
        Instantiate(ProjectilePrefab, transform.position , Quaternion.identity);
    }

    private bool CanStillSpawnProjectiles()
    {
        return stork == null || !stork.IsShotDown;
    }
}