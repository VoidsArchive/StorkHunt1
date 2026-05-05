using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Stop moving if Rigidbody has been switched to Dynamic (i.e. triangle was hit)
        if (rb != null && rb.bodyType == RigidbodyType2D.Dynamic)
            return;

        transform.Translate(Vector3.left * speed * Time.deltaTime);

        Camera cam = Camera.main;
        float leftEdge = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x;

        if (transform.position.x < leftEdge - 1f)
            Destroy(gameObject);
    }
}