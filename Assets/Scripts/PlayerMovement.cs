using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeedX = 10;
    [SerializeField] private float movementSpeedY = 10;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D body;


    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * movementSpeedX, body.linearVelocity.y);

        if (horizontalInput > 0f)
            sprite.flipX = false;
        else if (horizontalInput < 0f)
            sprite.flipX = true;

        if (Input.GetKeyDown(KeyCode.Space))
            body.linearVelocity = new Vector2(body.linearVelocityX, movementSpeedY);
    }

}
