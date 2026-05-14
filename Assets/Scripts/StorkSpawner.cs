using UnityEngine;
using System.Collections;

public class StorkSpawner : TimedObjectPlacer
{
    private Coroutine storkSwarmCoroutine;
    private bool isStorkSwarmActive;
    
    protected override void Start()
    {
        minimumSecondsToWait = GameParameters.StorkMinimumSecondsToWait;
        maximumSecondsToWait = GameParameters.StorkMaximumSecondsToWait;
        base.Start();
    }
    public void TriggerStorkSwarm()
    {
        if (storkSwarmCoroutine != null)
        {
            StopCoroutine(storkSwarmCoroutine);
        }
        storkSwarmCoroutine = StartCoroutine(StorkSwarmRoutine());
    }

    // Public API so external controllers (e.g. Game) can pause/resume spawning
    public void StopSpawning()
    {
        StopSpawnLoop();
    }

    public void StartSpawning()
    {
        RestartSpawnLoop();
    }
    protected override float GetMinimumSecondsToWait()
    {
        return isStorkSwarmActive ? 0f : minimumSecondsToWait;
    }
    protected override float GetMaximumSecondsToWait()
    {
        return isStorkSwarmActive ? 1f : maximumSecondsToWait;
    }
    private IEnumerator StorkSwarmRoutine()
    {
        isStorkSwarmActive = false;
        RestartSpawnLoop();
        yield return new WaitForSeconds(GameParameters.StorkSwarmDelaySeconds);
        isStorkSwarmActive = true;
        PowerUps.RegisterTimedPowerup(PowerUps.EffectType.StorkSwarm, "Stork Swarm", GameParameters.StorkSwarmDurationSeconds);
        RestartSpawnLoop();
        yield return new WaitForSeconds(GameParameters.StorkSwarmDurationSeconds);
        isStorkSwarmActive = false;
        RestartSpawnLoop();
        storkSwarmCoroutine = null;
    }
}
