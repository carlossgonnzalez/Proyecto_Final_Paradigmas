
using UnityEngine;

public class PlayerStaminaUI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PlayerStamina playerStamina;
    [SerializeField] private RectTransform staminaBackground;
    [SerializeField] private RectTransform staminaFill;

    [Header("Configuracion")]
    [SerializeField] private float horizontalPadding = 3f;

    private void Update()
    {
        if (playerStamina == null ||
            staminaBackground == null ||
            staminaFill == null)
        {
            return;
        }

        float maxWidth = Mathf.Max(
            0f,
            staminaBackground.rect.width - horizontalPadding * 2f
        );

        float currentWidth =
            maxWidth * Mathf.Clamp01(playerStamina.StaminaPercentage);

        // Cambiar la anchura sin deformar la imagen
        staminaFill.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            currentWidth
        );
    }
}
