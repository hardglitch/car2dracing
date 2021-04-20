using UnityEngine;

public class DropItemCollectable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            if (transform.TryGetComponent<CircleCollider2D>(out var component1))
                component1.isTrigger = true;

            if (transform.TryGetComponent<PolygonCollider2D>(out var component2))
                component2.isTrigger = true;
        }
    }
}
