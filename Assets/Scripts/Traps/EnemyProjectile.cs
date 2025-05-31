using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;

    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;
    private bool hit;
    private int direction = 1;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }

    public void SetDirection(int _direction)
    {
        direction = _direction;
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * direction;
        transform.localScale = localScale;
    }

    private void Update()
    {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(Vector3.right * direction * movementSpeed);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;

        if (collision.CompareTag("Player"))
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(damage);
        }

        coll.enabled = false;

        if (anim != null)
            anim.SetTrigger("Explode");
        else
            gameObject.SetActive(false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}