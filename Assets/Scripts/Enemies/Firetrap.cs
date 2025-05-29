using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    [SerializeField] private float damageInterval = 1f; // добавлено

    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    private Dictionary<GameObject, float> damageTimers = new Dictionary<GameObject, float>();

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        List<GameObject> keys = new List<GameObject>(damageTimers.Keys);
        foreach (GameObject obj in keys)
        {
            damageTimers[obj] -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            StartCoroutine(ActivateFiretrap());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!active || !collision.CompareTag("Player")) return;

        if (!damageTimers.ContainsKey(collision.gameObject))
        {
            damageTimers[collision.gameObject] = 0f;
        }

        if (damageTimers[collision.gameObject] <= 0f)
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
                damageTimers[collision.gameObject] = damageInterval;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damageTimers.ContainsKey(collision.gameObject))
        {
            damageTimers.Remove(collision.gameObject);
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        spriteRend.color = Color.red;

        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("Activated", true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("Activated", false);
    }
}

