using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public BulletManager BulletManager;
    private bool clickPending = false;

    public Animator Animator;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            clickPending = true;
            BulletManager.Shoot();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            clickPending = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (clickPending && !BulletManager.GetIsReloading())
        {
            if (other.CompareTag("Stork"))
            {
                //Animator.SetBool("IsFlying", false);
                Debug.Log("HIT STORK!");
                clickPending = false;

                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null && rb.bodyType != RigidbodyType2D.Dynamic)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    TimedObject timed = other.GetComponent<TimedObject>();
                    Animator.SetBool("IsFalling", true);
                    if (timed != null)
                        timed.CancelDeath();
                }
            }
        }
        clickPending = false;
    }
}