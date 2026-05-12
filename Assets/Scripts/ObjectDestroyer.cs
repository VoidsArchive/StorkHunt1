using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PowerUps>() != null)
        {
            return;
        }

        Destroy(collision.gameObject);
    }
}