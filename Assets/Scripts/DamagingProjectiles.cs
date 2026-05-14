using UnityEngine;

public class DamagingProjectiles : TimedObject
{
    public new void Start()
    {
        secondsOnScreen = 5f;
        base.Start();
    }

    public void Update()
    {
        if (!Game.isGameActive)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!(other.CompareTag("Stork") || other.CompareTag("Crosshair")))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.ChangeHealth(-GameParameters.EnemyProjectileDamage);
        }
    }
}
