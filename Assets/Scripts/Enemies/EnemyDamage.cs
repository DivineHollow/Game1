using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float damageInterval = 1f;

    private Dictionary<GameObject, float> damageTimers = new Dictionary<GameObject, float>();

    private void Update()
    {
        List<GameObject> keys = new List<GameObject>(damageTimers.Keys);
        foreach (GameObject obj in keys)
        {
            damageTimers[obj] -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

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
}
