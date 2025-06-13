using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private int fireballIndex = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }
    
    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("Attack");
        cooldownTimer = 0;

        GameObject fireball = fireballs[fireballIndex];
        fireball.transform.position = firePoint.position;
        fireball.SetActive(true);
        fireball.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

        fireballIndex = (fireballIndex + 1) % fireballs.Length; 
    }
}

