using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;

    [Header("Damage")]
    [SerializeField] private float damage;
    [SerializeField] private float damageInterval = 1f;

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private Dictionary<GameObject, float> damageTimers = new Dictionary<GameObject, float>();

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;
        }

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