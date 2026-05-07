using UnityEngine;

public class DamagingProjectiles : TimedObject
{
    public new void Start()
    {
        secondsOnScreen = 5f;
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Stork"))
        {
            Destroy(gameObject);
        }
    }

    public int damage = GameParameters.EnemyProjectileDamage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
        }
    }
}
