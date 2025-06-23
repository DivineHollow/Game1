using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]private float Speed;
    [SerializeField]private float Damage;
    private float direction;
    private bool hit;
    
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit) return; 
        float movementSpeed = Speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("Explode");

        if (collision.CompareTag("Enemy"))
            collision.GetComponent<Health>().TakeDamage(Damage);

    }

    public void SetDirection(float direction1)
    {
        direction = direction1;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction1)
            localScaleX = -localScaleX;

        transform.localScale = new Vector2(localScaleX, transform.localScale.y);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
