using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeedX = 10;
    [SerializeField] private float movementSpeedY = 10;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    private bool canDoubleJump;
    private BoxCollider2D boxCollider;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * movementSpeedX, body.linearVelocity.y);

        if (horizontalInput > 0f)
            sprite.flipX = false;
        else if (horizontalInput < 0f)
            sprite.flipX = true;

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded() || canDoubleJump))
        {
            canDoubleJump = isGrounded();
            Jump();
        }

        anim.SetBool("Run", Mathf.Abs(horizontalInput) > 0.5f);
        anim.SetBool("Grounded", isGrounded());
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocityX, movementSpeedY);
        anim.SetTrigger("Jump");
 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
 
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D raycastHit = raycastHit2D;
        return raycastHit.collider != null;
    }
}
