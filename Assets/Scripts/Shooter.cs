using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public BulletManager BulletManager;
    private bool clickPending = false;
    public static int storksShot = 0;
    public static void ResetStorksShot()
    {
        storksShot = 0;
    }
    public static int getStorksShot()
    {
        return storksShot;
    }
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
                Debug.Log("HIT STORK!");
                storksShot++;
                clickPending = false;

                Stork stork = other.GetComponent<Stork>();
                if (stork != null)
                {
                    stork.MarkAsShotDown();
                }
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null && rb.bodyType != RigidbodyType2D.Dynamic)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    TimedObject timed = other.GetComponent<TimedObject>();
                    if (timed != null)
                        timed.CancelDeath();
                }
            }
        }
        clickPending = false;
    }
}