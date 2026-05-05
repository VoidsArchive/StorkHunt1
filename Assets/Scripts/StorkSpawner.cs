using UnityEngine;

public class StorkSpawner : TimedObjectPlacer
{
    public void Start()
    {
        minimumSecondsToWait = GameParameters.StorkMinimumSecondsToWait;
        maximumSecondsToWait = GameParameters.StorkMaximumSecondsToWait;
    }
}
