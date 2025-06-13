using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private float triggerRange;
    [SerializeField] private LayerMask playerLayer;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    private float cooldownTimer;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, triggerRange, playerLayer);

        if (cooldownTimer >= attackCooldown && hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(arrowSound);
        cooldownTimer = 0f;
        int arrowIndex = FindArrow();
        if (arrowIndex != -1)
        {
            GameObject arrow = arrows[arrowIndex];
            arrow.transform.position = firePoint.position;
            arrow.transform.rotation = firePoint.rotation;
            arrow.GetComponent<EnemyProjectile>().ActivateProjectile();
        }
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return -1;
    }
}