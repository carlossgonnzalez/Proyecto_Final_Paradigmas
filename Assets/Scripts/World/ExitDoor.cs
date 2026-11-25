
using UnityEngine;

public class ExitDoor : MonoBehaviour, IInteractable
{
    [Header("Referencias")]
    [SerializeField] private GameManager gameManager;

    public void Interact(GameObject interactor)
    {
        if (!interactor.TryGetComponent(
            out PlayerInventory inventory))
        {
            Debug.LogWarning("El jugador no tiene inventario.");
            return;
        }

        // Comprobar los objetivos de la partida
        bool hasAllBooks =
            inventory.Books >= inventory.RequiredBooks;

        bool hasAllKeys =
            inventory.Keys >= inventory.RequiredKeys;

        if (!hasAllBooks || !hasAllKeys)
        {
            Debug.Log(
                $"No puedes escapar todavía. " +
                $"Libros: {inventory.Books}/{inventory.RequiredBooks}. " +
                $"Llaves: {inventory.Keys}/{inventory.RequiredKeys}."
            );
            return;
        }

        if (gameManager == null)
        {
            Debug.LogError("Falta asignar GameManager.");
            return;
        }

        // Condicion de victoria cumplida
        gameManager.Win();
    }
}
