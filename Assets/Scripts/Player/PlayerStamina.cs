
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [Header("Resistencia")]
    [SerializeField] private float maxStamina = 5f;
    [SerializeField] private float drainPerSecond = 1f;
    [SerializeField] private float regenPerSecond = 1.5f;
    [SerializeField] private float regenDelay = 1f;

    [SerializeField] private float currentStamina;

    private float regenTimer;
    private bool exhausted;

    public float StaminaPercentage =>
        maxStamina > 0f ? currentStamina / maxStamina : 0f;

    private void Awake()
    {
        currentStamina = maxStamina;
    }

    public bool HandleSprint(bool wantsToSprint, float deltaTime)
    {
        // Tras agotarse, debe recuperar un 30% para volver a correr.
        if (exhausted && currentStamina >= maxStamina * 0.3f)
        {
            exhausted = false;
        }

        bool isSprinting =
            wantsToSprint && !exhausted && currentStamina > 0f;

        if (isSprinting)
        {
            currentStamina = Mathf.Max(
                0f,
                currentStamina - drainPerSecond * deltaTime
            );

            regenTimer = regenDelay;

            if (currentStamina <= 0f)
            {
                exhausted = true;
            }
        }
        else
        {
            regenTimer = Mathf.Max(0f, regenTimer - deltaTime);

            if (regenTimer <= 0f)
            {
                currentStamina = Mathf.Min(
                    maxStamina,
                    currentStamina + regenPerSecond * deltaTime
                );
            }
        }

        return isSprinting;
    }
}
