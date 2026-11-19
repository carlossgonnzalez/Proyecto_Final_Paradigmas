
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private RectTransform healthBackground;
    [SerializeField] private RectTransform healthFill;

    [Header("Configuracion")]
    [SerializeField] private float horizontalPadding = 3f;

    private void Start()
    {
        if (playerHealth == null ||
            healthBackground == null ||
            healthFill == null)
        {
            Debug.LogError("Faltan referencias en PlayerHealthUI.");
            return;
        }

        // Suscribirse a los cambios de vida
        playerHealth.HealthChanged += UpdateHealthBar;

        // Mostrar el valor inicial
        UpdateHealthBar(playerHealth.HealthPercentage);
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.HealthChanged -= UpdateHealthBar;
        }
    }

    private void UpdateHealthBar(float percentage)
    {
        float maxWidth = Mathf.Max(
            0f,
            healthBackground.rect.width - horizontalPadding * 2f
        );

        float currentWidth =
            maxWidth * Mathf.Clamp01(percentage);

        healthFill.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            currentWidth
        );

        // Ocultar el relleno cuando la vida llega a cero
        healthFill.gameObject.SetActive(percentage > 0f);
    }
}
