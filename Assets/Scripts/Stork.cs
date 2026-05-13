using UnityEngine;

public class Stork : TimedObject

{
    public Animator animator;
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
    
    public void StartFalling()
    {
        animator.SetBool("IsFalling", true);
    }
}