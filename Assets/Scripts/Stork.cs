using UnityEngine;

public class Stork : TimedObject
{
    public new void Start()
    {
        secondsOnScreen = GameParameters.StorkSecondsOnScreen;
        base.Start();
    }
}
