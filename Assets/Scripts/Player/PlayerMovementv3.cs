using Unity.Mathematics;
using UnityEngine;

public class PlayerMovementv2 : MonoBehaviour
{
    [SerializeField] private float movementSpeedX = 10;
    [SerializeField] private float movementSpeedY = 10;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private BoxCollider2D boxCollider;
    private bool canDoubleJump;
    private float wallJumpCooldown;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0f)
            sprite.flipX = false;
        else if (horizontalInput < 0f)
            sprite.flipX = true;

        bool isGrounded = IsGrounded();

        body.linearVelocity = new Vector2(horizontalInput * movementSpeedX, body.linearVelocity.y);

        anim.SetBool("Run", Mathf.Abs(horizontalInput) > 0.5f);
        anim.SetBool("Grounded", isGrounded);

        if (wallJumpCooldown > 0.2f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (OnWall() && !isGrounded)
                {
                    wallJumpCooldown = 0.2f;
                    Jump();
                }
                else if (isGrounded || canDoubleJump)
                {
                    Jump();
                    canDoubleJump = isGrounded;
                }
            }
        }
        else
                wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            
        }
        body.linearVelocity = new Vector2(body.linearVelocityX, movementSpeedY);
        anim.SetTrigger("Jump");
 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
 
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D raycastHit = raycastHit2D;
        return raycastHit.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(sprite.flipX ? -1f : 1f, 0f), 0.1f, wallLayer);
        RaycastHit2D raycastHit = raycastHit2D;
        return raycastHit.collider != null;
    }
}   
