using UnityEngine;

public class DamagingProjectiles : MonoBehaviour
{
    public int damage = GameParameters.EnemyProjectileDamage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
        }
    }
}
