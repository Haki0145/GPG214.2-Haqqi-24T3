using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private Rigidbody2D rb;
    private bool isGravityFlipped = false;

    public GameObject gravityHint;
    private bool isNearSign = false;
    private bool hasShownHint = false;
    public Transform spawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityHint.SetActive(false);
       
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded )
        {
            Vector2 jumpDirection = isGravityFlipped ? Vector2.down : Vector2.up;
            rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && isNearSign)
        {
            FlipGravity();
        }
    }

    private void FlipGravity()
    {
        isGravityFlipped = !isGravityFlipped;
        rb.gravityScale *= -1;
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.position = spawnPoint.position;
            DeafultGravity();

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sign"))
        {
            isNearSign = true;

            if (!hasShownHint)
            {
                gravityHint.SetActive(true);
                hasShownHint = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sign"))
        {
            isNearSign = false;
            gravityHint.SetActive(false);
        }
    }

    void DeafultGravity()
    {
        isGravityFlipped = false;
        rb.gravityScale = 1;
    }
}