
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida del jugador")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    public float HealthPercentage =>
        maxHealth > 0f ? currentHealth / maxHealth : 0f;

    public bool IsDead => currentHealth <= 0f;

    // Eventos para comunicar los cambios de vida
    public event Action<float> HealthChanged;
    public event Action Died;

    private void Awake()
    {
        maxHealth = Mathf.Max(1f, maxHealth);
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0f || IsDead)
            return;

        currentHealth = Mathf.Max(
            0f,
            currentHealth - damage
        );

        HealthChanged?.Invoke(HealthPercentage);

        if (IsDead)
        {
            Debug.Log("El jugador ha muerto.");
            Died?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        if (amount <= 0f || IsDead)
            return;

        currentHealth = Mathf.Min(
            maxHealth,
            currentHealth + amount
        );

        HealthChanged?.Invoke(HealthPercentage);
    }

    // Pruebas disponibles desde el Inspector de Unity
#if UNITY_EDITOR
    [ContextMenu("TEST: Recibir 20 de daño")]
    private void TestDamage()
    {
        TakeDamage(20f);
    }

    [ContextMenu("TEST: Recuperar 20 de vida")]
    private void TestHeal()
    {
        Heal(20f);
    }
#endif
}
