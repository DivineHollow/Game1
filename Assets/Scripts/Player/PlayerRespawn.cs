using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    [SerializeField] private Transform currentCheckpoint;

    private UIManager uIManager;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uIManager = FindAnyObjectByType<UIManager>();
    }

    public void GameOver()
    {
        uIManager.GameOver();
    }

    public void CheckpointRespawn()
    {
        playerHealth.Respawn();
        transform.position = currentCheckpoint.position + Vector3.up * 0.5f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpoint);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}