
using UnityEngine;

public class GhostAttack : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private float attackRange = 1.8f;
    [SerializeField] private float attackCooldown = 1.5f;

    private Transform player;
    private PlayerHealth playerHealth;

    private float nextAttackTime;

    public bool IsPlayerDead =>
        playerHealth != null && playerHealth.IsDead;

    public bool Initialize(Transform playerTransform)
    {
        player = playerTransform;

        if (player == null)
            return false;

        playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth not found.");
            return false;
        }

        return true;
    }

    public bool CanAttack()
    {
        if (player == null ||
            playerHealth == null ||
            IsPlayerDead)
        {
            return false;
        }

        Vector3 difference =
            player.position - transform.position;

        // Ignorar la diferencia de altura
        difference.y = 0f;

        return difference.magnitude <= attackRange;
    }

    public void TryAttack()
    {
        if (!CanAttack())
            return;

        if (Time.time < nextAttackTime)
            return;

        nextAttackTime = Time.time + attackCooldown;

        playerHealth.TakeDamage(damage);

        Debug.Log("Ghost attack! Damage: " + damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            attackRange
        );
    }
}
