using UnityEngine;

public class SimpleMove2D : MonoBehaviour
{
    public Vector2 destiny = Vector2.zero;
    public float velocity = 3f;

    public Rigidbody2D rigidbody;


    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0f;
        rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;

    }

    private void FixedUpdate()
    {
        Vector2 direction = (destiny - (Vector2)transform.position).normalized;
        rigidbody.linearVelocity = velocity * direction;
    }
}
