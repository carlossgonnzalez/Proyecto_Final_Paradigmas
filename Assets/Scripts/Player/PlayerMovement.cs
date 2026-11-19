
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float walkSpeed = 3.5f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private PlayerStamina stamina;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        stamina = GetComponent<PlayerStamina>();
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        float horizontal = 0f;
        float vertical = 0f;

        // Movimiento WASD
        if (keyboard.aKey.isPressed) horizontal -= 1f;
        if (keyboard.dKey.isPressed) horizontal += 1f;
        if (keyboard.wKey.isPressed) vertical += 1f;
        if (keyboard.sKey.isPressed) vertical -= 1f;

        Vector3 direction =
            transform.right * horizontal +
            transform.forward * vertical;

        direction = direction.normalized;

        // Comprobar si se está pulsando Shift
        bool shiftPressed =
            keyboard.leftShiftKey.isPressed ||
            keyboard.rightShiftKey.isPressed;

        bool wantsToSprint =
            shiftPressed && direction.sqrMagnitude > 0.01f;

        // Consultar al sistema de resistencia
        bool isSprinting = stamina != null &&
            stamina.HandleSprint(wantsToSprint, Time.deltaTime);

        float currentSpeed =
            isSprinting ? sprintSpeed : walkSpeed;

        // Aplicar gravedad
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity =
            direction * currentSpeed +
            Vector3.up * verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }
}
