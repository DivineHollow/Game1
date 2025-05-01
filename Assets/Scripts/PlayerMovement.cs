using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeedX = 10;
    [SerializeField] private float movementSpeedY = 10;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator anim;
    [SerializeField] private bool grounded;
    private bool canDoubleJump;


    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * movementSpeedX, body.linearVelocity.y);

        if (horizontalInput > 0f)
            sprite.flipX = false;
        else if (horizontalInput < 0f)
            sprite.flipX = true;

        if (Input.GetKeyDown(KeyCode.Space) && (grounded || canDoubleJump))
        {
            canDoubleJump = grounded;
            Jump();
        }

        anim.SetBool("Run", Mathf.Abs(horizontalInput) > 0.5f);
        anim.SetBool("Grounded", grounded);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocityX, movementSpeedY);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
