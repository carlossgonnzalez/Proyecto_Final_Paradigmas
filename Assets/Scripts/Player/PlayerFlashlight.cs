
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFlashlight : MonoBehaviour
{
    [Header("Linterna")]
    [SerializeField] private Light flashlight;
    [SerializeField] private bool startsOn = true;

    private void Start()
    {
        if (flashlight != null)
        {
            flashlight.enabled = startsOn;
        }
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null || flashlight == null)
            return;

        // Encender o apagar al pulsar F
        if (keyboard.fKey.wasPressedThisFrame)
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }
}
