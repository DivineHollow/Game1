using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField] private float movementSpeedX = 10;
    [SerializeField] private float movementSpeedY = 10;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField]private AudioClip jumpSound;
 
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");      

        if (horizontalInput > 0.01f) 
            transform.localScale = Vector2.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector2(-1, 1);

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", IsGrounded());

        if (wallJumpCooldown > 0.2f)
        {      
            body.linearVelocity = new Vector2(horizontalInput * movementSpeedX, body.linearVelocity.y);

            if (OnWall() && !IsGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
                body.gravityScale = 5;

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();

                if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
                    SoundManager.instance.PlaySound(jumpSound);
            }
                
        }
        else
            wallJumpCooldown += Time.deltaTime; 
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, movementSpeedY);
            anim.SetTrigger("Jump");
        }
        else if (OnWall() && !IsGrounded()) 
        {
            if(horizontalInput == 0)
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), transform.localScale.y);
            }
            else
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;          
        }
      
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return horizontalInput == 0 && IsGrounded() && !OnWall(); 
    }
}
