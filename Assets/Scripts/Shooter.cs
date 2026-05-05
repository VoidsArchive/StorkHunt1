using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    private bool clickPending = false;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            clickPending = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Stork") && clickPending)
        {
            Debug.Log("HIT STORK!");
            clickPending = false;

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
}