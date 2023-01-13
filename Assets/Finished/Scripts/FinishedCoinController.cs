using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedCoinController : MonoBehaviour
{
    public float fallVelocity;
    public LayerMask playerLayerMask;
    public Rigidbody2D body;
    public CircleCollider2D collider;
    public float maxLifeTime;

    private float expiration;

    // Start is called before the first frame update
    private void Start()
    {
        body.gravityScale = 0.0f;
        body.velocity = new Vector2(0.0f, -1 * fallVelocity);

        expiration = Time.time + maxLifeTime;
    }

    private void Update()
    {
        if (Time.time > expiration)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = transform.up * -1.0f;
        float distance = collider.radius + 0.025f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, playerLayerMask);
        if (hit && hit.collider.gameObject.CompareTag("Basket"))
        {
            // TODO: add points???

            Destroy(gameObject);
        }
    }
}
