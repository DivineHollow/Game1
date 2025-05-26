using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField]private float speed;
    [SerializeField]private float jumpSpeed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (horizontalInput > 0.01f) 
            transform.localScale = Vector2.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector2(-1, 1);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
        
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")        
            grounded = true;
    }

}
