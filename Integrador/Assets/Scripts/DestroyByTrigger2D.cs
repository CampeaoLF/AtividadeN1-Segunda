using UnityEngine;

public class DestroyByTrigger2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject); 
    }
}
