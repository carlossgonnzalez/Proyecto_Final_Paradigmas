
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    [Header("Requisitos")]
    [SerializeField] private int requiredKeys = 1;

    public void Interact(GameObject interactor)
    {
        // Obtener el inventario del jugador
        if (!interactor.TryGetComponent(
            out PlayerInventory inventory))
        {
            Debug.LogWarning(
                "No se ha encontrado PlayerInventory."
            );
            return;
        }

        // Comprobar si tiene suficientes llaves
        if (inventory.Keys < requiredKeys)
        {
            Debug.Log("Necesitas una llave para abrir esta puerta.");
            return;
        }

        // Abrir la puerta
        Debug.Log("Puerta abierta.");
        gameObject.SetActive(false);
    }
}
