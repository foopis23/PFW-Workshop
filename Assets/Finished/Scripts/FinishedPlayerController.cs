using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedPlayerController : MonoBehaviour
{
    public float moveVelocity;
    public Vector2 jumpForce;
    public LayerMask groundLayer;

    public Rigidbody2D body;
    public BoxCollider2D boxCollider;
    public SpriteRenderer characterRenderer;

    private float horizontal = 0.0f;
    private bool jumpQueued = false;
    private Vector2 velocity;
    private float initialGravity;

    private void Start()
    {
        velocity = body.velocity;
        initialGravity = body.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        // read move input
        horizontal = Input.GetAxis("Horizontal");

        // queue jump
        if (!jumpQueued)
            jumpQueued = Input.GetButton("Jump");

        // update direction character is facing based on current move direction
        if (Mathf.Abs(body.velocity.x) > 0.05f)
        {
            characterRenderer.flipX = body.velocity.x < 0.0f;
        }
    }

    private void FixedUpdate()
    {
        // if attempting to jump and grounded, jump
        if (jumpQueued && isGrounded())
        {
            body.AddForce(jumpForce);
            jumpQueued = false;
        }

        // set x velocity based on current input
        velocity.x = moveVelocity * horizontal;
        velocity.y = body.velocity.y;
        body.velocity = velocity;

        // want character to feel less floaty, so I double gravity only when falling
        if (velocity.y > 0.1f)
        {
            body.gravityScale = initialGravity;
        } else
        {
            body.gravityScale = initialGravity * 2;
        }
    }

    bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, transform.up * -1.0f, (boxCollider.size.y / 2.0f) + 0.025f, groundLayer);
    }
}
