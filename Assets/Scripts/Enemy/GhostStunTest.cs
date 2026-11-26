
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostStunTest : MonoBehaviour
{
    [SerializeField] private float stunDuration = 5f;

    private GhostController ghost;

    private void Awake()
    {
        ghost = GetComponent<GhostController>();
    }

    private void Update()
    {
        if (Keyboard.current == null || ghost == null)
            return;

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            ghost.Stun(stunDuration);
        }
    }
}