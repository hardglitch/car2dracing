using UnityEngine;

public class DropItemStatic : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
