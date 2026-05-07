using UnityEngine;

public class Stork : TimedObject
{
    public new void Start()
    {
        secondsOnScreen = GameParameters.StorkSecondsOnScreen;
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }
}